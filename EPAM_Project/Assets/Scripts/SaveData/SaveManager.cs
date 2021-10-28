using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Services;
using UnityEngine;
using Newtonsoft.Json;

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
            var path = Application.persistentDataPath + "/saveDataJSON.txt";
            using var sw = new StreamWriter(path, false);
            using var writer = new JsonTextWriter(sw);
            
            new JsonSerializer().Serialize(writer, data);
        }

        private GameData LoadGameData()
        {
            var path = Application.persistentDataPath + "/saveDataJSON.txt";
            using var sw = new StreamReader(path);
            using var reader = new JsonTextReader(sw);

            return new JsonSerializer().Deserialize<GameData>(reader);
        }

        private void SaveGameDataBinary(GameData data)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/saveData.dat";

            using var stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
        }
        
        private GameData LoadGameDataBinary()
        {
            var path = Application.persistentDataPath + "/saveData.dat";

            if (!File.Exists(path)) throw new FileNotFoundException();
            
            var formatter = new BinaryFormatter();

            using var stream = new FileStream(path, FileMode.Open);
            return formatter.Deserialize(stream) as GameData;
        }
    }
}