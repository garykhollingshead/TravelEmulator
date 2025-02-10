using System.Text;
using TravelEmulator.Data;
using TravelEmulator.Vehicles;

namespace TravelEmulator.Generators;

public class VehicleGenerator
{
    private readonly BodyStyleTypes[] _bodyTypes = Enum.GetValues<BodyStyleTypes>();
    private readonly FuelTypes[] _fuelTypes = Enum.GetValues<FuelTypes>();
    private readonly PowerTypes[] _powerTypes = Enum.GetValues<PowerTypes>();

    private string GenerateRandomWord()
    {
        return GenerateRandomWord(RandomGenerator.Generator.Next(2, 13));
    }

    private string GenerateRandomWord(int length)
    {
        var wordBuilder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var nextCharacter = (char) RandomGenerator.Generator.Next('a', 'z' + 1);
            wordBuilder.Append(nextCharacter);
        }

        return wordBuilder.ToString();
    }

    public VehicleBase GenerateRandom(VehicleTypes vehicleType)
    {
        return vehicleType == VehicleTypes.Car ? GenerateRandomCar() : GenerateRandomBoat();
    }
    
    public Car GenerateRandomCar()
    {
        var descriptor = GenerateRandomWord();
        var manufacturer = GenerateRandomWord();
        
        var weight = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        var width = RandomGenerator.Generator.NextDouble() * 5 + 5;
        var height = RandomGenerator.Generator.NextDouble() * 5 + 5;
        var length = RandomGenerator.Generator.NextDouble() * 5 + 5;
        
        var modelYear = (uint)RandomGenerator.Generator.Next(1920, DateTime.Now.Year);

        var bodyStyle = _bodyTypes[RandomGenerator.Generator.Next(_bodyTypes.Length)];
        var fuel = _fuelTypes[RandomGenerator.Generator.Next(_fuelTypes.Length)];

        return new Car(descriptor, weight, width, height, length, manufacturer, modelYear,
            bodyStyle, fuel);
    }
    
    public Boat GenerateRandomBoat()
    {
        var descriptor = GenerateRandomWord();
        var manufacturer = GenerateRandomWord();
        
        var weight = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        var width = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        var height = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        var length = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        var draft = RandomGenerator.Generator.NextDouble() * 5000 + 1000;
        
        var power = _powerTypes[RandomGenerator.Generator.Next(_powerTypes.Length)];

        return new Boat(descriptor, weight, width, height, length, draft, manufacturer, power);
    }
}