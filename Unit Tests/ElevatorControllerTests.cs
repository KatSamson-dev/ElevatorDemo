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
        /// <summary>
        /// THe following test will check that the elevator positions will update on a unique set of elevators
        /// </summary>
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

        /// <summary>
        /// The following test will check that the elevator updates even when there are multiple types of elevators
        /// </summary>
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

        /// <summary>
        /// The following test checks to see that when all elevators are available, it can call for one to pick up passengers
        /// </summary>
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

        /// <summary>
        /// The following test checks that an elevator can be called based on which is the most optimum
        /// </summary>
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

        /// <summary>
        /// The following test checks that the system can accomodate when no elevators are available by returning a null value. This value is used in the program file
        /// to indicate none was available. 
        /// </summary>
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
