using System;
using Extensions;
using Services;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        public SerializableVector3 position;
        public GameData()
        {
            position = ServiceLocator.Instance.Get<PlayerManager>().Transform.position.ToSerializable();
        }
    }
}