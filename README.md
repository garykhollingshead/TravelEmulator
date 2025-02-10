# TravelEmulator
Simple DotNET 8 program

## Build Instructions:
`dotnet build TravelEmulator.sln`

## Executing Instructions
Can be executed from the commandline
`TravelEmulator.exe`

There are several flags that can be used:
- `TravelEmulator.exe -waypoints 5`
  - sets the number of waypoints to be generated
  - use values 10 to 30
- `TravelEmulator.exe -vehicle car`
  - sets the type of vehicle to generate
  - use values Car or Boat
- `TravelEmulator.exe -output file.jny`
  - sets the filename to output 
- `TravelEmulator.exe -maxSeconds 10`
  - sets the maximum seconds between waypoints
  - used with the speed of the boat to determine the distance
- `TravelEmulator.exe -settings settings.json`
  - specifies a JSON file to be read for settings

## Initial thoughts on the project:
- Need interface system for vehicles, probably just getDetails to get first line of JNY file and canNavigate to account for land/zones and size of vehicle.
- Need waypoint generator that needs to account for heading and zones.
- Need JNY file reader and generator that takes the vehicle's info as first line and waypoints as the rest

##Implimentation order:
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