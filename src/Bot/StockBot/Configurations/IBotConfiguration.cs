namespace StockBot.Configurations
{
    public interface IBotConfiguration
    {
        string Password { get; set; }
        string ServicesUrl { get; set; }
        string User { get; set; }
    }
}