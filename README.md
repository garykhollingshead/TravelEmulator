# TravelEmulator

Initial thoughts on the project:
- Need interface system for vehicles, probably just getDetails to get first line of JNY file and canNavigate to account for land/zones and size of vehicle.
- Need waypoint generator that needs to account for heading and zones.
- Need JNY file reader and generator that takes the vehicle's info as first line and waypoints as the rest

Implimentation order:
- Vehicles
  - data for the JNY file and how to write them, as well as 
  - navigation logic around boats
- Waypoints
  - data for the JNY file and how to write them
  - time traveled logic
- Generators
  - Waypoints
    - initial point with speed 0 and valid location for boats
    - next point with calculated speed and valid location for boats
  - Vehicles
    - random and parameterized inputs for creation
    - random data for description fields for creation
- CMD line arguements parsing
  - inline commands parsed to program directions
  - file read and parsed to program directions
- Core loop logic
  - get program settings
  - generate vehicle
  - generate waypoints
  - write JNY file
- Unit tests
  - waypoint logic
  - JNY writing logic
- Optimizations
- Documentation