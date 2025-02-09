namespace TravelEmulator.Data;

public class Settings
{
    public int NumberOfWaypoints { get; set; }
    public double MaxSecondsBetweenWaypoints { get; set; }
    public VehicleTypes VehicleType { get; set; }
    public string OutputFile { get; set; }

    public Settings(int? numberOfWaypoints = null, double? maxSecondsBetweenWaypoints = null, VehicleTypes? vehicleType = null, string outputFile = null)
    {
        var randomGenerator = new Random();
        NumberOfWaypoints = numberOfWaypoints ?? randomGenerator.Next(10, 31);
        MaxSecondsBetweenWaypoints = maxSecondsBetweenWaypoints ?? randomGenerator.NextDouble() * 30 + 1;
        VehicleType = vehicleType ?? (randomGenerator.Next() % 2 == 1 ? VehicleTypes.Car : VehicleTypes.Boat);
        OutputFile = outputFile ?? $"TravelLog_{DateTime.Now.ToString("ddMMMMyyyy_HH-mm-ss")}.jny";
    }

    public static Settings GenerateFromFile(string settingsFile)
    {

        return new Settings();
    }
    
}