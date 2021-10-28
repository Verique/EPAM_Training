using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Services;
using UnityEngine;

namespace SaveData
{
    public class SaveManager : MonoBehaviour, IService
    {
        public void Save()
        {
            var playerData = ServiceLocator.Instance.Get<PlayerManager>().GetSaveData();
            var enemyData = ServiceLocator.Instance.Get<EnemyManager>().GetSaveData();
            SaveGameData(new GameData(playerData, enemyData));
        }

        public void Load()
        {
            var gameData = LoadGameData();
            ServiceLocator.Instance.Get<PlayerManager>().LoadData(gameData.playerData); 
            ServiceLocator.Instance.Get<EnemyManager>().LoadData(gameData.enemyData);
        }

        private void SaveGameData(GameData data)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/saveData.dat";

            using var stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
        }
        
        private GameData LoadGameData()
        {
            var path = Application.persistentDataPath + "/saveData.dat";

            if (!File.Exists(path)) throw new FileNotFoundException();
            
            var formatter = new BinaryFormatter();

            using var stream = new FileStream(path, FileMode.Open);
            return formatter.Deserialize(stream) as GameData;
        }
    }
}