using TravelEmulator.Data;
using TravelEmulator.SNC;
using TravelEmulator.Vehicles;

namespace TravelEmulator.Generators;

public class WayPointGenerator
{
    private Random _randomGenerator;

    public WayPointGenerator(Random randomGenerator = null)
    {
        if (randomGenerator == null)
        {
            var seed = Convert.ToInt32(DateTime.Now.Ticks % int.MaxValue);
            randomGenerator = new Random(seed);
        }

        _randomGenerator = randomGenerator;
    }

    public WayPoint GetInitialWayPoint(VehicleBase vehicle)
    {
        if (vehicle is Boat boat)
        {
            return GetInitialWayPointForBoat(boat);
        }
        
        var latitude = _randomGenerator.NextDouble() * 180.0 - 90.0;
        var longitude = _randomGenerator.NextDouble() * 360.0 - 180.0;
        var heading = _randomGenerator.NextDouble() * 360.0;
        return new WayPoint(new Coordinate(latitude, longitude), 0, heading);
    }
    
    private WayPoint GetInitialWayPointForBoat(Boat boat)
    {
        var lowerLeftCoord = boat.GetLowerLeftTravelZoneCoordinate();
        var upperRightCoord = boat.GetUpperRightTravelZoneCoordinate();
        var latitudeRange = Math.Abs(upperRightCoord.Latitude - lowerLeftCoord.Latitude);
        var longitudeRange = Math.Abs(upperRightCoord.Longitude - lowerLeftCoord.Longitude);
        var latitude = _randomGenerator.NextDouble() * latitudeRange + lowerLeftCoord.Latitude;
        var longitude = _randomGenerator.NextDouble() * longitudeRange + lowerLeftCoord.Longitude;
        var heading = _randomGenerator.NextDouble() * 360.0;
        return new WayPoint(new Coordinate(latitude, longitude), 0, heading);
    }

    public WayPoint GetNextWayPoint(VehicleBase vehicle, WayPoint prevWayPoint, double maxSeconds = double.MaxValue)
    {
        var secondsOfTravel = _randomGenerator.NextDouble() * maxSeconds;

        Coordinate newCoords;
        double newHeading;
        double newLatitude;
        double newLongitude;
        
        do
        {
            var distanceFeet = vehicle.GetTravelDistanceInFeetFromSecondsTraveled(secondsOfTravel);
            
            var maxTurnAttempts = 5;
            while (maxTurnAttempts > 0)
            {
                maxTurnAttempts--;
                
                newHeading = vehicle.GetNextHeading(prevWayPoint.Heading);
                GeoCalc.GetEndingCoordinates(prevWayPoint.Latitude, prevWayPoint.Longitude, newHeading, distanceFeet, 
                    out newLatitude, out newLongitude);
                newCoords = new Coordinate(newLatitude, newLongitude);

                if (vehicle.CanNavigate(newCoords))
                {
                    return new WayPoint(newCoords, secondsOfTravel, newHeading);
                }
            }

            newHeading = vehicle.GetMaxLeftHeading(prevWayPoint.Heading);
            GeoCalc.GetEndingCoordinates(prevWayPoint.Latitude, prevWayPoint.Longitude, newHeading, distanceFeet, 
                out newLatitude, out newLongitude);
            newCoords = new Coordinate(newLatitude, newLongitude);
            if (vehicle.CanNavigate(newCoords))
            {
                return new WayPoint(newCoords, secondsOfTravel, newHeading);
            }

            newHeading = vehicle.GetMaxRightHeading(prevWayPoint.Heading);
            GeoCalc.GetEndingCoordinates(prevWayPoint.Latitude, prevWayPoint.Longitude, newHeading, distanceFeet, 
                out newLatitude, out newLongitude);
            newCoords = new Coordinate(newLatitude, newLongitude);
            if (vehicle.CanNavigate(newCoords))
            {
                return new WayPoint(newCoords, secondsOfTravel, newHeading);
            }

            secondsOfTravel /= 2;
        } while (secondsOfTravel > 0);

        throw new Exception("Unable to generate a WayPoint based on previous points.");
    }
}