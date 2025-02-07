using System.Text;
using TravelEmulator.Data;

namespace TravelEmulator.Vehicles;

public class Boat : VehicleBase
{
    private readonly PowerTypes _power;
    public string Power => _power.ToString();
    public double Draft { get; private set; }
    public string Manufacturer { get; private set; }

    public Boat(string descriptor, double weight, double width, double height, double length, double speed, double maxTurning,
        double draft, string manufacturer, PowerTypes power) : 
        base("BOAT", descriptor, weight, width, height, length, speed, maxTurning)
    {
        _power = power;
        Draft = draft;
        Manufacturer = manufacturer;
    }

    public override string GetDetails()
    {
        return $"{base.GetDetails()},{Power},{Draft},{Manufacturer}";
    }

    public override bool CanNavigate(Coordinate coordinate)
    {
        //todo: only navigate in certain zones
        return true;
    }
}