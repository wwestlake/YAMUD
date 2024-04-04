using LagDaemon.YAMUD.Model.Characters;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices;

public class CharacterGenerationService
{
    private IRandomNumberService _randomNumberService;
    private INameGenerator _nameGenerator;

    public CharacterGenerationService(IRandomNumberService randomNumberService, INameGenerator nameGenerator)
    {
        _randomNumberService = randomNumberService;
        _nameGenerator = nameGenerator;
    }

    public Character GenerateCharacter()
    {
        var character = new Character();

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


        return character;
    }

    private string GenerateName()
    {
        return _nameGenerator.GenerateName(5, 15);
    }
}
