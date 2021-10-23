namespace SaveData
{
    public interface ISaveable<T>
    {
        public T GetSaveData();
        public void LoadData(T data);
    }
}