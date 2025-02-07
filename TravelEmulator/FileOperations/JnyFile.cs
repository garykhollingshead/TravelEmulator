using System.Text;
using TravelEmulator.Data;
using TravelEmulator.Vehicles;

namespace TravelEmulator.FileOperations;

public class JnyFile
{
    public VehicleBase Vehicle { get; }

    public IList<WayPoint> WayPoints { get; }

    public JnyFile(VehicleBase vehicle, IList<WayPoint> wayPoints)
    {
        Vehicle = vehicle ?? throw new Exception("Vehicle cannot be null for a JNY file.");
        WayPoints = wayPoints ?? throw new Exception("WayPoints cannot be null for a JNY file.");
    }

    public string GetJnyString()
    {
        var sb = new StringBuilder();

        sb.AppendLine(Vehicle.GetDetailsForJny());
        
        foreach (var wayPoint in WayPoints)
        {
            sb.AppendLine(wayPoint.GetDetailsForJny());
        }

        return sb.ToString();
    }
}