using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDemo.Contollers;
using Models.Elevators;

namespace Unit_Tests
{
    [TestClass]    
    
    public class ElevatorControllerTests
    {
        [TestMethod]
        public void ElevatorControllerUpdatesUnique_tests()
        {
            #region init                        
            List<Elevator> initList = new List<Elevator>()
            {
                new GlassElevator(),
                new FreightElevator(),
                new HighspeedElevator()
            };

            ElevatorController _elevatorController = new ElevatorController(initList);
            #endregion

            foreach (var elevator in _elevatorController.ActiveElevators) 
            {
                Random random = new Random();
                elevator.Orders.Add(new ElevatorOrders(1, 1));
            }

            _elevatorController.updateElevators();

            //Test whether there are any elevators with orders
            Assert.IsFalse(_elevatorController.ActiveElevators.Any(x => x.Orders.Any()));

        }

        [TestMethod]
        public void ElevatorControllerUpdatesDuplicates_tests()
        {
            #region init                        
            List<Elevator> initList = new List<Elevator>()
            {
                new GlassElevator(),
                new FreightElevator(),
                new HighspeedElevator(),
                new GlassElevator()
            };

            ElevatorController _elevatorController = new ElevatorController(initList);
            #endregion

            foreach (var elevator in _elevatorController.ActiveElevators)
            {
                Random random = new Random();
                elevator.Orders.Add(new ElevatorOrders(1, 1));
            }

            _elevatorController.updateElevators();

            //Test whether there are any elevators with orders
            Assert.IsFalse(_elevatorController.ActiveElevators.Any(x => x.Orders.Any()));
        }

        [TestMethod]
        public void AddElevatorOrderAllAvailable_tests() 
        {
            #region init                        
            List<Elevator> initList = new List<Elevator>()
            {
                new GlassElevator(),
                new FreightElevator(),
                new HighspeedElevator(),
                new GlassElevator()
            };

            ElevatorController _elevatorController = new ElevatorController(initList);
            #endregion

            Random random = new Random();

            Elevator? elevator = _elevatorController.CallElevator(new ElevatorOrders(random.Next(1, 10), random.Next(1, 10)), random.Next(1, 10));

            Assert.IsNotNull(elevator);
        }

        [TestMethod]
        public void AddElevatorOrder_tests()
        {
            #region init                        
            List<Elevator> initList = new List<Elevator>()
            {
                new GlassElevator(),
                new FreightElevator(),
                new HighspeedElevator(),
                new GlassElevator()
            };

            ElevatorController _elevatorController = new ElevatorController(initList);

            Random random = new Random();
            foreach (var elevator in _elevatorController.ActiveElevators)
            {
                elevator.CurrentWeight = random.Next(1, elevator.WeightLimit);
                elevator.CurrentFloor = random.Next(1, 10);
            }
            #endregion

            Elevator? calledElevator = _elevatorController.CallElevator(new ElevatorOrders(random.Next(1, 10), random.Next(1, 10)), random.Next(1, 10));

            Assert.IsNotNull(calledElevator);
        }

        [TestMethod]
        public void AddElevatorOrderNoneAvailable_tests()
        {
            #region init                        
            List<Elevator> initList = new List<Elevator>()
            {
                new GlassElevator(),
                new FreightElevator(),
                new HighspeedElevator(),
                new GlassElevator()
            };

            ElevatorController _elevatorController = new ElevatorController(initList);

            Random random = new Random();
            foreach (var elevator in _elevatorController.ActiveElevators)
            {
                elevator.CurrentWeight = 1;
                elevator.WeightLimit = 2;
                elevator.CurrentFloor = random.Next(1, 10);
            }
            #endregion

            Elevator? calledElevator = _elevatorController.CallElevator(new ElevatorOrders(random.Next(1, 10), 100), random.Next(1, 10));

            Assert.IsNull(calledElevator);
        }
    }
}
