using System.Collections.Generic;
using Newtonsoft.Json;

namespace DBapp
{
    public class CarClass
    {
        public string carID;
        public string carName;
        public string carType;
        public string KMPerL;
        public string userID;
        private CarInfo carInfo = new CarInfo();

        public CarClass(string carName, string carType, string KMPerL, UserClass user) {
            this.carName = carName;
            this.carType = carType;
            this.KMPerL = KMPerL;
            userID = user.UserID;

            carID = carInfo.CarInsert(carType, carName, KMPerL, userID);
        }

        [JsonConstructor]
        public CarClass() { }

        public void Delete() {

            carInfo.CarDelete(this.carID);
        }

        public void Update(string column, string newValue) {

            // You cannot change the IDs 
            if (!column.Equals("carID") || !column.Equals("userID"))
            {
                // Update in database
                carInfo.CarUpdate(column, newValue, this.carID);

                // Update in object
                if (column.Equals("carType")) { this.carType = newValue; }
                else if (column.Equals("carName")) { this.carName = newValue; }
                else if (column.Equals("KMPerL")) { this.KMPerL = newValue; }
            }
            // TODO: Might need an error handling statement here
            else { }
        }

        public List<string> Select(string column) {

            string presetCondition = "carID = " + this.carID;

            List<string> result = carInfo.CarSelect(column, presetCondition);

            for(int i = 0; i <= result.Count; i++) {

                if (result[i].Contains(',')){

                    result[i].Replace(',','.');
                }
            }

            return result;
        }

        // Getters
        public string CarID {
            get { return this.carID; }
        }

        public string CarName
        {
            get { return this.carName; }
        }

        public string CarType {
            get { return this.carType; }
        }

        public string KMPerLProp
        {
            get { return this.KMPerL; }
        }

        public string UserID
        {
            get { return this.userID; }
        }
    }
}