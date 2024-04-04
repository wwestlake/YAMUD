using LagDaemon.YAMUD.WebAPI.Services;
using LagDaemon.YAMUD.WebAPI.Services.CharacterServices;
using Moq;

namespace LagDaemon.YAMUD.WebAPI.Tests.Services
{
    public class CharacterGeneratorTests
    {
        const string STR_CharacterName = "CharacterName";
        [Fact]
        public void CharacterGenerator_GenerateCharacter_ReturnsCharacter()
        {
            // Arrange
            var randomNumberService = new Mock<IRandomNumberService>();
            var nameGenerator = new Mock<INameGenerator>();
            var characterGenerator = new CharacterGenerationService(randomNumberService.Object, nameGenerator.Object);

            randomNumberService.Setup(x => x.GetRandomInt(It.IsAny<int>(), It.IsAny<int>())).Returns(5);
            nameGenerator.Setup(x => x.GenerateName(It.IsAny<int>(), It.IsAny<int>())).Returns(STR_CharacterName);
            // Act
            var character = characterGenerator.GenerateCharacter();

            // Assert
            Assert.NotNull(character);
            Assert.NotNull(character.Name);
            Assert.Equal(STR_CharacterName, Assert.IsType<string>(character.Name));
            Assert.True(character.Strength >= 1 && character.Strength <= 10);
            Assert.True(character.Dexterity >= 1 && character.Dexterity <= 10);
            Assert.True(character.Intelligence >= 1 && character.Intelligence <= 10);
            Assert.True(character.Luck >= 1 && character.Luck <= 10);
            Assert.Equal(100, character.HealthPoints);
            Assert.Equal(100, character.MaxHealthPoints);
            Assert.Equal(100, character.ManaPoints);
            Assert.Equal(100, character.MaxManaPoints);
            Assert.Equal(1, character.Level);
            Assert.Equal(0, character.ExperiencePoints);
        }
    }
}
