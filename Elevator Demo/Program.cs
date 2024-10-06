using ElevatorDemo.Contollers;
using ElevatorDemo.Presenters;
using Models.Elevators;


class Program
{

    static void Main(string[] args)
    {
        #region init
        ElevatorControlBoard elevatorControlBoard = new ElevatorControlBoard();
        //Add the elevators you want to run into this list.
        List<Elevator> initList = new List<Elevator>();
        initList.Add(new GlassElevator());
        initList.Add(new FreightElevator());
        initList.Add(new HighspeedElevator());
        initList.Add(new GlassElevator());

        //This block is to make it easier to demo the program
        Random random = new Random();

        foreach (var elevator in initList)
        {
            for (int i = 0; i < random.Next(1, 2); i++)
            {
                int passenger = random.Next(1, 5);
                elevator.Orders.Add(new ElevatorOrders(random.Next(1, 10), passenger));
                elevator.CurrentWeight = elevator.CurrentWeight + passenger;
            }
        }
        #endregion

        ElevatorController elevatorController = new ElevatorController(initList);

        Timer updateTimer = null;
        bool isUpdating = false;
        AutoResetEvent pauseEvent = new AutoResetEvent(false);

        // Start the update timer
        updateTimer = new Timer(UpdateElevators, null, 2000, 2000);

        void UpdateElevators(object state)
        {
            if (!isUpdating)
            {
                isUpdating = true;

                try
                {
                    elevatorController.updateElevators();
                }
                catch (Exception e)
                {
                    //In a real world scenario, the correct thing to do here would be to report the error to a control board as mentioned in the design spec
                    Console.WriteLine($"{e.Message}");
                }

                //I moved this logic into a view layer to give the idea of a MVC design pattern
                elevatorControlBoard.DisplayBoard(elevatorController.ActiveElevators);

                isUpdating = false;
            }
        }


        // Console input handling
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow)
            {
                isUpdating = true;
                //Ideally I would refactor the code below into the view layer but leaving for now
                Console.WriteLine("Input the current floor");
                int currentFloor;
                int.TryParse(Console.ReadLine(), out currentFloor);

                Console.WriteLine("Input the floor you would like to go to");
                int floor;
                int.TryParse(Console.ReadLine(), out floor);

                Console.WriteLine("Input the number of passengers");
                int passengers;
                int.TryParse(Console.ReadLine(), out passengers);

                try
                {
                    if (floor != currentFloor)
                    {
                        var elevator = elevatorController.CallElevator(new ElevatorOrders(floor, passengers), currentFloor);
                        elevatorControlBoard.DisplayBoard(elevatorController.ActiveElevators);
                        if(elevator == null) Console.WriteLine("No Available Elevators");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }

                isUpdating = false;
            }            
        }
    }
}