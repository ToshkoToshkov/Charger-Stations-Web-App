namespace Charger_Stations_Web_App.Services.Dealers
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int GetIdByUser(string userId);
    }
}
