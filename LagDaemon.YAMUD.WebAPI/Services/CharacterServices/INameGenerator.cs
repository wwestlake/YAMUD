namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public interface INameGenerator
    {
        string GenerateName(int minLength, int maxLength);
    }
}
