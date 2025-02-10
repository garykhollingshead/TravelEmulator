using System.Text;
using TravelEmulator.Data;
using TravelEmulator.Vehicles;

namespace TravelEmulator.Generators;

public class JnyGenerator
{
    public VehicleBase Vehicle { get; }

    public IList<WayPoint> WayPoints { get; }

    public JnyGenerator(VehicleBase vehicle, IList<WayPoint> wayPoints)
    {
        Vehicle = vehicle ?? throw new Exception("Vehicle cannot be null for a JNY file.");
        WayPoints = wayPoints ?? throw new Exception("WayPoints cannot be null for a JNY file.");
    }

    public string GetJnyString()
    {
        var sb = new StringBuilder();

        sb.AppendLine(GetVehicleDetails(Vehicle));

        foreach (var wayPoint in WayPoints)
        {
            sb.AppendLine(GetWayPointDetails(wayPoint));
        }

        return sb.ToString();
    }

    private string GetVehicleBaseDetails()
    {
        return
            $"{Vehicle.Identifier},{Vehicle.Descriptor},{Vehicle.Weight:F4},{Vehicle.Width:F4},{Vehicle.Height:F4},{Vehicle.Length:F4}";
    }

    public string GetCarDetails(Car car)
    {
        return $"{GetVehicleBaseDetails()},{car.Manufacturer},{car.ModelYear},{car.BodyStyle},{car.Fuel}";
    }

    public string GetBoatDetails(Boat boat)
    {
        return $"{GetVehicleBaseDetails()},{boat.Power},{boat.Draft:F4},{boat.Manufacturer}";
    }

    public string GetVehicleDetails(VehicleBase vehicleBase)
    {
        if (Vehicle is Car car)
        {
            return GetCarDetails(car);
        }

        if (Vehicle is Boat boat)
        {
            return GetBoatDetails(boat);
        }

        throw new Exception("Unable to get details from Vehicle. Not a boat or car.");
    }

    public string GetWayPointDetails(WayPoint wayPoint)
    {
        return wayPoint.DeltaTime == 0
            ? $"{wayPoint.Latitude:F6},{wayPoint.Longitude:F6},0"
            : $"{wayPoint.Latitude:F6},{wayPoint.Longitude:F6},{wayPoint.DeltaTime:F2}";
    }
}