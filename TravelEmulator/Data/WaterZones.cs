namespace TravelEmulator.Data;

public static class WaterZones
{
    public static readonly IList<(Coordinate, Coordinate)> Zones = new List<(Coordinate, Coordinate)>
    {
        (new Coordinate(15.6, -49.8), new Coordinate(56.2, -23.1)),
        (new Coordinate(-48.8, -28.6), new Coordinate(-6.9, 8.2)),
        (new Coordinate(-43.4, -161.4), new Coordinate(8.1, -98.4)),
        (new Coordinate(-41.1, 62.2), new Coordinate(-1.4, 94.5)),
    };
}