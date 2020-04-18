using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DBapp
{
    public class XPSystem
    {
        public int currentLevel;
        public double xpNeededToLevelUp;
        public double xpReceivedFromTrip;
        public double totalXp;
        public double currentExperience;
        private int maxLevel = 50;
        public double restXp;

        public XPSystem(int currentLevel, double totalXp, double currentExperience, double restXp)
        {
            this.currentLevel = currentLevel;
            this.totalXp = totalXp;
            this.currentExperience = currentExperience;
            this.restXp = restXp;

            XpToNextLevel();
        }

        [JsonConstructor]
        public XPSystem() { }

        public int GetCurrentLevel
        {
            get { return currentLevel; }
        }

        public double GetXpReceivedFromTrip
        {
            get { return xpReceivedFromTrip; }
        }

        public double GetTotalXp
        {
            get { return totalXp; }
        }

        public double GetCurrentExperience
        {
            get { return currentExperience; }
        }

        public double GetRestXp
        {
            get { return restXp; }
        }

        public double GetXpNeededToLevelUp
        {
            get { return xpNeededToLevelUp; }
        }

        // Simple method experience needed to level up.
        public void XpToNextLevel()
        {
            xpNeededToLevelUp = 25 * currentLevel * (1 + currentLevel);
        }

        public double ConvertMetertoXp(double meter)
        {

            // Metodekald
            double xpPerMeter = (Math.Log(currentLevel) / 3);

            if (currentLevel == 1)
            {

                xpPerMeter = 0.1;
            }

            double newXP = meter * xpPerMeter;

            return newXP;
        }

        /* Method for leveling up. There are three cases. 
		 * First case: If a person does not receive enough experience to level up his current experience will simply increase. 
		 * Second case: If the person receives exactly enough experience to level up, he will and his current xp will reset.
		 * Third case: If the person receives more xp than what is needed this person will level up. Besides leveling up
		 * the person will also be receiving further experience in his next level. For example if he needs 80 experience
		 * however he receives 100 experience, he will level up to the next level and start with 20 experience. To do this
		 * a supporting method is used called "CalcRestXp".
		 */
        public void LevelUp(string type, int meters) // Testen giver ballade hvis denne er void og ikke int - Pga. Parametrene
        {
            XpMultiplier(type, meters);
            // Total xp for current after a trip
            double XPAfterTrip = currentExperience + xpReceivedFromTrip;

            if (XPAfterTrip < xpNeededToLevelUp)
            {
                currentExperience += xpReceivedFromTrip;
            }
            else if (XPAfterTrip >= xpNeededToLevelUp)
            {
                restXp = CalcRestXp();
                currentLevel++;
                XpToNextLevel();

                while (restXp >= xpNeededToLevelUp)
                {

                    currentLevel++;
                    restXp = restXp - xpNeededToLevelUp;
                    XpToNextLevel();
                }

                currentExperience = restXp;
            }

            CalcTotalXp();
        }

        // Helper method to calculate the experience that exceeded when leveling up. Used in "LevelUp" method.
        public double CalcRestXp()
        {

            double restXp = (currentExperience + xpReceivedFromTrip) - xpNeededToLevelUp;

            return restXp;

        }

        // Simple method to calculate a players total experience as long as they have not reached the max level.
        public void CalcTotalXp()
        {
            if (currentLevel < maxLevel)
            {
                totalXp += xpReceivedFromTrip;
            }
        }


        // Function to ensure that bicycle and walk can get a multiplier on the value 
        public void XpMultiplier(string type, int meters)
        {

            // 1 = Bike, 2 = Walk
            if (type.Equals("Walking") || type.Equals("Bike"))
            {
                double xpReceived = ConvertMetertoXp(meters);

                if (meters >= 5000)
                {
                    xpReceivedFromTrip = xpReceived * 2;
                }
                else if (meters >= 3500)
                {
                    xpReceivedFromTrip = xpReceived * 1.75;
                }
                else if (meters >= 2000)
                {
                    xpReceivedFromTrip = xpReceived * 1.25;
                }
                else
                {
                    xpReceivedFromTrip = xpReceived;
                }
            }
            // Bus
            else if (type.Equals("Bus"))
            {

                if (meters <= 2000)
                {
                    double xpReceived = ConvertMetertoXp(meters) / 4;
                    xpReceivedFromTrip = xpReceived;
                }
                else
                {

                    double xpReceived = ConvertMetertoXp(2000) / 4;
                    xpReceivedFromTrip = xpReceived;
                }
            }
        }
    }
}