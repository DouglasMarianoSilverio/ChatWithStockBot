namespace StockBot.Configurations
{
    public class BotConfiguration : IBotConfiguration
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string ServicesUrl { get; set; }
    }
}
