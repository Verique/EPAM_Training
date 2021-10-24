using System;
using System.Collections.Generic;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        public PlayerData playerData;
        public List<EnemyData> enemyData;
        public GameData(PlayerData playerData, List<EnemyData> enemyData)
        {
            this.playerData = playerData;
            this.enemyData = enemyData;
        }
    }
}