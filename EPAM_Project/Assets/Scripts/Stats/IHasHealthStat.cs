namespace Stats
{
    public interface IHasHealthStat
    {
        Stat<int> Health { get; }
    }
}