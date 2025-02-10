namespace TravelEmulator.Data;

public class WayPoint
{
    private readonly Coordinate _coordinate;
    public double Latitude => _coordinate.Latitude;
    public double Longitude => _coordinate.Longitude;
    public double DeltaTime { get; }
    public double Heading { get; }

    public WayPoint(Coordinate coordinate, double deltaTime, double heading)
    {
        _coordinate = coordinate ?? throw new Exception("Coordinate cannot be null for a WayPoint.");
        DeltaTime = deltaTime;
        Heading = heading;
    }
}