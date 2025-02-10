using TravelEmulator.Data;
using TravelEmulator.Generators;

namespace UnitTests;

[TestFixture]
public class VehicleTests
{
    [SetUp]
    public void SetUp()
    {
        RandomGenerator.Seed = 5;
    }
    
    [TestCaseSource(nameof(GetCoordinatesInsideOfWaterZones))]
    public void CanNavigate_GivenCarInsideWaterZone_ReturnsTrue(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var car = vehicleGenerator.GenerateRandomCar();
        Assert.That(car.CanNavigate(coordinate), Is.True);
    }
    
    [TestCaseSource(nameof(GetCoordinatesOutsideOfWaterZones))]
    public void CanNavigate_GivenCarOutsideWaterZone_ReturnsTrue(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var car = vehicleGenerator.GenerateRandomCar();
        Assert.That(car.CanNavigate(coordinate), Is.True);
    }
    
    [TestCaseSource(nameof(GetCoordinatesOnWaterZoneLines))]
    public void CanNavigate_GivenCarOnWaterZoneLine_ReturnsTrue(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var car = vehicleGenerator.GenerateRandomCar();
        Assert.That(car.CanNavigate(coordinate), Is.True);
    }
    
    [TestCaseSource(nameof(GetCoordinatesInsideOfWaterZones))]
    public void CanNavigate_GivenBoatInsideWaterZone_ReturnsTrue(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var boat = vehicleGenerator.GenerateRandomBoat();
        Assert.That(boat.CanNavigate(coordinate), Is.True);
    }
    
    [TestCaseSource(nameof(GetCoordinatesOutsideOfWaterZones))]
    public void CanNavigate_GivenBoatOutsideWaterZone_ReturnsFalse(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var boat = vehicleGenerator.GenerateRandomBoat();
        Assert.That(boat.CanNavigate(coordinate), Is.True);
    }
    
    [TestCaseSource(nameof(GetCoordinatesOnWaterZoneLines))]
    public void CanNavigate_GivenBoatOnWaterZoneLine_ReturnsTrue(Coordinate coordinate)
    {
        var vehicleGenerator = new VehicleGenerator();
        var boat = vehicleGenerator.GenerateRandomBoat();
        Assert.That(boat.CanNavigate(coordinate), Is.True);
    }

    private static IEnumerable<Coordinate> GetCoordinatesInsideOfWaterZones()
    {
        var zones = new List<Coordinate>();
        foreach (var (lowerLeft, upperRight) in WaterZones.Zones)
        {
            zones.Add(new Coordinate(lowerLeft.Latitude + 0.1, lowerLeft.Longitude + 0.1));
            zones.Add(new Coordinate(upperRight.Latitude - 0.1, upperRight.Longitude - 0.1));
        }

        return zones;
    }

    private static IEnumerable<Coordinate> GetCoordinatesOutsideOfWaterZones()
    {
        var zones = new List<Coordinate>();
        foreach (var (lowerLeft, upperRight) in WaterZones.Zones)
        {
            zones.Add(new Coordinate(lowerLeft.Latitude + 0.1, lowerLeft.Longitude + 0.1));
            zones.Add(new Coordinate(upperRight.Latitude - 0.1, upperRight.Longitude - 0.1));
        }

        return zones;
    }

    private static IEnumerable<Coordinate> GetCoordinatesOnWaterZoneLines()
    {
        var zones = new List<Coordinate>();
        foreach (var (lowerLeft, upperRight) in WaterZones.Zones)
        {
            zones.Add(new Coordinate(lowerLeft.Latitude + 0.1, lowerLeft.Longitude + 0.1));
            zones.Add(new Coordinate(upperRight.Latitude - 0.1, upperRight.Longitude - 0.1));
        }

        return zones;
    }
}