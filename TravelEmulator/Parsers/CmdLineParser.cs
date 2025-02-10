using TravelEmulator.Data;

namespace TravelEmulator.Parsers;

public class CmdLineParser
{
    private const string NumberOfWaypointsFlag = "-waypoints";
    private const string VehicleTypeFlag = "-vehicle";
    private const string OutputFileFlag = "-output";
    private const string MaxSecondsBetweenWaypointsFlag = "-maxSeconds";
    private const string SettingsFlag = "-settings";
    
    public Settings GetSettings(string[] args)
    {
        int? numberOfWaypoints = null;
        double? maxSecondsBetweenWaypoints = null;
        VehicleTypes? vehicleType = null;
        string outputFile = null;

        for (var i = 0; i < args.Length - 1; i += 2)
        {
            switch (args[i])
            {
                case SettingsFlag:
                    return Settings.GenerateFromFile(args[i+1]);
                case NumberOfWaypointsFlag:
                    if (int.TryParse(args[i + 1], out var parsedNumberOfWaypoints))
                    {
                        if (parsedNumberOfWaypoints < 10 || parsedNumberOfWaypoints > 30)
                        {
                            throw new Exception("Number of waypoints out of range. Select between 10 and 30");
                        }
                        
                        numberOfWaypoints = parsedNumberOfWaypoints;
                        continue;
                    }

                    throw new Exception($"Not able to set number of waypoints: {args[i]} {args[i+1]}");
                case MaxSecondsBetweenWaypointsFlag:
                    if (double.TryParse(args[i + 1], out var parsedMaxSecondsBetweenWaypoints))
                    {
                        if (parsedMaxSecondsBetweenWaypoints < 0.1)
                        {
                            throw new Exception("Max seconds between waypoints out of range. Select value greater than 0.1");
                        }
                        
                        maxSecondsBetweenWaypoints = parsedMaxSecondsBetweenWaypoints;
                        continue;
                    }

                    throw new Exception($"Not able to set number of waypoints: {args[i]} {args[i+1]}");
                case VehicleTypeFlag:
                    if (Enum.TryParse<VehicleTypes>(args[i + 1], out var parsedVehicleType))
                    {
                        vehicleType = parsedVehicleType;
                        continue;
                    }

                    throw new Exception($"Not able to set vehicle type: {args[i]} {args[i+1]}.{Environment.NewLine}Please choose 'Car' or 'Boat'.");
                case OutputFileFlag:
                    var proposedOutputFile = args[i + 1];

                    if (proposedOutputFile.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
                    {
                        throw new Exception($"Invalid filename: '{proposedOutputFile}'");
                    }
                    
                    if (!File.Exists(proposedOutputFile))
                    {
                        outputFile = proposedOutputFile;
                        continue;
                    }

                    throw new Exception($"Not able to set number of waypoints: {args[i]} {args[i+1]}");
            }
        }
        
        return new Settings(numberOfWaypoints, maxSecondsBetweenWaypoints, vehicleType, outputFile);
    }
}