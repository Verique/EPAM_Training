using System;

namespace SaveData
{
    [Serializable]
    public class GameStateData
    {
        public int kills;
        public float time;

        public GameStateData(int kills, float time)
        {
            this.kills = kills;
            this.time = time;
        }
    }
}