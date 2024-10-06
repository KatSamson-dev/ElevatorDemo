using Models;
using Models.Elevators;

public class FreightElevator : Elevator
{
    public FreightElevator()
    {
        this.Type = ElevatorType.Freight;
        this.Description = "Elevator for moving extra heavy things";
        this.WeightLimit = 20;        
        this.Orders = new List<ElevatorOrders>();
        this.IsMovingUp = true; //Elevator always starts on floor 0
    }    
}