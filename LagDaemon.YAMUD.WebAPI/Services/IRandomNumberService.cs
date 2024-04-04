namespace LagDaemon.YAMUD.WebAPI.Services
{
    public interface IRandomNumberService
    {
        float GetRandomFloat();
        int GetRandomInt();
        int GetRandomInt(int max);
        int GetRandomInt(int min, int max);
    }
}
