##Project - ElevatorDemo
##Description
The following project is a POC demo to demonstrate technical ability and design thinking. The project includes a console application as a POC, a Unit
Testing addon, a folder of some system design example docs

The main console application includes the following functionality:

1. Class types for multiple elevators
2. A MVP style architecture that has specific controller to run the simulation and another to present it
3. The ability to update elevator movements incrementally as if acomodating for the real time simulation of movements. The elevator also includes an ordering
queue to offload passengers
4. An algorithm that calls an elevator to a specific floor based on real life parameters such as an elevator that can accomodate the new passengers,
moving in the direction of the calling floor and which is the closest

The Unit testing appication tests the Update and calling functionality of the main application.

Included is also a rough diagram to show how analytics could be gathered on the elevators and used to generate insights such as foot traffic in the building
and when to clean certain floors. The idea here is that the controller would access data coming from edge computers on the elevators and then transfer that data
to a server where building operations would be running. That data is then ingested to azure through a pipeline resulting in BI dashboards.

##Dependencies
No external dependencies are required to build the application however draw.io is required for system design files

