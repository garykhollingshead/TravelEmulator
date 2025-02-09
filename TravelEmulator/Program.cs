using TravelEmulator.Data;
using TravelEmulator.FileOperations;
using TravelEmulator.Generators;
using TravelEmulator.Parsers;

namespace TravelEmulator;

class Program
{
    private static CmdLineParser _cmdLineParser = new CmdLineParser();
    private static VehicleGenerator _vehicleGenerator = new VehicleGenerator();
    private static WayPointGenerator _wayPointGenerator = new WayPointGenerator();
    
    static void Main(string[] args)
    {
        try
        {
            var settings = _cmdLineParser.GetSettings(args);

            var vehicle = _vehicleGenerator.GenerateRandom(settings.VehicleType);
            var waypoints = _wayPointGenerator.GetWayPoints(settings.NumberOfWaypoints, vehicle,
                settings.MaxSecondsBetweenWaypoints);

            var jnyFile = new JnyFile(vehicle, waypoints);
            File.WriteAllText(settings.OutputFile, jnyFile.GetJnyString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}