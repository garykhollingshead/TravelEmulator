using System.Text;
using TravelEmulator.Data;
using TravelEmulator.Vehicles;

namespace TravelEmulator.Generators;

public class VehicleGenerator
{
    private Random _randomGenerator;
    private readonly BodyStyleTypes[] _bodyTypes = Enum.GetValues<BodyStyleTypes>();
    private readonly FuelTypes[] _fuelTypes = Enum.GetValues<FuelTypes>();
    private readonly PowerTypes[] _powerTypes = Enum.GetValues<PowerTypes>();

    public VehicleGenerator(Random randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    private string GenerateRandomWord()
    {
        return GenerateRandomWord(_randomGenerator.Next(2, 13));
    }

    private string GenerateRandomWord(int length)
    {
        var wordBuilder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var nextCharacter = (char) _randomGenerator.Next('a', 'z' + 1);
            wordBuilder.Append(nextCharacter);
        }

        return wordBuilder.ToString();
    }
    
    public Car GenerateRandomCar()
    {
        var descriptor = GenerateRandomWord();
        var manufacturer = GenerateRandomWord();
        
        var weight = _randomGenerator.NextDouble() * 5000 + 1000;
        var width = _randomGenerator.NextDouble() * 5 + 5;
        var height = _randomGenerator.NextDouble() * 5 + 5;
        var length = _randomGenerator.NextDouble() * 5 + 5;
        
        var modelYear = (uint)_randomGenerator.Next(1920, DateTime.Now.Year);

        var bodyStyle = _bodyTypes[_randomGenerator.Next(_bodyTypes.Length)];
        var fuel = _fuelTypes[_randomGenerator.Next(_fuelTypes.Length)];

        return new Car(descriptor, weight, width, height, length, manufacturer, modelYear,
            bodyStyle, fuel);
    }
    
    public Boat GenerateRandomBoat()
    {
        var descriptor = GenerateRandomWord();
        var manufacturer = GenerateRandomWord();
        
        var weight = _randomGenerator.NextDouble() * 5000 + 1000;
        var width = _randomGenerator.NextDouble() * 5000 + 1000;
        var height = _randomGenerator.NextDouble() * 5000 + 1000;
        var length = _randomGenerator.NextDouble() * 5000 + 1000;
        var draft = _randomGenerator.NextDouble() * 5000 + 1000;
        
        var power = _powerTypes[_randomGenerator.Next(_powerTypes.Length)];

        return new Boat(descriptor, weight, width, height, length, draft, manufacturer, power);
    }
}