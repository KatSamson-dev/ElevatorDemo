using Models.Elevator;

public class HighspeedElevator : Elevator
{
    public HighspeedElevator()
    {
        this.Type = ElevatorType.HighSpeed;
        this.Description = "This one goes really fast!";
        this.WeightLimit = 10;
    }    
}