using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.DebugViews;

namespace GeometricReplication
{
    class UserData
    {
        public UserData(int id, int player_ID, bool is_player) 
        {
            ID = id;
            bIsPlayer = is_player;
            playerID = player_ID;
        }
        public int ID;
        public bool bIsPlayer = false;
        public int playerID;
    }
}
