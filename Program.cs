using Contollers;
using Models.Elevators;


class Program
{   
    
    static void Main(string[] args)
    {
        #region init
        //Add the elevators you want to run into this list. 
        List<Elevator> initList = new List<Elevator>();
        initList.Add(new GlassElevator());
        initList.Add(new FreightElevator());
        initList.Add(new HighspeedElevator());
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

                elevatorController.updateElevators();
                Console.Clear();
                foreach(var elevator in elevatorController.ActiveElevators) 
                {
                    Console.WriteLine($"E1: \nFloor: {elevator.CurrentFloor} \nPassengers: {elevator.CurrentWeight}");
                }
                

                isUpdating = false;
            }
        }


        // Console input handling
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                isUpdating = true;
                Console.WriteLine("Input the current floor");
                int currentFloor;
                int.TryParse(Console.ReadLine(), out currentFloor);

                Console.WriteLine("Input the floor you would like to go to");
                int floor;
                int.TryParse(Console.ReadLine(), out floor);

                Console.WriteLine("Input the number of passengers");
                int passengers;
                int.TryParse(Console.ReadLine(), out passengers);

                if(floor != currentFloor)
                    elevatorController.CallElevator(new ElevatorOrders(floor, passengers), currentFloor);

                isUpdating = false;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                isUpdating = true;
                Console.WriteLine("Input the current floor");
                int currentFloor;
                int.TryParse(Console.ReadLine(), out currentFloor);

                Console.WriteLine("Input the floor you would like to go to");
                int floor;
                int.TryParse(Console.ReadLine(), out floor);

                Console.WriteLine("Input the number of passengers");
                int passengers;
                int.TryParse(Console.ReadLine(), out passengers);

                if (floor != currentFloor)
                    elevatorController.CallElevator(new ElevatorOrders(floor, passengers), currentFloor);

                isUpdating = false;
            }
        }
    }
}