using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GamificationAppUnitTest
{

    [TestClass]
    public class XPTest
    {

        XPSystem xpSystem;

        [TestInitialize]
        public void Initialize() {

            xpSystem = new XPSystem(1,0,0,0);
        }

        [TestMethod]
        public void CalculateNeededXP() {

            xpSystem.XpToNextLevel();

            Assert.AreEqual(50, xpSystem.GetXpNeededToLevelUp);

        }

        [TestMethod]
        public void ConvertMeterToXP() {

            Assert.AreEqual(100, xpSystem.ConvertMetertoXp(1000));
        }

        [TestMethod]
        public void LevelUp() {

            Assert.AreEqual(0, xpSystem.GetCurrentExperience);


            xpSystem.LevelUp("Walking", 1000);

            Assert.AreEqual(2, xpSystem.GetCurrentLevel);
            Assert.AreEqual(50, xpSystem.GetRestXp);
        }

        [TestMethod]
        public void LevelUpMany()
        {

            xpSystem.LevelUp("Walking", 2000);

            Assert.AreEqual(3, xpSystem.GetCurrentLevel);

        }

        [TestMethod]
        public void RestXp()
        {

            xpSystem.LevelUp("Walking", 1900);

            Assert.AreEqual(40, xpSystem.GetRestXp);

        }

        [TestMethod]
        public void ConvertMeterToXPLvl2()
        {
            XPSystem xpSystem2 = new XPSystem(2,0,0,0);
            Assert.AreEqual(11.6, Math.Round(xpSystem2.ConvertMetertoXp(50),1));
        }
    }
}
