using System;
using Extensions;
using Services;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        public PlayerData playerData;
        public GameData(PlayerData playerData)
        {
            this.playerData = playerData;
        }
    }
}