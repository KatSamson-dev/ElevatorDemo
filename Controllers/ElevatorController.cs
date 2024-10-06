using Models.Elevators;

namespace ElevatorDemo.Contollers;

/// <summary>
/// This Controller can be seen as the board which controls all the elevators in the building. When constructed, it will take in a lsit of elevators
/// and initialise the board
/// </summary>
public class ElevatorController
{
    public ElevatorController(List<Elevator> elevators)
    {
            if(elevators.Any())
            {
                ActiveElevators = elevators;
            }
            else
            {
                throw new Exception("No Elevators Could Be Found");
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
        foreach(var elevator in ActiveElevators.Where(x => x.Orders.Any()))
        {            
            if(elevator.IsMovingUp)
            {
                elevator.Orders.Sort();
                ElevatorOrders? currentOrder = elevator.Orders.FirstOrDefault(x => x.Floor >= elevator.CurrentFloor);

                if(currentOrder != null)
                {
                    if(currentOrder.Floor > elevator.CurrentFloor) 
                    {
                        elevator.CurrentFloor++;

                        if (currentOrder.Floor == elevator.CurrentFloor)
                        {
                            elevator.CurrentWeight = elevator.CurrentWeight - currentOrder.Passengers;
                            elevator.Orders.Remove(currentOrder);
                        }
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
                /*I am using orderby. If I was in a usecase where I was sorting a large list I would use a merge sort where I am splitting the list in halves and
                 and then recursively calling the list until the sublists are split down to one item and merging at the end of each function until back to the full list.
                 I would also make this more efficient by removing and rows where the floor is already greater in the order than the current floor.
                 The same would apply for the previous section but by removing where the current floor is already less
                  */
                
                elevator.Orders.OrderBy(x => x.Floor);
                ElevatorOrders? currentOrder = elevator.Orders.FirstOrDefault(x => x.Floor <= elevator.CurrentFloor);

                if(currentOrder != null)
                {
                    if(currentOrder.Floor < elevator.CurrentFloor) 
                    {
                        elevator.CurrentFloor-- ;

                        if (currentOrder.Floor == elevator.CurrentFloor)
                        {
                            elevator.CurrentWeight = elevator.CurrentWeight - currentOrder.Passengers;
                            elevator.Orders.Remove(currentOrder);
                        }
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
    /// Using a order and where the elevator is being called to, the function calculates the best and most efficient elevator to call and then 
    /// puts that order into that elevators current order list
    /// 
    /// In this implementation, I am assuming that when the call is made the most optimum elevator immediately goes to the floor where the order was made however
    /// This is unrealistic and does not accomodate for passengers who are on the elevator to get off. If I were to implement that, I would effectively be calling the
    /// updateElevators function above until the current floor equals the floor where the order was called.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>a potentially nullable elevator object indicating that an elevator ws sent to pick up the order if it is not null.</returns>
    public Elevator? CallElevator(ElevatorOrders newOrder, int currentPassengerFloor)  
    {
        Elevator? closestElevator = new Elevator();
        //First check if there are any Elevators currently with no orders and that can carry the weight.
        //Then select the closest lift
        var inActiveElevators = ActiveElevators.Where(x => !x.Orders.Any() && x.WeightLimit >= newOrder.Passengers);
        
        if(inActiveElevators.Any())
        {
            //Here we are selecting the closest elevator 
            closestElevator = inActiveElevators.OrderBy(x => Math.Abs(x.CurrentFloor - currentPassengerFloor)).FirstOrDefault();

            closestElevator.Orders.Add(newOrder);
            closestElevator.CurrentWeight = closestElevator.CurrentWeight + newOrder.Passengers;
            closestElevator.CurrentFloor = currentPassengerFloor;
            if (closestElevator.CurrentFloor < newOrder.Floor)
                closestElevator.IsMovingUp = true;
            else
                closestElevator.IsMovingUp = false;
        }
        else
        {
            //If we are in here, then there are no inactive elevators. First we get the lifts that can carry the weight
            var applicableElevators = ActiveElevators.Where(x => x.WeightLimit > x.CurrentWeight + newOrder.Passengers);

            //No available elevators that can carry load
            if(!applicableElevators.Any()) return null;

            //The logic here is we first order the list by the elevators that are closest to the order floor, we then select the first elevator that is already
            //moving in the direction of the floor
            closestElevator = applicableElevators.OrderBy(x => Math.Abs(x.CurrentFloor - currentPassengerFloor)).FirstOrDefault(
                y => (y.IsMovingUp && currentPassengerFloor > y.CurrentFloor) || (!y.IsMovingUp && currentPassengerFloor < y.CurrentFloor)
            );

            if(closestElevator == null)
            {
                //This is for the case where there are no elevators moving in the direction we want. Here I just choose the closest floor but in reality, there would
                //Be an algorithm to pause the order onboarding until an elevator is available. Because I am using linq I am also updating the object here which
                //points to the original list
                closestElevator = applicableElevators.OrderBy(x => Math.Abs(x.CurrentFloor - currentPassengerFloor)).FirstOrDefault();
            }

            //Throw an exception if we somehow cannot find an elevator
            if (closestElevator == null) throw new Exception("Elevator Could Not Be Found");

            closestElevator.Orders.Add(newOrder);
            closestElevator.CurrentWeight = closestElevator.CurrentWeight + newOrder.Passengers;
            closestElevator.CurrentFloor = currentPassengerFloor;            
        }

        return closestElevator;
    }
}