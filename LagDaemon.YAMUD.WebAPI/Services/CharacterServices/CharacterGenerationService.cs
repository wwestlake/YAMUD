using LagDaemon.YAMUD.Model.Characters;
using LagDaemon.YAMUD.WebAPI.Utilities;
using Npgsql.TypeMapping;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices;

public class CharacterGenerationService : ICharacterGenerationService
{
    private IRandomNumberService _randomNumberService;
    private INameGenerator _nameGenerator;
    private IUserAccountService _userAccountService;

    public CharacterGenerationService(IRandomNumberService randomNumberService, INameGenerator nameGenerator, IUserAccountService userAccountService)
    {
        _randomNumberService = randomNumberService;
        _nameGenerator = nameGenerator;
        _userAccountService = userAccountService;
    }

    public async Task<Character> GenerateCharacter()
    {
        var character = new Character();
        character.Id = Guid.NewGuid();
        character.Name = GenerateName();
        character.Strength = _randomNumberService.GetRandomInt(1, 10);
        character.Dexterity = _randomNumberService.GetRandomInt(1, 10);
        character.Intelligence = _randomNumberService.GetRandomInt(1, 10);
        character.Dexterity = _randomNumberService.GetRandomInt(1, 10);
        character.Luck = _randomNumberService.GetRandomInt(1, 10);
        character.HealthPoints = 100;
        character.MaxHealthPoints = 100;
        character.ManaPoints = 100;
        character.MaxManaPoints = 100;
        character.Level = 1;
        character.ExperiencePoints = 0;
        (await _userAccountService.GetCurrentUser()).OnSuccess(x => { 
            character.OwnerId = x.ID;
        }).OnFailure(x => { 
            character.OwnerId = Guid.Empty;
        });
        character.CreatedAt = DateTime.Now.ToUniversalTime();
        return character;
    }
    private string GenerateName()
    {
        return _nameGenerator.GenerateName(5, 15);
    }
}
