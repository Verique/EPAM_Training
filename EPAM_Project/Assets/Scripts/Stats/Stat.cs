using System;

namespace Stats
{
    [Serializable]
    public struct Stat<T>
    {
        public string name;
        public T value;

        public Stat(string name, T value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
