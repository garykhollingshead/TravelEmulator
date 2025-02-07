using System.Text;
using TravelEmulator.Data;

namespace TravelEmulator.Vehicles;

public class Car : VehicleBase
{
    public string Manufacturer { get; }
    
    public uint ModelYear { get; }
    
    private readonly BodyStyleTypes _bodyStyle;
    public string BodyStyle => _bodyStyle.ToString();
    
    private readonly FuelTypes _fuel;
    public string Fuel => _fuel.ToString();

    public Car(string descriptor, double weight, double width, double height, double length, double speedInMph,
        string manufacturer, uint modelYear, BodyStyleTypes bodyStyle, FuelTypes fuel) : 
        base("CAR", descriptor, weight, width, height, length, speedInMph, 90)
    {
        _bodyStyle = bodyStyle;
        _fuel = fuel;
        Manufacturer = manufacturer;
        ModelYear = modelYear;
    }

    public Car(string descriptor, double weight, double width, double height, double length, string manufacturer, 
        uint modelYear, BodyStyleTypes bodyStyle, FuelTypes fuel) : this(descriptor, weight, width,
        height, length, 0, manufacturer, modelYear, bodyStyle, fuel)
    {
        SpeedInMph = RandomGenerator.Next(25, 60);
    }

    public override string GetDetailsForJny()
    {
        return $"{base.GetDetailsForJny()},{Manufacturer},{ModelYear},{BodyStyle},{Fuel}";
    }
}