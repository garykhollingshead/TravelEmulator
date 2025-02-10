using System.Text;
using TravelEmulator.Data;
using TravelEmulator.Generators;

namespace TravelEmulator.Vehicles;

public class Boat : VehicleBase
{
    private readonly PowerTypes _power;
    public string Power => _power.ToString();
    public double Draft { get; }
    public string Manufacturer { get; }

    private static readonly IDictionary<PowerTypes, (int, int)> TravelSpeeds = new Dictionary<PowerTypes, (int, int)>
    {
        {PowerTypes.UNPOWERED, (1, 10)},
        {PowerTypes.SAIL, (15, 30)},
        {PowerTypes.MOTOR, (25, 60)}
    };
    
    public Boat(string descriptor, double weight, double width, double height, double length, double speedInMph, double maxTurning,
        double draft, string manufacturer, PowerTypes power) : 
        base("BOAT", descriptor, weight, width, height, length, speedInMph, maxTurning)
    {
        _power = power;
        Draft = draft;
        Manufacturer = manufacturer;
    }

    public Boat(string descriptor, double weight, double width, double height, double length, double draft, string manufacturer, PowerTypes power) : 
        this(descriptor, weight, width, height, length, 0, 30, draft, manufacturer, power)
    {
        _power = power;
        Draft = draft;
        Manufacturer = manufacturer;
        
        SpeedInMph = RandomGenerator.Generator.Next(TravelSpeeds[_power].Item1, TravelSpeeds[_power].Item2);
    }

    public override bool CanNavigate(Coordinate coordinate)
    {
        var isInsideWaterZone = false;
        foreach (var (lowerLeftCoord, upperRightCoord) in WaterZones.Zones)
        {
            if (coordinate.Latitude >= lowerLeftCoord.Latitude && coordinate.Latitude <= upperRightCoord.Latitude &&
                coordinate.Longitude >= lowerLeftCoord.Longitude && coordinate.Longitude <= upperRightCoord.Longitude)
            {
                isInsideWaterZone = true;
            }
        }

        return isInsideWaterZone;
    }
}