using System.Collections.Generic;
using System.IO;
using System.Linq;
using Services;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveData
{
    public class SaveManager : MonoBehaviour, IService
    {
        private const string saveNamesFileName = "saveFileNames";
        
        public void Save(string saveName)
        {
            var playerData = ServiceLocator.Instance.Get<PlayerManager>().GetSaveData();
            var enemyData = ServiceLocator.Instance.Get<EnemyManager>().GetSaveData();
            SaveJson(new GameData(playerData, enemyData), saveName);
        }

        public void Load(string saveName)
        {
            var gameData = LoadJson<GameData>(saveName);
            ServiceLocator.Instance.Get<PlayerManager>().LoadData(gameData.playerData); 
            ServiceLocator.Instance.Get<EnemyManager>().LoadData(gameData.enemyData);
        }

        private static T LoadJson<T>(string fileName)
        {
            var path = string.Format(Application.persistentDataPath + $"/{fileName}.json"); 
            using var sr = new StreamReader(path);
            using var reader = new JsonTextReader(sr);

            return new JsonSerializer().Deserialize<T>(reader);
        }

        private static void SaveJson<T>(T data, string fileName)
        {
            var path = string.Format(Application.persistentDataPath + $"/{fileName}.json"); 
            using var sw = new StreamWriter(path, false);
            using var writer = new JsonTextWriter(sw);
            
            new JsonSerializer().Serialize(writer, data);
        }

        public static void NewSaveFile(string name)
        {
            var saveFiles = LoadJson<List<string>>(saveNamesFileName);
            
            saveFiles.Add(name);
            
            SaveJson(saveFiles, saveNamesFileName);
        }

        public static List<string> GetSaveFiles()
        {
            return new DirectoryInfo(Application.persistentDataPath)
                .EnumerateFiles()
                .Where(info => info.Name.EndsWith(".json"))
                .OrderBy(info => info.LastWriteTime)
                .Select(info => info.Name.Replace(".json", ""))
                .ToList();
        }
    }
}