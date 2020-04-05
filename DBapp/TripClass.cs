using System;

namespace DBapp
{
    public class TripClass : TripInfo
    {
        private string tripID;
        private string distance;
        private string timeStamp;
        private string userID;
        private string otherTransport;
        private string carID;

        public TripClass(string distance, UserClass user, string otherTransport)
        {
            this.distance = distance;
            timeStamp = DateTime.Now.ToString();
            userID = user.UserID;
            this.otherTransport = otherTransport;

            tripID = TripInsert(distance, timeStamp, userID, otherTransport);
        }

        public TripClass(string distance, UserClass user, CarClass car)
        {
            this.distance = distance;
            timeStamp = DateTime.Now.ToString();
            userID = user.UserID;
            carID = car.CarID;

            tripID = TripInsert(distance, timeStamp, userID, carID);
        }

        public void Delete() {
            TripDelete(this.tripID);
        }

        public void Update(string column, string newValue) {

            // Update
            if (column.Equals("distance")) {

                TripUpdate(column, newValue, this.tripID);
                this.distance = newValue;
            }
            else if (column.Equals("otherTransport")) {

                TripUpdate(column, newValue, this.tripID);              
                this.otherTransport = newValue;

                if (carID != null) {
                    TripUpdate("carID", "", this.tripID);
                    this.carID = null;
                }                
            }
            else if (column.Equals("carID")) {
                
                TripUpdate(column, newValue, this.tripID);
                this.carID = newValue;

                if (otherTransport != null) {
                    TripUpdate("otherTransport", "", this.tripID);
                    this.otherTransport = null;
                }        
            }
            
        }

        public string Select(string column) {
        
            string presetCondition = "tripID = " + this.tripID;

            string result = TripSelect(column, presetCondition);

            return result;
        }

        public string TripID {
            get { return tripID; }
        }

        public string Distance
        {
            get { return distance; }
        }

        public string TimeStamp
        {
            get { return timeStamp; }
        }

        public string UserID
        {
            get { return userID; }
        }

        public string OtherTransport
        {
            get { return otherTransport; }
        }

        public string CarID
        {
            get { return carID; }
        }
    }
}