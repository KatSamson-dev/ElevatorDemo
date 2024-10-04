using System.Diagnostics.Contracts;

namespace Models.Elevators; 

/// <summary>
/// Placing one comment here rather than on the subtypes.
/// 
/// The Elevator class is for the elevators itself. I then created subtypes whuch can be constructed to create different kinds of lifts.
/// This can be expanded by just adding new subclasses. I also created a model which will be used to manage elevator orders and
/// a enum to manage the types.
/// 
/// I moved all the objects related to the elevators into its own namespace so that if a need for something arises outside of elevators
/// then I can use a separate namespace to import those.
/// </summary>
public class Elevator 
{
    public int ElevatorNumber;

    public ElevatorType Type;

    public string? Description;

    public int WeightLimit;

    public bool IsCalled;

    public int CurrentFloor;

    public List<ElevatorOrders> Orders;
}