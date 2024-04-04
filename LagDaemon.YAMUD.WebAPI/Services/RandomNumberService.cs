namespace LagDaemon.YAMUD.WebAPI.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private Random _random;

        public RandomNumberService()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public RandomNumberService(int seed)
        {
            _random = new Random(seed);
        }

        public float GetRandomFloat()
        {
            return (float)_random.NextDouble();
        }

        public int GetRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public int GetRandomInt(int max)
        {
            return _random.Next(max);
        }

        public int GetRandomInt()
        {
            return _random.Next();
        }
    }
}
