using System;
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
        private const string audioSettingsPath = "settings/audio";
        
        public void Save(string saveName)
        {
            var playerData = ServiceLocator.Instance.Get<PlayerManager>().GetSaveData();
            var enemyData = ServiceLocator.Instance.Get<EnemyManager>().GetSaveData();
            var gameData = ServiceLocator.Instance.Get<GameManager>().GetSaveData();
            SaveJson(new GameData(gameData, playerData, enemyData), saveName);
        }

        public void Load(string saveName)
        {
            var gameData = LoadJson<GameData>(saveName);
            ServiceLocator.Instance.Get<PlayerManager>().LoadData(gameData.playerData); 
            ServiceLocator.Instance.Get<EnemyManager>().LoadData(gameData.enemyData);
            ServiceLocator.Instance.Get<GameManager>().LoadData(gameData.gameStateData);
        }

        private static T LoadJson<T>(string fileName)
        {
            var path = string.Format(Application.persistentDataPath + $"/{fileName}.json");

            if (!new FileInfo(path).Exists) return default;

            using var sr = new StreamReader(path);
            using var reader = new JsonTextReader(sr);

            return new JsonSerializer().Deserialize<T>(reader);
        }

        private static void SaveJson<T>(T data, string fileName)
        {
            var path = string.Format(Application.persistentDataPath + $"/{fileName}.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new InvalidOperationException());
            using var sw = new StreamWriter(path, false);
            using var writer = new JsonTextWriter(sw);
            
            new JsonSerializer().Serialize(writer, data);
        }

        public void SaveAudioSettings(GameAudioSettings settings) =>
            SaveJson(settings, audioSettingsPath);

        public GameAudioSettings LoadAudioSettings()
        {
            var settings = LoadJson<GameAudioSettings>(audioSettingsPath);
            
            if (settings == null)
            {
                settings = new GameAudioSettings();
                SaveAudioSettings(settings);
            }

            return settings;
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