using System.Text;

namespace LagDaemon.YAMUD.WebAPI.Services.CharacterServices
{
    public class NameGenerator : INameGenerator
    {
        private static readonly string[] Vowels = { "a", "e", "i", "o", "u" };
        private static readonly string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

        private readonly Random random = new Random();

        public string GenerateName(int minLength, int maxLength)
        {
            StringBuilder name = new StringBuilder();
            int length = random.Next(minLength, maxLength + 1);

            for (int i = 0; i < length; i++)
            {
                if (i % 2 == 0)
                {
                    name.Append(GetRandomConsonant());
                }
                else
                {
                    name.Append(GetRandomVowel());
                }
            }

            return name.ToString();
        }

        private string GetRandomVowel()
        {
            return Vowels[random.Next(Vowels.Length)];
        }

        private string GetRandomConsonant()
        {
            return Consonants[random.Next(Consonants.Length)];
        }
    }
}
