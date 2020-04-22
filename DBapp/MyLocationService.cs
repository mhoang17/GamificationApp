using System;
using System.Text;
using Xamarin.Essentials;
using Android.Content;
using Android.Gms.Location;
using Android.Widget;


namespace DBapp
{
    [BroadcastReceiver]
    public class MyLocationService : BroadcastReceiver
    {
        public static string ACTION_PROCESS_LOCATION = "TestRun.UPDATE_LOCATION";
        public override void OnReceive(Context context, Intent intent)
        {

            if(intent != null)
            {
                string action = intent.Action;
                if (action.Equals(ACTION_PROCESS_LOCATION))
                {
                    LocationResult result = LocationResult.ExtractResult(intent);
                    if(result != null)
                    {
                        var location = result.LastLocation;
                        var speedkmh = location.Speed * 3.6;

                        Preferences.Set("newLong", location.Longitude);
                        Preferences.Set("newLat", location.Latitude);


                        //Preferences.Get("lastCalmLatitude", location.Latitude);
                        //Preferences.Get("lastCalmLongitude", location.Longitude);
                        if (Preferences.Get("setFirstCompared", 0) == 0)
                        {
                            Preferences.Set("setFirstCompared", 1);
                            Preferences.Set("lastComparedLatitude", location.Latitude);
                            Preferences.Set("lastComparedLongitude", location.Longitude);
                        }

                        if (Preferences.Get("updateFive", 10) == 0){

                            if (isPositionChanged(Preferences.Get("newLat", location.Latitude)
                                , Preferences.Get("newLong", location.Longitude)
                                , Preferences.Get("lastComparedLatitude", location.Latitude)
                                , Preferences.Get("lastComparedLongitude", location.Longitude)))
                            {
                                if (Preferences.Get("setCalmPostion", 0) == 0)
                                {
                                    Preferences.Set("setCalmPosition", 1);
                                    Preferences.Get("lastCalmLatitude", Preferences.Get("lastComparedLatitude", location.Latitude));
                                    Preferences.Get("lastCalmLongitude", Preferences.Get("lastComparedLongitude", location.Longitude));
                                }
                                Preferences.Set("isTrip", true);
                                Preferences.Set("lastComparedLatitude", location.Latitude);
                                Preferences.Set("lastComparedLongitude", location.Longitude);
                            }

                            else if (Preferences.Get("isTrip", false))
                            {
                                if (!isPositionChanged(Preferences.Get("newLat", location.Latitude)
                                , Preferences.Get("newLong", location.Longitude)
                                , Preferences.Get("lastCalmLatitude", Preferences.Get("newLat", location.Latitude))
                                , Preferences.Get("lastCalmLongitude", Preferences.Get("newLong", location.Longitude))))
                                {
                                    resetVaribles(location.Latitude, location.Longitude);

                                    Console.WriteLine("Reset");
                                    Toast.MakeText(context, "Trip is deleted", ToastLength.Short).Show();

                                }
                                else
                                {
                                    Console.WriteLine("Trip saved");
                                    Console.WriteLine(Preferences.Get("vehicle", "car"));
                                    double distance = Math.Round(Preferences.Get("distance", 0.0), 2);
                                    TripClass newTrip = null;
                                    string tripName = "";

                                    // If preferences says that they are walking or riding a bike then we save the trip as that
                                    if (Preferences.Get("vehicle", "car").Equals("Walking") || Preferences.Get("vehicle", "car").Equals("Bike"))
                                    {
                                        newTrip =  new TripClass(distance.ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, Preferences.Get("vehicle", "car"));
                                        tripName = newTrip.TimeStamp + " " + newTrip.OtherTransport + " " + newTrip.Distance;
                                    }
                                    else
                                    {   
                                        // If we can see that they have chosen bus in their profile, then we safe the trip as them riding the bus
                                        if (MainActivity.GetInstance.GetTransport.Equals("Bus"))
                                        {
                                            newTrip = new TripClass(distance.ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, Preferences.Get("vehicle", "car"));
                                            tripName = newTrip.TimeStamp + " " + newTrip.OtherTransport + " " + newTrip.Distance;
                                        }
                                        // If we can see that their chosen transport is neither walking or bike, then we will find the car they have chosen and save it to the trip
                                        else if (!MainActivity.GetInstance.GetTransport.Equals("Walking") && !MainActivity.GetInstance.GetTransport.Equals("Bike") && MainActivity.GetInstance.GetCarList.Count != 0)
                                        {
                                            foreach (CarClass car in MainActivity.GetInstance.GetCarList)
                                            {
                                                if (car.CarName.Equals(MainActivity.GetInstance.GetTransport))
                                                {
                                                    newTrip = new TripClass(distance.ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, car);
                                                    tripName = newTrip.TimeStamp + " " + car.CarName + " " + newTrip.Distance;
                                                    break;
                                                }
                                            }
                                        }
                                        // If they have chosen walking or bike as their primary transport,
                                        // but we have tracked that they are driving then we will either set their transport to be their first registeret car
                                        // and if they have not registeret a car before, we assume that they have been taking a bus
                                        else
                                        {
                                            if (MainActivity.GetInstance.GetCarList.Count != 0)
                                            {
                                                newTrip = new TripClass(distance.ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, MainActivity.GetInstance.GetCarList[0]);
                                                tripName = newTrip.TimeStamp + " " + MainActivity.GetInstance.GetCarList[0].CarName + " " + newTrip.Distance;
                                            }
                                            else {

                                                newTrip = new TripClass(distance.ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, "Bus");
                                                tripName = newTrip.TimeStamp + " " + newTrip.OtherTransport + " " + newTrip.Distance;
                                            }
                                        }
                                    }

                                    // Give Quiz
                                    MainActivity.GetInstance.TriggerQuiz();

                                    Console.WriteLine("Happens");

                                    // Give Fact
                                    MainActivity.GetInstance.ShowFact();

                                    Console.WriteLine("Happens");

                                    // Level up
                                    MainActivity.GetInstance.XPLevelUp();

                                    Console.WriteLine("Happens");

                                    // Add the new trip to the UI for later editing
                                    MainActivity.GetInstance.AddTripToUI(newTrip, tripName);

                                    // Print out the toast and reset
                                    Toast.MakeText(context, "Trip is saved", ToastLength.Short).Show();
                                    resetVaribles(location.Latitude, location.Longitude);

                                }
                            }
                        }

                        
                        if (Preferences.Get("updateFive", 10) == 0)
                        {
                            Preferences.Set("updateFive", 10);
                        }
                        else { Preferences.Set("updateFive", Preferences.Get("updateFive", 5) - 1); }
                        

                        if (Preferences.Get("isTrip", false))
                        {
                            if (Preferences.Get("isWalking", 0) == 0)
                            {
                                Preferences.Set("updateFive", 100);
                                Preferences.Set("isWalking", 1);
                                Preferences.Set("vehicle", "Walking");
                            }

                            if (speedkmh >= 12 && Preferences.Get("isBiking", 0) == 0)
                            {
                                Preferences.Set("vehicle", "Bike");
                                Preferences.Set("updateFive", 20);
                                Preferences.Set("isBiking", 1);
                            }

                            if ((speedkmh > 45) && Preferences.Get("isDriving", 0) == 0)
                            {
                                Preferences.Set("vehicle", "Bus");
                                Preferences.Set("updateFive", 10);
                                Preferences.Set("isDriving", 1);
                            }



                            Console.WriteLine(Preferences.Get("distance", 0.0));
                            var temp = CalculateDistance(Preferences.Get("newLat", location.Latitude)
                             , Preferences.Get("newLong", location.Longitude)
                             , Preferences.Get("oldLat", location.Latitude)
                             , Preferences.Get("oldLong", location.Longitude)) + Preferences.Get("distance", 0.0);

                            Preferences.Set("distance", temp);

                            Console.WriteLine("Dist: " + Preferences.Get("distance", 0.0));

                            Console.WriteLine("Trip Status: " + Preferences.Get("isTrip", false));

                            Console.WriteLine("Speed: " + speedkmh);
                        }
                    
                        Preferences.Set("oldLong", Preferences.Get("newLong", location.Longitude));
                        Preferences.Set("oldLat", Preferences.Get("newLat", location.Latitude));

                    }
                }
            }
        }

        private double GetDistance()
        {
            return Preferences.Get("distance", 0.0);
        }

        private void resetVaribles(double latitude, double longitude)
        {
            Console.WriteLine("Reset variables");
            Preferences.Set("updateFive", 10);
            Preferences.Set("isWalking", 0);
            Preferences.Set("isBiking", 0);
            Preferences.Set("isDriving", 0);
            Preferences.Set("isTrip", false);
            Preferences.Set("lastCalmLatitude", latitude);
            Preferences.Set("lastCalmLongitude", longitude);
            Preferences.Set("distance", 0.0);
            Preferences.Set("vehicle", "");
        }

        public bool isPositionChanged(double newLat, double newLong, double oldLat, double oldLong)
        {
            //Test if it work, 
            if (Math.Abs(newLong - oldLong) + Math.Abs(newLat - oldLat) > 0.002)
            {
                return true;
            }
            else {return false;}
        }


        public double CalculateDistance(double newLat, double newLong, double oldLat, double oldLong)
        {
            float[] result = new float[1];
            double distance;
            Android.Locations.Location.DistanceBetween(newLat, newLong, oldLat, oldLong, result);
            distance = result[0];
            return distance;
        }

        
    }

    
}