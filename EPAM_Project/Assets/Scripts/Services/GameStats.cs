namespace Services
{
    public class GameStats
    {
        public int Kills;
        public string TimeSurvived;
        public string Message;

        public GameStats(int kills, float timeSurvived, string message)
        {
            Kills = kills;
            TimeSurvived = $"{(int) timeSurvived / 60}:{(int) timeSurvived % 60}";
            Message = message;
        }
    }
}