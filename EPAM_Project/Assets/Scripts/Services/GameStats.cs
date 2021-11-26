namespace Services
{
    public class GameStats
    {
        public readonly int Kills;
        public readonly string TimeSurvived;
        public readonly string Message;

        public GameStats(int kills, float timeSurvived, string message)
        {
            Kills = kills;
            TimeSurvived = $"{(int) timeSurvived / 60}:{(int) timeSurvived % 60}";
            Message = message;
        }
    }
}