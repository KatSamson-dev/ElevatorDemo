using Models.Elevator;

public class FreightElevator : Elevator
{
    public FreightElevator()
    {
        this.Type = ElevatorType.Freight;
        this.Description = "Elevator for moving extra heavy things";
        this.WeightLimit = 20;
    }    
}