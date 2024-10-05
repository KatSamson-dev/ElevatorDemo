namespace Models.Elevators;

public class ElevatorOrders
{

    public ElevatorOrders(int floor, int passengers){
        Floor = floor;
        Passengers = passengers;
    }
    
    public int Floor;

    public int Passengers;
}