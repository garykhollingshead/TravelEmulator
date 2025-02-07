using System.Text;
using TravelEmulator.Data;
using TravelEmulator.SNC;

namespace TravelEmulator.Vehicles;

public abstract class VehicleBase
{
    public string Identifier { get; }
    public string Descriptor { get; }
    public double Weight { get; }
    public double Width { get; }
    public double Height { get; }
    public double Length { get; }
    public double SpeedInMph { get; protected set; }
    public double MaxTurning { get; }

    protected readonly Random RandomGenerator;

    public VehicleBase(string identifier, string descriptor, double weight, double width, double height, double length, double speedInMph, 
        double maxTurning, Random randomGenerator = null)
    {
        Identifier = identifier;
        Descriptor = descriptor;
        Weight = weight;
        Width = width;
        Height = height;
        Length = length;
        SpeedInMph = speedInMph;
        MaxTurning = maxTurning;

        if (randomGenerator == null)
        {
            var seed = Convert.ToInt32(DateTime.Now.Ticks % int.MaxValue);
            randomGenerator = new Random(seed);
        }

        RandomGenerator = randomGenerator;
    }

    public virtual string GetDetailsForJny()
    {
        return $"{Identifier},{Descriptor},{Weight},{Width},{Height},{Length}";
    }

    public virtual bool CanNavigate(Coordinate coordinate)
    {
        return true;
    }

    public double GetTravelTime(Coordinate start, Coordinate end)
    {
        GeoCalc.GetGreatCircleDistance(start.Latitude, start.Longitude, end.Latitude, end.Longitude, out var distanceInFeet);

        var speedInFph = SpeedInMph * 1.467;
        var travelTimeInHours = distanceInFeet / speedInFph;
        return travelTimeInHours * 60 * 60;
    }

    public double GetTravelDistanceInFeetFromSecondsTraveled(double secondsTraveled)
    {
        var hoursTraveled = secondsTraveled / (60 * 60);
        var distanceInMiles = SpeedInMph / hoursTraveled;
        return distanceInMiles * 5280;
    }

    public double GetNextHeading(double heading)
    {
        var turn = RandomGenerator.NextDouble() * (2 * MaxTurning) - MaxTurning;
        return GetTurnHeading(heading, turn);
    }

    public double GetMaxLeftHeading(double heading)
    {
        return GetTurnHeading(heading, -MaxTurning);
    }

    public double GetMaxRightHeading(double heading)
    {
        return GetTurnHeading(heading, MaxTurning);
    }

    private double GetTurnHeading(double heading, double turn)
    {
        var newHeading = heading + turn;
        
        if (newHeading < 0)
        {
            return newHeading + 360;
        }

        if (newHeading >= 360)
        {
            return newHeading - 360;
        }

        return newHeading;
    }
}