using ElevatorDemo.Contollers;
using Models.Elevators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDemo.Presenters
{
    public class ElevatorControlBoard
    {
        public void DisplayBoard(List<Elevator> elevators) 
        {
            Console.Clear();
            foreach (var elevator in elevators)
            {
                Console.WriteLine($"E1: \nFloor: {elevator.CurrentFloor} \nPassengers: {elevator.CurrentWeight}");
            }
        }
    }
}
