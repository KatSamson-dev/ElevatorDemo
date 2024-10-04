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
                ActiveElevators = elevators;
            }
    }

    public List<Elevator> ActiveElevators;

    /// <summary>
    /// Description: The function iterates through the list of available elevators and updates their current position as well as removes orders. 
    /// This is contained within this domain as both the orders and the elevator movements are within scope of this controller.
    /// </summary>
    public void updateElevators()
    {
        //Only performing this function on elevators where orders exist for efficiency.
        foreach(var elevator in ActiveElevators.Where(x => x.Orders.Count > 0))
        {            
            if(elevator.IsMovingUp)
            {
                elevator.Orders.Sort();
                ElevatorOrders currentOrder = elevator.Orders.First(x => x.Floor >= elevator.CurrentFloor);

                if(currentOrder != null)
                {
                    if(currentOrder.Floor > elevator.CurrentFloor) 
                    {
                        elevator.CurrentFloor++ ;
                    }
                    else if(currentOrder.Floor == elevator.CurrentFloor) 
                    {
                        elevator.CurrentWeight = elevator.CurrentWeight - (currentOrder.Passengers*75);
                        elevator.Orders.Remove(currentOrder);
                    }
                    else
                    {
                        //Edge case, dont see it reaching here but usually I would do an analysis 
                    }
                }
                else
                {
                    //If it gets here, there is no order above the current floor of the elevator.
                    elevator.IsMovingUp = false;
                }
            }
            else
            {
                elevator.Orders.Sort();
                ElevatorOrders currentOrder = elevator.Orders.First(x => x.Floor <= elevator.CurrentFloor);

                if(currentOrder != null)
                {
                    if(currentOrder.Floor < elevator.CurrentFloor) 
                    {
                        elevator.CurrentFloor-- ;
                    }
                    else if(currentOrder.Floor == elevator.CurrentFloor) 
                    {
                        elevator.CurrentWeight = elevator.CurrentWeight - (currentOrder.Passengers*75);
                        elevator.Orders.Remove(currentOrder);
                    }
                    else
                    {
                        //Edge case, dont see it reaching here but usually I would do an analysis 
                    }
                }
                else
                {
                    //If it gets here, there is no order below the current floor of the elevator.
                    elevator.IsMovingUp = true;
                }
            }
        }
    }  

    /// <summary>
    /// Using a order, the function calculates the best and most efficient elevator to call and then puts that order into that elevators current order list
    /// 
    /// For the sake of this example, I am assuming that the elevator immediately picks up the passengers when called however in a more practical approach,
    /// I would consider factoring int he movements of the elevators so that actually passengers are picked up along the path and not by the closest elevator.
    /// 
    /// This approach would make more sense realistically and I would do it by adding a queue of new orders and then adding the order to the elevator that first reaches the list
    /// however this would not technically be the most efficient way.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    public Elevator CallElevator(ElevatorOrders newOrder)  
    {
        

        return new Elevator();
    }
}