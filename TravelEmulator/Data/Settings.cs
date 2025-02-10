using System.Text.Json;
using System.Text.Json.Serialization;
using TravelEmulator.Generators;

namespace TravelEmulator.Data;

public class Settings
{
    public int NumberOfWaypoints { get; set; }
    
    public double MaxSecondsBetweenWaypoints { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VehicleTypes VehicleType { get; set; }
    
    public string OutputFile { get; set; }

    public Settings()
    {
        
    }
    
    public Settings(int? numberOfWaypoints = null, double? maxSecondsBetweenWaypoints = null, VehicleTypes? vehicleType = null, string outputFile = null)
    {
        var randomGenerator = RandomGenerator.Generator;
        NumberOfWaypoints = numberOfWaypoints ?? randomGenerator.Next(10, 31);
        MaxSecondsBetweenWaypoints = maxSecondsBetweenWaypoints ?? randomGenerator.NextDouble() * 30 + 1;
        VehicleType = vehicleType ?? (randomGenerator.Next() % 2 == 1 ? VehicleTypes.Car : VehicleTypes.Boat);
        OutputFile = outputFile ?? $"TravelLog_{DateTime.Now:ddMMMMyyyy_HH-mm-ss}.jny";
    }

    public static Settings GenerateFromFile(string settingsFile)
    {
        string settingsJson;
        try
        {
            settingsJson = string.Join("", File.ReadAllLines(settingsFile));
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to read settings file from '{settingsFile}'. {e.Message}");
        }

        Settings settings;
        try
        {
            var options = new JsonSerializerOptions{Converters = { new JsonStringEnumConverter() }};
            settings = JsonSerializer.Deserialize<Settings>(settingsJson, options);
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to parse Settings file. Not in JSON format. {e.Message}");
        }
        
        return settings;
    }
    
}