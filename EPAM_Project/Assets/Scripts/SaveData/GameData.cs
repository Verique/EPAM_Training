using System;
using Extensions;
using Services;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        public PlayerData PlayerData;
        public GameData(PlayerData playerData)
        {
            PlayerData = playerData;
        }
    }
}