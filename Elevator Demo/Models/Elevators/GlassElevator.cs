using Models.Elevators;

public class GlassElevator : Elevator
{
    public GlassElevator()
    {
        this.Type = ElevatorType.Glass;
        this.Description = "A clear (not bullet proof) elevator!";
        this.WeightLimit = 5;
        this.Orders = new List<ElevatorOrders>();
        this.IsMovingUp = true; //Elevator always starts on floor 0
    }    
}