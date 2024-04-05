using LagDaemon.YAMUD.Model.Characters;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(Character character);
        Task<IEnumerable<Character>> GetAll();
        Task<IEnumerable<Character>> GetAllForCurrentUser();
        Task<Character> UpdateOrCreateCharacter(Character character);
        Task DeleteCharacter(Guid id);
        Task<Character> GetCharacter(Guid id);
    }
}
