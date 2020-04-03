using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBapp;
using System.Data.SqlClient;

namespace DB_Unit_Test
{
    [TestClass]
    public class UserTest
    {
        string name = "John Doe";
        string age = "23";
        string primaryTransportStart = "Car";
        string primaryTransportCurrent = "Car";
        string totalXP = "0";
        UserClass user;

        [TestInitialize]
        public void TestInitialize()
        {
            user = new UserClass(name, age, primaryTransportStart, primaryTransportCurrent, totalXP);
        }

        [TestMethod]
        public void CreateUser()
        {
            Assert.AreEqual(name, user.Select("userName"));
            user.Delete();
        }

        [TestMethod]
        public void DeleteUser() {

            user.Delete();
            Assert.AreEqual("", user.Select("userName"));
        }

        [TestMethod]
        public void UpdateUser() {

            user.Update("primaryTransportCurrent", "Bike");
            Assert.AreEqual("Bike", user.Select("primaryTransportCurrent"));
            user.Delete();
        }
    }


    [TestClass]
    public class CarTest {
         
        string name = "John Doe";
        string age = "23";
        string primaryTransportStart = "Car";
        string primaryTransportCurrent = "Car";
        string totalXP = "0";
        UserClass user;
        CarClass car;

        [TestInitialize]
        public void TestInitialize() {

            user = new UserClass(name, age, primaryTransportStart, primaryTransportCurrent, totalXP);
            car = new CarClass("Benzin", "20.5", user);
        }

        [TestMethod]
        public void CreateCar() {

            Assert.AreEqual("Benzin", car.Select("carType"));

            car.Delete();
            user.Delete();
        }

        [TestMethod]
        public void DeleteCar() {

            car.Delete();

            Assert.AreEqual("", car.Select("carType"));

            user.Delete();
        }

        [TestMethod]
        public void UpdateCar() {

            car.Update("KMPerL", "25.60");

            // Remeber that it always ends with two decimals 
            Assert.AreEqual("25.60", car.Select("KMPerL"));

            car.Delete();
            user.Delete();
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException), "The INSERT statement conflicted with the CHECK constraint \"CK__carInfo__carType__5CD6CB2B\". The conflict occurred in database \"ProjectDB\", table \"dbo.carInfo\", column 'carType'. The statement has been terminated.")]
        public void FailCreate() {

            car.Delete();
            user.Delete();
            CarClass failCar = new CarClass("Water", "0", user);
        }

        [TestMethod]
        public void UserDeleted() {

            user.Delete();

            Assert.AreEqual("", car.Select("carType"));
        }
    }

    [TestClass]
    public class TripTest {

        string name = "John Doe";
        string age = "23";
        string primaryTransportStart = "Car";
        string primaryTransportCurrent = "Car";
        string totalXP = "0";
        UserClass user;
        CarClass car;

        [TestInitialize]
        public void TestInitialize() {

            user = new UserClass(name, age, primaryTransportStart, primaryTransportCurrent, totalXP);
            car = new CarClass("Benzin", "20.5", user);
        }

        [TestMethod]
        public void CreateTripCar() {

            TripClass trip = new TripClass("5000", user, car);

            Assert.AreEqual(car.CarID, trip.Select("carID"));

            trip.Delete();
            user.Delete();
        }

        [TestMethod]
        public void CreateTripOther() {

            TripClass trip = new TripClass("5000", user, "Bike");

            Assert.AreEqual("", trip.Select("carID"));

            trip.Delete();
            user.Delete();
        }

        [TestMethod]
        public void DeleteTrip() {

            TripClass trip = new TripClass("5000", user, car);

            trip.Delete();

            Assert.AreEqual("", trip.Select("tripID"));

            user.Delete();
        }

        [TestMethod]
        public void UpdateTripTransportCar()
        {

            TripClass trip = new TripClass("5000", user, "Bike");
            trip.Update("carID", car.CarID);

            Assert.AreEqual("", trip.Select("otherTransport"));

            trip.Delete();
            user.Delete();
        }

        [TestMethod]
        public void UpdateTripTransportOther() {

            TripClass trip = new TripClass("5000", user, car);
            trip.Update("otherTransport", "Bike");

            // Because carID is an INT it will show 0 if there is no ID connected to it
            Assert.AreEqual("0", trip.Select("carID"));

            trip.Delete();
            user.Delete();
        }
    }
}
