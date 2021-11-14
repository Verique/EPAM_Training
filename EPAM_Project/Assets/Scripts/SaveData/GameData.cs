using System;
using System.Collections.Generic;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        public GameStateData gameStateData;
        public PlayerData playerData;
        public List<EnemyData> enemyData;

        public GameData(GameStateData gameStateData, PlayerData playerData, List<EnemyData> enemyData)
        {
            this.gameStateData = gameStateData;
            this.playerData = playerData;
            this.enemyData = enemyData;
        }
    }
}