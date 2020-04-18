using Android.OS;
using Android.Runtime;
using Java.Interop;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DBapp
{
    [Serializable]
    public class TripClass
    {
        public string tripID;
        public string distance;
        public string timeStamp;
        public string userID;
        public string otherTransport;
        public string carID;
        private TripInfo tripInfo = new TripInfo();


        public TripClass(string distance, string timeStamp, UserClass user, string otherTransport)
        {
            this.distance = distance;
            this.timeStamp = timeStamp;
            userID = user.UserID;
            this.otherTransport = otherTransport;

            tripID = tripInfo.TripInsert(distance, timeStamp, userID, otherTransport);

            MainActivity.GetInstance.GetXPSystem.LevelUp(otherTransport, Convert.ToInt32(Math.Floor(Double.Parse(distance))));
        }

        public TripClass(string distance, string timeStamp, UserClass user, CarClass car)
        {
            this.distance = distance;
            this.timeStamp = timeStamp;
            userID = user.UserID;
            carID = car.CarID;

            tripID = tripInfo.TripInsert(distance, timeStamp, userID, carID);
        }
       
        [JsonConstructor]
        public TripClass()
        {
        }

        public void Delete() {
            tripInfo.TripDelete(this.tripID);
        }

        public void Update(string column, string newValue) {

            // Update
            if (column.Equals("distance"))
            {

                tripInfo.TripUpdate(column, newValue, this.tripID);
                this.distance = newValue;
            }
            else if (column.Equals("otherTransport"))
            {

                tripInfo.TripUpdate(column, newValue, this.tripID);
                this.otherTransport = newValue;

                if (carID != null)
                {
                    tripInfo.TripUpdate("carID", "", this.tripID);
                    this.carID = null;
                }
            }
            else if (column.Equals("carID"))
            {

                tripInfo.TripUpdate(column, newValue, this.tripID);
                this.carID = newValue;

                if (otherTransport != null)
                {
                    tripInfo.TripUpdate("otherTransport", "", this.tripID);
                    this.otherTransport = null;
                }
            }
            else if (column.Equals("timeStamp")) {

                tripInfo.TripUpdate(column, newValue, this.tripID);
                this.timeStamp = newValue;
            }
            
        }

        public string Select(string column) {
        
            string presetCondition = "tripID = " + this.tripID;

            string result = tripInfo.TripSelect(column, presetCondition);

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