namespace TravelEmulator.Generators;

public class RandomGenerator
{
    private readonly Random _randomGenerator;

    private static readonly Lazy<RandomGenerator> LazyInstance = new Lazy<RandomGenerator>(() => new RandomGenerator());
    
    public static Random Generator => LazyInstance.Value._randomGenerator;
    public static int Seed = Convert.ToInt32(DateTime.Now.Ticks % int.MaxValue);
    
    private RandomGenerator()
    {
        _randomGenerator = new Random(Seed);
    }
}