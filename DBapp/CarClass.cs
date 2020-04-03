using System.Collections.Generic;

namespace DBapp
{
    public class CarClass : CarInfo
    {
        private string carID;
        private string carName;
        private string carType;
        private string KMPerL;
        private string userID;

        public CarClass(string carName, string carType, string KMPerL, UserClass user) {
            this.carName = carName;
            this.carType = carType;
            this.KMPerL = KMPerL;
            userID = user.UserID;

            CarInsert(carType, carName, KMPerL, userID);

            this.carID = CarSelect("carID", "userID = '" + userID + "'")[0];
        }

        public void Delete() {

            CarDelete(this.carID);
        }

        public void Update(string column, string newValue) {

            // You cannot change the IDs 
            if (!column.Equals("carID") || !column.Equals("userID"))
            {
                // Update in database
                CarUpdate(column, newValue, this.carID);

                // Update in object
                if (column.Equals("carType")) { this.carType = newValue; }
                else if (column.Equals("KMPerL")) { this.KMPerL = newValue; }
            }
            // TODO: Might need an error handling statement here
            else { }
        }

        public List<string> Select(string column) {

            string presetCondition = "carID = " + this.carID;

            List<string> result = CarSelect(column, presetCondition);

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