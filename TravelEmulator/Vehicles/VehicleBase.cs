using System.Text;
using SNC;
using TravelEmulator.Data;

namespace TravelEmulator.Vehicles;

public abstract class VehicleBase
{
    public string Identifier { get; private set; }
    public string Descriptor { get; private set; }
    public double Weight { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }
    public double Length { get; private set; }
    public double Speed { get; private set; }
    public double MaxTurning { get; private set; }

    public VehicleBase(string identifier, string descriptor, double weight, double width, double height, double length, double speed, double maxTurning)
    {
        Identifier = identifier;
        Descriptor = descriptor;
        Weight = weight;
        Width = width;
        Height = height;
        Length = length;
        Speed = speed;
        MaxTurning = maxTurning;
    }

    public virtual string GetDetails()
    {
        return $"{Identifier},{Descriptor},{Weight},{Width},{Height},{Length}";
    }

    public virtual bool CanNavigate(Coordinate coordinate)
    {
        return true;
    }

    public double GetTravelTime(Coordinate start, Coordinate end)
    {
        GeoCalc.GetGreatCircleDistance(start.Latitude, start.Longitude, end.Latitude, end.Longitude, out var distance);

        return distance;
    }
}