using System.Text;
using TravelEmulator.Data;

namespace TravelEmulator.Vehicles;

public class Car : VehicleBase
{
    public string Manufacturer { get; private set; }
    public uint ModelYear { get; private set; }
    private readonly BodyStyleTypes _bodyStyle;
    public string BodyStyle => _bodyStyle.ToString();
    private readonly FuelTypes _fuel;
    public string Fuel => _fuel.ToString();

    public Car(string descriptor, double weight, double width, double height, double length, double speed, double maxTurning, 
        string manufacturer, uint modelYear, BodyStyleTypes bodyStyle, FuelTypes fuel) : 
        base("CAR", descriptor, weight, width, height, length, speed, maxTurning)
    {
        _bodyStyle = bodyStyle;
        _fuel = fuel;
        Manufacturer = manufacturer;
        ModelYear = modelYear;
    }

    public override string GetDetails()
    {
        return $"{base.GetDetails()},{Manufacturer},{ModelYear},{BodyStyle},{Fuel}";
    }
}