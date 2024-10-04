using Models.Elevators;

namespace Contollers.ElevatorControllers;

/// <summary>
/// This Controller can be seen as the board which controls all the elevators in the building. When constructed, it will take in a lsit of elevators
/// and initialise the board
/// </summary>
public class ElevatorControllers
{
    public ElevatorControllers(List<Elevator> elevators)
    {
            if(elevators.Count > 0)
            {
                
            }
    }
}