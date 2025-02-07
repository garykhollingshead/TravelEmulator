using System.Text;
using TravelEmulator.Data;

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

    private static readonly IList<(Coordinate, Coordinate)> TravelZones = new List<(Coordinate, Coordinate)>
    {
        (new Coordinate(15.6, -49.8), new Coordinate(56.2, -23.1)),
        (new Coordinate(-48.8, -28.6), new Coordinate(-6.9, 8.2)),
        (new Coordinate(-43.4, -161.4), new Coordinate(8.1, -98.4)),
        (new Coordinate(-41.1, 62.2), new Coordinate(-1.4, 94.5)),
    };

    // lower left, upper right corners
    private readonly (Coordinate, Coordinate) _travelZone;
    
    public Boat(string descriptor, double weight, double width, double height, double length, double speedInMph, double maxTurning,
        double draft, string manufacturer, PowerTypes power) : 
        base("BOAT", descriptor, weight, width, height, length, speedInMph, maxTurning)
    {
        _power = power;
        Draft = draft;
        Manufacturer = manufacturer;

        _travelZone = TravelZones[RandomGenerator.Next(0, TravelZones.Count - 1)];
    }

    public Boat(string descriptor, double weight, double width, double height, double length, double draft, string manufacturer, PowerTypes power) : 
        this(descriptor, weight, width, height, length, 0, 30, draft, manufacturer, power)
    {
        _power = power;
        Draft = draft;
        Manufacturer = manufacturer;
        
        SpeedInMph = RandomGenerator.Next(TravelSpeeds[_power].Item1, TravelSpeeds[_power].Item2);
    }

    public override string GetDetailsForJny()
    {
        return $"{base.GetDetailsForJny()},{Power},{Draft},{Manufacturer}";
    }

    public override bool CanNavigate(Coordinate coordinate)
    {
        var lowerLeftCoord = GetLowerLeftTravelZoneCoordinate();
        var upperRightCoord = GetUpperRightTravelZoneCoordinate();
        return coordinate.Latitude >= lowerLeftCoord.Latitude && coordinate.Latitude <= upperRightCoord.Latitude &&
               coordinate.Longitude >= lowerLeftCoord.Longitude && coordinate.Longitude <= upperRightCoord.Longitude;
    }

    public Coordinate GetLowerLeftTravelZoneCoordinate()
    {
        return _travelZone.Item1;
    }

    public Coordinate GetUpperRightTravelZoneCoordinate()
    {
        return _travelZone.Item2;
    }
}