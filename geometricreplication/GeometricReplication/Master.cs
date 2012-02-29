using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;

namespace GeometricReplication
{
    class Master
    {
        const int MAX_UNITS = 25;
        const int PLAYERS = 2;
        const int SCREENBOUNDS_WIDTH = 800;
        const int SCREENBOUNDS_HEIGHT = 600;
        public int GameTimeLeft = 1;
        public bool roundOver = false;
        //int gameMode = 0;
        List<float> playerScores = new List<float>();

        private readonly Vector2 theGravity;
        private DebugViewXNA theView;
        private World _theWorld;
        private static Master _theMaster;
        public Game game;

        private CirclePlayer circlePlayer;
        private SquarePlayer squarePlayer;
        private List<Enemy> enemies;

        internal int StepCount;
        private GamePadState _oldGamePad;
        private int maxEnemyNumber = 30;
        private Vector2 worldsize;

        private SoundFx gSoundfx;
        private BackgroundSong gSong;
        private Leaderboard gLeaderboard;
        private GameBackground gBackground;

        private SpriteBatch sb;

        private camera _camera;

        private Border border;

        public float dt;

        private int CircleCount;
        private int SquareCount;
        private int NeutralCount;

        Texture2D CircleBar, SquareBar, NeutralBar;

        Credits credits;

        public World theWorld
        {
            get
            {
                if (_theWorld != null)
                {
                    return _theWorld;
                }
                else
                {
                    _theWorld = new World(theGravity);                   
                    return _theWorld;
                }
            }
        }

        public static Master theMaster
        {
            get
            {
                if (_theMaster != null)
                    return _theMaster;
                else
                    return null;
            }
            set
            {
                if (_theMaster == null)
                    _theMaster = value;
                else
                    throw new Exception("blarghhh");
            }
        }

        public DebugViewXNA Renderer
        {
            get
            {
                if (theView != null)
                    return theView;
                else
                {
                    theView = new DebugViewXNA(theWorld);
                    return theView;
                }
            }
        }

        public Master(Game game)
        {
            theGravity = Vector2.Zero;
            this.game = game;
            for (int i = 0; i < PLAYERS; i++)
            {
                playerScores.Add(0);
            }            
            worldsize = new Vector2(1000, 1000);            
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, camera.transform);
            gBackground.drawThis(sb);
            circlePlayer.drawCollisionParticles(sb, gameTime);
            squarePlayer.drawCollisionParticles(sb, gameTime);
            sb.End();
            
            sb.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, camera.transform);
            border.Draw(sb);
            circlePlayer.Draw(sb);
            
            squarePlayer.Draw(sb);
            
            foreach (Enemy enemy in enemies)            
            {
                enemy.Draw(sb);
            }
            
            
            sb.End();

            
            
            gLeaderboard.drawLeaderboard(game, gameTime, this, CircleCount, NeutralCount, SquareCount, maxEnemyNumber);
            game.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //DrawContestBar(game.spriteBatch);
            game.spriteBatch.End();
        }

        private void DrawContestBar(SpriteBatch sb)
        {
            //sb.Draw(CircleBar, new Rectangle(0, 730, 800 * CircleCount / maxEnemyNumber, 70), Color.White);
            //sb.Draw(NeutralBar, new Rectangle(800 * CircleCount / maxEnemyNumber, 730, 800 * NeutralCount / maxEnemyNumber, 70), Color.White);
            //sb.Draw(SquareBar, new Rectangle(800 * (CircleCount + NeutralCount) / maxEnemyNumber, 730, 800 * SquareCount / maxEnemyNumber, 70), Color.White);
        }

        public void InitializeSfx(Game1 cGame)
        {
            
            gSong = new BackgroundSong(cGame);
            gSoundfx = new SoundFx(cGame);
            gLeaderboard = new Leaderboard(cGame);
            gBackground = new GameBackground(cGame);
        }

        public void Initialize()
        {
            //credits = new Credits();

            theView = new DebugViewXNA(theWorld);
            theView.LoadContent(game.GraphicsDevice, game.Content);
            circlePlayer = new CirclePlayer();
            circlePlayer.Init();
            squarePlayer = new SquarePlayer();
            squarePlayer.Init();

            Texture2D[] textures = new Texture2D[6];
            textures[0] = game.Content.Load<Texture2D>("images/WhiteHexagon");
            textures[1] = game.Content.Load<Texture2D>("images/WhiteOctagon");
            textures[2] = game.Content.Load<Texture2D>("images/WhiteDiamond");
            textures[3] = game.Content.Load<Texture2D>("images/WhiteTriangle");
            textures[4] = game.Content.Load<Texture2D>("images/WhitePantage");
            textures[5] = game.Content.Load<Texture2D>("images/WhiteStar");

            //CircleBar = game.Content.Load<Texture2D>("images/Bar/CircleBar");
            //SquareBar = game.Content.Load<Texture2D>("images/Bar/SquareBar");
            //NeutralBar = game.Content.Load<Texture2D>("images/Bar/NeutralBar");

            Random rand = new Random();
            enemies = new List<Enemy>();
            int j = 0;
            for (int i = 0; i < maxEnemyNumber; i++)
            {
                Enemy enemy = new Enemy();
                
                enemy.Init(i, textures[j]);
                enemy.Position = new Vector2((float)rand.Next(800), (float)rand.Next(800)); 
                enemies.Add(enemy);
                j++;
                if (j > 5)
                    j = 0;
            }
            
            _oldGamePad = GamePad.GetState(PlayerIndex.One);
            _camera = new camera(game, 0);
            sb = new SpriteBatch(game.GraphicsDevice);

            circlePlayer.Position = new Vector2(500, 500);
            squarePlayer.Position = new Vector2(500, 500);
            border = new Border(_theWorld, worldsize.X / 2, worldsize.Y / 2, 10.0f);
        }

        public void Update(GameTime gameTime)
        {
            //credits.Update(gameTime);
            float timeStep = dt = Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, (1f / 30f));
            if (gLeaderboard.countDownMins == -1 && gLeaderboard.countDownSecs == 0 && GameTimeLeft <= 0)
            {
                roundOver = true;
                gLeaderboard.doOnce = false;
            }
            else
            {
                gBackground.Update(gameTime);
                gSong.playSong();
                circlePlayer.Update();
                squarePlayer.Update();

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.userData.playerID != -1)
                    {
                        UpdateScore(enemy.userData.playerID, dt);                        
                    }
                    // add here
                }
                UpdateContestBar(enemies);
                
                _theWorld.Step(timeStep);
                if (timeStep > 0.0f)
                {
                    ++StepCount;
                }
                
                _camera.update(gameTime, this);
            }
        }

        private void UpdateContestBar(List<Enemy> enemies)
        {
            NeutralCount = SquareCount = CircleCount = 0;
            foreach (Enemy enemy in enemies)
            {
                switch (enemy.userData.playerID)
                {
                    case -1:
                        NeutralCount++;
                        break;
                    case 0:
                        SquareCount++;
                        break;
                    case 1:
                        CircleCount++;
                        break;
                    default:
                        break;
                }
            }
        }

        public List<float> returnScore
        {
            get { return playerScores; }
        }

        public SoundFx returnGameSound
        {
            get { return gSoundfx; }
        }

        public BackgroundSong returnBackgroundSound
        {
            get { return gSong; }
        }

        public void UpdateScore(int playerID, float amt)
        {
            playerScores[playerID] += amt;
        }

        internal Enemy GetEnemy(int p)
        {
            return enemies[p];
        }

        internal Rectangle playerspace()
        {
            Rectangle rect1 = new Rectangle();
            rect1.Y = (int)(circlePlayer.Position.Y < squarePlayer.Position.Y ? circlePlayer.Position.Y : squarePlayer.Position.Y);
            rect1.X = (int)(circlePlayer.Position.X < squarePlayer.Position.X ? circlePlayer.Position.X : squarePlayer.Position.X);
            rect1.Width = (int)(circlePlayer.Position.X < squarePlayer.Position.X ?
                squarePlayer.Position.X - circlePlayer.Position.X : circlePlayer.Position.X - squarePlayer.Position.X);
            rect1.Height = (int)(circlePlayer.Position.Y < squarePlayer.Position.Y ? squarePlayer.Position.Y - circlePlayer.Position.Y : circlePlayer.Position.Y - squarePlayer.Position.Y);
            return rect1;
        }
    }
}
