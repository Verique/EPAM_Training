using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveData
{
    public static class SaveManager
    {
        public static void Save()
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/saveData.dat";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                var data = new GameData();
                formatter.Serialize(stream, data);
            }
        }

        public static GameData Load()
        {
            var path = Application.persistentDataPath + "/saveData.dat";

            if (!File.Exists(path)) 
                throw new FileNotFoundException();
            
            var formatter = new BinaryFormatter();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as GameData;
            }
        }
    }
}