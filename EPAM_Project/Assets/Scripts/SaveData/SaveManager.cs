

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using Services;
using UnityEngine;

namespace SaveData
{
    public static class SaveManager
    {
        /*public static void Save()
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/saveData.dat";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                var playerManager = ServiceLocator.Instance.GetComponent<PlayerManager>();
                var data = new GameData(new PlayerData(playerManager.transform.position, playerManager.GetComponent<PlayerMovement>().Speed));
                formatter.Serialize(stream, data);
            }
        }*/
    }
}