using Newtonsoft.Json;

namespace DBapp
{
    public class UserClass : UserInfo
    {
        public string userID;
        public string userName;
        public string age;
        public string primaryTransportStart;
        public string primaryTransportCurrent;
        public string totalXP;

        public UserClass(string userName, string age, string primaryTransportCurrent) {

            this.userName = userName;
            this.age = age;
            primaryTransportStart = primaryTransportCurrent;
            this.primaryTransportCurrent = primaryTransportCurrent;
            totalXP = "0";

            userID = UserInsert(userName, age, primaryTransportStart, primaryTransportCurrent, totalXP);

        }

        [JsonConstructor]
        public UserClass() { }

        public void Update(string column, string newValue) {

            if (!column.Equals("userID")) {

                // Update in database
                UserUpdate(column, newValue, this.userID);

                // Update object
                if (column.Equals("userName")) { this.userName = newValue; }
                else if (column.Equals("age")) { this.age = newValue; }
                else if (column.Equals("primaryTransportStart")) { this.primaryTransportStart = newValue; }
                else if (column.Equals("primaryTransportCurrent")) { this.primaryTransportCurrent = newValue; }
                else if (column.Equals("totalXP")) { this.totalXP = newValue; }
            }
        }

        public string Select(string column) {

            string presetCondition = "userID = " + this.userID;

            string result = UserSelect(column, presetCondition);

            return result;
        }

        public void Delete() {

            UserDelete(this.userID);
        }

        public string UserID
        {
            get { return this.userID; }
        }

        public string UserName
        {
            get { return this.userName; }
        }

        public string UserAge
        {
            get { return this.age; }
        }

    }
}