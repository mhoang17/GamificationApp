using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBapp;

namespace GamificationAppUnitTest
{
    [TestClass]
    public class UserClassTest
    {
        [TestMethod]
        public void CreateUser()
        {
            UserClass user = new UserClass("Maria", "21", "Bike");

            Assert.IsTrue(!user.UserID.Equals(""));

            user.Delete();
        }
    }


    [TestClass]
    public class CarClassTest
    {
        [TestMethod]
        public void CreateCar()
        {
            UserClass user = new UserClass("Maria", "21", "Car");
            CarClass car = new CarClass("Skoda", "Gasoline", "25.00", user);

            TripClass trip = new TripClass("1000", DateTime.Now.ToString(), user, car);

            Assert.IsTrue(!car.CarID.Equals(""));

            user.Delete();
        }
    }


}
