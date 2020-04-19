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
    class FactLoader
    {
        string fact1 = "If Earth’s history is squeezed into one year, modern humanity has existed for 37 minutes and used up a third of Earth’s natural resources in the last 0.2 seconds";
        string fact2 = "Number of planet Earths we need to provide resources and absorb our waste is 1.75 right now";
        string fact3 = "Tons of CO2 emitted into the atmosphere in 2020 is 1,268,954,000";
        string fact4 = "Tons of ice melted globally in 2020 is 220,227,849,000";
        string fact5 = "Rise in sea level(cm) from 1900 until now 24.35. ⅓ has taken place in the last 25 years. Expected by the end of the century to reach 1.2 meters because of climate change";
        string fact6 = "Cost of not acting on climate change (US $) right now 15,646,428,760,000";
        string fact7 = "Word average temperature 14.88 in celsius";
        string fact8 = "Time left to the end of oil 47y 257d 11h 10m";
        string fact9 = "World population 7,816,981,000";
        string fact10 = "Time left until the end of rain forests 79y, 257d, 10h, 36m";
        string fact11 = "12 million people die every year from diseases caused by pollution, chemical exposures, climate change, and ultraviolet radiation";
        string fact12 = "From 1850 to 2019 2,400 gigatons of CO2 were emitted by human activity. Around 950 gigatons went into the atmosphere. The rest has been absorbed by oceans and land";
        string fact13 = "Cause of CO2 emissions\nCoal: 14.7 billion tons\nOil: 12.4 billion tons\nGas 7.5 billion tons";
        string fact14 = "The main CO2-emitting sectors\nElectricity and heat production: 49.0%\nTransport: 20.5%\nManufacturing & construction industries: 20.0%\nOther sectors: 10.5%";
        string fact15 = "Top five CO2- emitting countries (megatons)\nChina: 10065\nUSA: 5416\nIndia: 2654\nRussia: 1711\nJapan: 1162";
        string fact16 = "750 billion tons of ice is melting every year due to global warming";
        string fact17 = "From 1961 to 2016 nine trillion tons of ice have been lost from Earth’s glaciers due to global warming";
        string fact18 = "2100 will be the year that all rainforest will be gone if we keep cutting it down at the same pace";
        string fact19 = "About 360,000 babies are born each day according to the UN. That’s more than 130 million a year. At birth, many of them already show signs of chemical pollution";
        string fact20 = "Around 12 million people are diagnosed with cancer each year\nSmoking remains the greatest issue, however, over the last decade’s lung cancer among non-smokers have doubled";

        public string ChooseFact()
        {

            Random rand = new Random();
            int num = rand.Next(1, 21);

            switch (num) {

                case 1:
                    return fact1;
                case 2:
                    return fact2;
                case 3:
                    return fact3;
                case 4:
                    return fact4;
                case 5:
                    return fact5;
                case 6:
                    return fact6;
                case 7:
                    return fact7;
                case 8:
                    return fact8;
                case 9:
                    return fact9;
                case 10:
                    return fact10;
                case 11:
                    return fact11;
                case 12:
                    return fact12;
                case 13:
                    return fact13;
                case 14:
                    return fact14;
                case 15:
                    return fact15;
                case 16:
                    return fact16;
                case 17:
                    return fact17;
                case 18:
                    return fact18;
                case 19:
                    return fact19;
                case 20:
                    return fact20;
            }

            return null;

        }
    }
}