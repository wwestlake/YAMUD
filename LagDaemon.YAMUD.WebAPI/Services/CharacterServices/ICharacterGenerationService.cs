using LagDaemon.YAMUD.Model.Characters;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public interface ICharacterGenerationService
    {
        Task<Character> GenerateCharacter();
    }
}
