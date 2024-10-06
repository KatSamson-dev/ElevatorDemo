using Models.Elevators;

public class HighspeedElevator : Elevator
{
    public HighspeedElevator()
    {
        this.Type = ElevatorType.HighSpeed;
        this.Description = "This one goes really fast!";
        this.WeightLimit = 10;
        this.Orders = new List<ElevatorOrders>();
        this.IsMovingUp = true; //Elevator always starts on floor 0
    }    
}