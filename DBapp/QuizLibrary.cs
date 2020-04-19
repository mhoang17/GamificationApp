using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DBapp
{
    class QuizLibrary
    {

        List<string> quiz1;
        List<string> quiz2;
        List<string> quiz3;
        List<string> quiz4;
        List<string> quiz5;
        List<string> quiz6;
        List<string> quiz7;
        List<string> quiz8;
        List<string> quiz9;
        List<string> quiz10;
        List<string> quiz11;
        List<string> quiz12;
        List<string> quiz13;
        List<string> quiz14;
        List<string> quiz15;
        List<string> quiz16;
        List<string> quiz17;

        public QuizLibrary() {

            quiz1 = new List<string>();
            quiz2 = new List<string>();
            quiz3 = new List<string>();
            quiz4 = new List<string>();
            quiz5 = new List<string>();
            quiz6 = new List<string>();
            quiz7 = new List<string>();
            quiz8 = new List<string>();
            quiz9 = new List<string>();
            quiz10 = new List<string>();
            quiz11 = new List<string>();
            quiz12 = new List<string>();
            quiz13 = new List<string>();
            quiz14 = new List<string>();
            quiz15 = new List<string>();
            quiz16 = new List<string>();
            quiz17 = new List<string>();

            // Load Quizzes
            LoadQuiz1();
            LoadQuiz2();
            LoadQuiz3();
            LoadQuiz4();
            LoadQuiz5();
            LoadQuiz6();
            LoadQuiz7();
            LoadQuiz8();
            LoadQuiz9();
            LoadQuiz10();
            LoadQuiz11();
            LoadQuiz12();
            LoadQuiz13();
            LoadQuiz14();
            LoadQuiz15();
            LoadQuiz16();
            LoadQuiz17();

        }

        public List<string> RandomList() {

            Random random = new Random();
            int num = random.Next(1, 18);

            switch (num) {

                case 1:
                    return quiz1;
                case 2:
                    return quiz2;
                case 3:
                    return quiz3;
                case 4:
                    return quiz4;
                case 5:
                    return quiz5;
                case 6:
                    return quiz6;
                case 7:
                    return quiz7;
                case 8:
                    return quiz8;
                case 9:
                    return quiz9;
                case 10:
                    return quiz10;
                case 11:
                    return quiz11;
                case 12:
                    return quiz12;
                case 13:
                    return quiz13;
                case 14:
                    return quiz14;
                case 15:
                    return quiz15;
                case 16:
                    return quiz16;
                case 17:
                    return quiz17;
            }

            return null;
        }

        private void LoadQuiz1() {

            quiz1.Add("If Earth’s history is squeezed into one year, modern humanity has existed for 37 minutes. How much of the Earth’s natural resources have they used in the last 0.2 seconds?");
            quiz1.Add("1/3");
            quiz1.Add("2%");
            quiz1.Add("5%");
        }
        
        private void LoadQuiz2()
        {

            quiz2.Add("How many planet Earths do we need right now to provide resources and absorb our waste?");
            quiz2.Add("1.75");
            quiz2.Add("0.5");
            quiz2.Add("1");
        }
        private void LoadQuiz3()
        {

            quiz3.Add("How many tons of CO2 has been emitted into the atmosphere in 2020");
            quiz3.Add("Approximately 1,268,954,546");
            quiz3.Add("Approximately 5,456,653,872");
            quiz3.Add("Approximately 2,879,353,459");
        }
        private void LoadQuiz4()
        {

            quiz4.Add("How many tons of ice has been melted globally in 2020?");
            quiz4.Add("Approximately 220,227,849,560");
            quiz4.Add("Approximately 587,524,365,852");
            quiz4.Add("Approximately 500,400,600,000");
        }
        private void LoadQuiz5()
        {

            quiz5.Add("How much has the sea risen in cm since 1900 until now?");
            quiz5.Add("24.35");
            quiz5.Add("15");
            quiz5.Add("9");
        }
        private void LoadQuiz6()
        {

            quiz6.Add("What is the cost of not acting on climate change right now in USD?");
            quiz6.Add("Approximately 15,646,428,760,343");
            quiz6.Add("Approximately 1,245,543,879,175");
            quiz6.Add("Approximately 11,456,876,432,234");
        }
        private void LoadQuiz7()
        {

            quiz7.Add("How much is the time left until there is no more oil left?");
            quiz7.Add("Approximately 47 years");
            quiz7.Add("Approximately 80 years");
            quiz7.Add("Approximately 59 years");
        }
        private void LoadQuiz8()
        {

            quiz8.Add("What is the world population?");
            quiz8.Add("Approximately 7.8 billion");
            quiz8.Add("Approximately 6.4 billion");
            quiz8.Add("Approximately 4 billion");
        }
        private void LoadQuiz9()
        {

            quiz9.Add("How much time left until there are no more rain forests?");
            quiz9.Add("Approximately 79 years");
            quiz9.Add("Approximately 64 years");
            quiz9.Add("Approximately 100 years");
        }
        private void LoadQuiz10()
        {

            quiz10.Add("How many people die annually from diseases caused by pollution, chemical exposures, climate change, and ultraviolet radiation?");
            quiz10.Add("Approximately 12 million");
            quiz10.Add("Approximately 5 million");
            quiz10.Add("Approximately 2 million");
        }
        private void LoadQuiz11()
        {

            quiz11.Add("How many gigatons of CO2 were emitted by human activity from 1850 to 2019?");
            quiz11.Add("Approximately 2400 gigatons");
            quiz11.Add("Approximately 1200 gigatons");
            quiz11.Add("Approximately 1800 gigatons");
        }
        private void LoadQuiz12()
        {

            quiz12.Add("How much CO2 emission is caused by Coal?");
            quiz12.Add("Approximately 14.7 billion tons");
            quiz12.Add("Approximately 5.2 billion tons");
            quiz12.Add("Approximately 2.9 billion tons");
        }
        private void LoadQuiz13()
        {

            quiz13.Add("How much CO2 emission is caused by Oil?");
            quiz13.Add("Approximately 12.4 billion tons");
            quiz13.Add("Approximately 8.6 billion tons");
            quiz13.Add("Approximately 16.2 billion tons");
        }
        private void LoadQuiz14()
        {

            quiz14.Add("How much CO2 emission is caused by Gas?");
            quiz14.Add("Approximately 7.5 billion tons");
            quiz14.Add("Approximately 5.4 billion tons");
            quiz14.Add("Approximately 8.8 billion tons");
        }
        private void LoadQuiz15()
        {

            quiz15.Add("How much of the CO2 emission does the transport sector stand for?");
            quiz15.Add("Approximately 20.5%");
            quiz15.Add("Approximately 14.2%");
            quiz15.Add("Approximately 19.5%");
        }
        private void LoadQuiz16()
        {

            quiz16.Add("Which country emits the most CO2?");
            quiz16.Add("China");
            quiz16.Add("USA");
            quiz16.Add("India");
        }

        private void LoadQuiz17()
        {

            quiz17.Add("How much ice is melting every year due to global warming?");
            quiz17.Add("Approximately 750 billion");
            quiz17.Add("Approximately 640 billion");
            quiz17.Add("Approximately 980 billion");
        }
    }
}