using System;

namespace Stats
{
    [Serializable]
    public class Stat<T>
    {
        public string name;
        public T value;
    }
}
