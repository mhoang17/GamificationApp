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

                        if (Preferences.Get("updateFive", 5) == 0){

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
                                    TripClass newTrip = new TripClass(Preferences.Get("distance", 0.0).ToString(), DateTime.Now.ToString(), MainActivity.GetInstance.GetUser, Preferences.Get("vehicle", "car"));
                                    MainActivity.GetInstance.XPLevelUp();
                                    //And save the trip.
                                    Toast.MakeText(context, "Trip is saved", ToastLength.Short).Show();
                                    resetVaribles(location.Latitude, location.Longitude);

                                }
                            }
                        }

                        
                        if (Preferences.Get("updateFive", 5) == 0)
                        {
                            Preferences.Set("updateFive", 5);
                        }
                        else { Preferences.Set("updateFive", Preferences.Get("updateFive", 5) - 1); }
                        

                        if (Preferences.Get("isTrip", false))
                        {
                            if (Preferences.Get("isWalking", 0) == 0)
                            {
                                Preferences.Set("updateFive", 5);
                                Preferences.Set("isWalking", 1);
                                Preferences.Set("vehicle", "Walking");
                            }

                            if (speedkmh >= 12 && Preferences.Get("isBiking", 0) == 0)
                            {
                                Preferences.Set("vehicle", "Bike");
                                Preferences.Set("updateFive", 5);
                                Preferences.Set("isBiking", 1);
                            }

                            if ((speedkmh > 45) && Preferences.Get("isDriving", 0) == 0)
                            {
                                Preferences.Set("vehicle", "Bus");
                                Preferences.Set("updateFive", 5);
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
            Preferences.Set("updateFive", 5);
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