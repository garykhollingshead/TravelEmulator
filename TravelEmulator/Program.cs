using TravelEmulator.Data;
using TravelEmulator.Generators;
using TravelEmulator.Parsers;

namespace TravelEmulator;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var cmdLineParser = new CmdLineParser();
            var vehicleGenerator = new VehicleGenerator();
            var wayPointGenerator = new WayPointGenerator();
            var settings = cmdLineParser.GetSettings(args);

            var vehicle = vehicleGenerator.GenerateRandom(settings.VehicleType);
            var waypoints = wayPointGenerator.GetWayPoints(settings.NumberOfWaypoints, vehicle,
                settings.MaxSecondsBetweenWaypoints);

            var jnyGenerator = new JnyGenerator(vehicle, waypoints);
            File.WriteAllText(settings.OutputFile, jnyGenerator.GetJnyString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}