using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace DBapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        // Backgrounds
        ImageView background_1;
        ImageView background_2;
        ImageView background_3;
        ImageView background_4;
        ImageView background_5;
        ImageView background_6;
        ImageView background_7;

        // Buildings and Road
        ImageView backbuildings_1;
        ImageView backbuildings_2;
        ImageView backbuildings_3;
        ImageView backbuildings_4;
        ImageView backbuildings_5;
        ImageView backbuildings_6;
        ImageView backbuildings_7;
        ImageView backbuildings_8;

        ImageView middleBuildings_1;
        ImageView middleBuildings_2;
        ImageView middleBuildings_3;
        ImageView middleBuildings_4;
        ImageView middleBuildings_5;
        ImageView middleBuildings_6;
        ImageView middleBuildings_7;

        ImageView frontbuildings_1;
        ImageView frontbuildings_2;
        ImageView frontbuildings_3;
        ImageView frontbuildings_4;
        ImageView frontbuildings_5;
        ImageView frontbuildings_6;
        ImageView frontbuildings_7;
        ImageView frontbuildings_8;
        ImageView frontbuildings_9;

        ImageView road1;
        ImageView road2;
        ImageView road3;
        ImageView road4;
        ImageView road5;
        ImageView road6;
        ImageView road7;
        ImageView road8;
        ImageView road9;
        ImageView road10;

        // Menu Buttons
        ImageButton ProfileButton;
        ImageButton MenuButton;
        ImageButton TrophyButton;

        // Pop Ups
        ScrollView ProfilePopUp;


        // Car Informations
        LinearLayout CarNameCreate;
        LinearLayout CarTypeCreate;
        LinearLayout CarKmPLCreate;
        LinearLayout CreateUserPopUp;
        LinearLayout CarName;
        LinearLayout CarType;
        LinearLayout CarKmPL;
        Button saveCarButton;
        Button deleteCarButton;

        // User Informations
        string transport = "";
        string userName = "";
        string age = "";
        UserClass user;

        string carName = "";
        string carType = "";
        string carKmPL = "";
        string oldCarName = "";

        List<CarClass> carList = new List<CarClass>();
        List<string> transportItems = new List<string>();
        Spinner spinner;
        ArrayAdapter<string> adapter;

        Spinner carTypeSpinner;

        // Constant that decides if the background should be changed.
        int sceneChange = 0;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Connect to database
            DBConnect connection = new DBConnect();

            // Create user
            CreateUser();

            transportItems.Add("Walking");
            transportItems.Add("Bike");
            transportItems.Add("Bus");
            transportItems.Add("New Car");

            //Spinners
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, transportItems);
            spinner.Adapter = adapter;


            carTypeSpinner = FindViewById<Spinner>(Resource.Id.carTypeSpinner);
            carTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CarTypeSpinner);
            var carTypeAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.car_type_array, Android.Resource.Layout.SimpleSpinnerItem);
            carTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            carTypeSpinner.Adapter = carTypeAdapter;

            background_1 = FindViewById<ImageView>(Resource.Id.background1);
            background_2 = FindViewById<ImageView>(Resource.Id.background2);
            background_3 = FindViewById<ImageView>(Resource.Id.background3);
            background_4 = FindViewById<ImageView>(Resource.Id.background4);
            background_5 = FindViewById<ImageView>(Resource.Id.background5);
            background_6 = FindViewById<ImageView>(Resource.Id.background6);
            background_7 = FindViewById<ImageView>(Resource.Id.background7);

            backbuildings_1 = FindViewById<ImageView>(Resource.Id.backbuildings1);
            backbuildings_2 = FindViewById<ImageView>(Resource.Id.backbuildings2);
            backbuildings_3 = FindViewById<ImageView>(Resource.Id.backbuildings3);
            backbuildings_4 = FindViewById<ImageView>(Resource.Id.backbuildings4);
            backbuildings_5 = FindViewById<ImageView>(Resource.Id.backbuildings5);
            backbuildings_6 = FindViewById<ImageView>(Resource.Id.backbuildings6);
            backbuildings_7 = FindViewById<ImageView>(Resource.Id.backbuildings7);
            backbuildings_8 = FindViewById<ImageView>(Resource.Id.backbuildings8);

            middleBuildings_1 = FindViewById<ImageView>(Resource.Id.middlebuilding1);
            middleBuildings_2 = FindViewById<ImageView>(Resource.Id.middlebuilding2);
            middleBuildings_3 = FindViewById<ImageView>(Resource.Id.middlebuilding3);
            middleBuildings_4 = FindViewById<ImageView>(Resource.Id.middlebuilding4);
            middleBuildings_5 = FindViewById<ImageView>(Resource.Id.middlebuilding5);
            middleBuildings_6 = FindViewById<ImageView>(Resource.Id.middlebuilding6);
            middleBuildings_7 = FindViewById<ImageView>(Resource.Id.middlebuilding7);

            frontbuildings_1 = FindViewById<ImageView>(Resource.Id.frontbuildings1);
            frontbuildings_2 = FindViewById<ImageView>(Resource.Id.frontbuildings2);
            frontbuildings_3 = FindViewById<ImageView>(Resource.Id.frontbuildings3);
            frontbuildings_4 = FindViewById<ImageView>(Resource.Id.frontbuildings4);
            frontbuildings_5 = FindViewById<ImageView>(Resource.Id.frontbuildings5);
            frontbuildings_6 = FindViewById<ImageView>(Resource.Id.frontbuildings6);
            frontbuildings_7 = FindViewById<ImageView>(Resource.Id.frontbuildings7);
            frontbuildings_8 = FindViewById<ImageView>(Resource.Id.frontbuildings8);
            frontbuildings_9 = FindViewById<ImageView>(Resource.Id.frontbuildings9);

            road1 = FindViewById<ImageView>(Resource.Id.road1);
            road2 = FindViewById<ImageView>(Resource.Id.road2);
            road3 = FindViewById<ImageView>(Resource.Id.road3);
            road4 = FindViewById<ImageView>(Resource.Id.road4);
            road5 = FindViewById<ImageView>(Resource.Id.road5);
            road6 = FindViewById<ImageView>(Resource.Id.road6);
            road7 = FindViewById<ImageView>(Resource.Id.road7);
            road8 = FindViewById<ImageView>(Resource.Id.road8);
            road9 = FindViewById<ImageView>(Resource.Id.road9);
            road10 = FindViewById<ImageView>(Resource.Id.road10);

            ProfileButton = FindViewById<ImageButton>(Resource.Id.profileIcon);
            MenuButton = FindViewById<ImageButton>(Resource.Id.menuIcon);
            TrophyButton = FindViewById<ImageButton>(Resource.Id.trophyIcon);

            ProfilePopUp = FindViewById<ScrollView>(Resource.Id.ProfilePopUp);

            CarName = FindViewById<LinearLayout>(Resource.Id.carNamePrompt);
            CarType = FindViewById<LinearLayout>(Resource.Id.carTypePrompt);
            CarKmPL = FindViewById<LinearLayout>(Resource.Id.carKmPLPrompt);
            saveCarButton = FindViewById<Button>(Resource.Id.saveCarButton);
            deleteCarButton = FindViewById<Button>(Resource.Id.deleteCarButton);

            FindViewById<Button>(Resource.Id.backbuildingsButton).Click += (o, e) =>
               ChangeBackBuildings();

            FindViewById<Button>(Resource.Id.middleBuildingsButton).Click += (o, e) =>
               ChangeMiddleBuildings();

            FindViewById<Button>(Resource.Id.frontbuildingsButton).Click += (o, e) =>
               ChangeFrontBuildings();

            FindViewById<Button>(Resource.Id.roadButton).Click += (o, e) =>
               ChangeRoad();

            ProfileButton.Click += (o, e) =>
            {
                if (CreateUserPopUp.Visibility != Android.Views.ViewStates.Visible)
                {
                    ProfilePopUp.Visibility = Android.Views.ViewStates.Visible;
                }
            };


            FindViewById<Button>(Resource.Id.closeProfilePopUp).Click += (o, e) =>
               ProfilePopUp.Visibility = Android.Views.ViewStates.Gone;


            FindViewById<Button>(Resource.Id.deleteUserButton).Click += (o, e) =>
                DeleteUserAlert();

            FindViewById<Button>(Resource.Id.saveProfileButton).Click += (o, e) =>
                SaveUser();


            FindViewById<Button>(Resource.Id.deleteCarButton).Click += (o, e) =>
                DeleteCarAlert();

            FindViewById<Button>(Resource.Id.saveCarButton).Click += (o, e) =>
                SaveCar();

        }


        // Create User (when you first open the app and when you delete your account).
        private void CreateUser()
        {

            // Initialization
            CreateUserPopUp = FindViewById<LinearLayout>(Resource.Id.UserCreationPopUp);
            CarNameCreate = FindViewById<LinearLayout>(Resource.Id.carNamePromptCreate);
            CarTypeCreate = FindViewById<LinearLayout>(Resource.Id.carTypePromptCreate);
            CarKmPLCreate = FindViewById<LinearLayout>(Resource.Id.carKmPLPromptCreate);

            // Tranport
            Spinner TransportSpinner = FindViewById<Spinner>(Resource.Id.spinnerCreate);
            TransportSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CreateSpinnerItemSelected);
            var transportAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.transport_array, Android.Resource.Layout.SimpleSpinnerItem);
            transportAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            TransportSpinner.Adapter = transportAdapter;

            // Car Type
            Spinner carTypeSpinner = FindViewById<Spinner>(Resource.Id.carTypeSpinnerCreate);
            carTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CreateCarTypeSpinner);
            var carTypeAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.car_type_array, Android.Resource.Layout.SimpleSpinnerItem);
            carTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            carTypeSpinner.Adapter = carTypeAdapter;

            // Create User Button
            FindViewById<Button>(Resource.Id.createUserButton).Click += (o, e) =>
                CreateUserButton();
        }

        // Spinner for making the car options visible (User creation).
        private void CreateSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            if (spinner.GetItemAtPosition(e.Position).Equals("Car"))
            {

                CarNameCreate.Visibility = Android.Views.ViewStates.Visible;
                CarTypeCreate.Visibility = Android.Views.ViewStates.Visible;
                CarKmPLCreate.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {

                CarNameCreate.Visibility = Android.Views.ViewStates.Gone;
                CarTypeCreate.Visibility = Android.Views.ViewStates.Gone;
                CarKmPLCreate.Visibility = Android.Views.ViewStates.Gone;
            }

            transport = spinner.GetItemAtPosition(e.Position).ToString();
        }

        // Spinner/dropdown of car type (User Creation).
        private void CreateCarTypeSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            carType = spinner.GetItemAtPosition(e.Position).ToString();
        }

        // This is the event that happens when a user presses the "create user button"
        private void CreateUserButton()
        {
            userName = FindViewById<EditText>(Resource.Id.et_usernameCreate).Text.ToString();
            age = FindViewById<EditText>(Resource.Id.ageCreate).Text.ToString();

            // Check if all informations are filled out
            if (userName != "" && age != "" && transport != "")
            {
                // If the user chose car as their way of transport, check if they have filled out the information here has well.
                if (transport.Equals("Car"))
                {

                    carName = FindViewById<EditText>(Resource.Id.carNameCreate).Text.ToString();
                    carKmPL = FindViewById<EditText>(Resource.Id.kmPerLCreate).Text.ToString();

                    if (carName != "" && carType != "" && carKmPL != "")
                    {
                        user = new UserClass(userName, age, transport);

                        if (user.UserID == null)
                        {
                            // The user id will be null if the username has been taken
                            FindViewById<TextView>(Resource.Id.ErrorUserTaken).Visibility = Android.Views.ViewStates.Visible;
                        }
                        else
                        {
                            // Create car if everything is satisfied
                            CarClass car = new CarClass(carName, carType, carKmPL, user);
                            carList.Add(car);
                            transportItems.Add(car.CarName);
                            FindViewById<EditText>(Resource.Id.et_username).SetText(user.UserName, TextView.BufferType.Editable);
                            FindViewById<EditText>(Resource.Id.age).SetText(user.UserAge, TextView.BufferType.Editable);
                            CreateUserPopUp.Visibility = Android.Views.ViewStates.Gone;

                            adapter.Add(car.CarName);
                            adapter.NotifyDataSetChanged();
                            spinner.SetSelection(4);

                        }
                    }
                    else
                    {
                        // Diplay that there's missing information
                        FindViewById<TextView>(Resource.Id.ErrorMissingInformation).Visibility = Android.Views.ViewStates.Visible;
                    }

                }
                else
                {
                    // Create user
                    user = new UserClass(userName, age, transport);

                    if (user.UserID == null)
                    {
                        // The user id will be null if the username has been taken
                        FindViewById<TextView>(Resource.Id.ErrorUserTaken).Visibility = Android.Views.ViewStates.Visible;
                    }
                    else
                    {
                        FindViewById<EditText>(Resource.Id.et_username).SetText(user.UserName, TextView.BufferType.Editable);
                        FindViewById<EditText>(Resource.Id.age).SetText(user.UserAge, TextView.BufferType.Editable);

                        switch (transport) {
                            case "Walking":
                                spinner.SetSelection(0);
                                break;
                            case "Bike":
                                spinner.SetSelection(1);
                                break;
                            case "Bus":
                                spinner.SetSelection(2);
                                break;
                        }
                        
                        // Close creation pop up
                        CreateUserPopUp.Visibility = Android.Views.ViewStates.Gone;
                    }

                }
            }
            else
            {
                // Diplay that there's missing information
                FindViewById<TextView>(Resource.Id.ErrorMissingInformation).Visibility = Android.Views.ViewStates.Visible;

            }

        }

        // Delete user
        public void DeleteUserAlert()
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

            alert.SetTitle("Are you sure?");
            alert.SetMessage("Do you want to delete your user?");

            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                user.Delete();
                user = null;
                ProfilePopUp.Visibility = Android.Views.ViewStates.Gone;
                CreateUserPopUp.Visibility = Android.Views.ViewStates.Visible;
                FindViewById<EditText>(Resource.Id.et_usernameCreate).Text = "";
                FindViewById<EditText>(Resource.Id.ageCreate).Text = "";
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        // Save user
        private void SaveUser()
        {

            if (!transport.Equals("New Car")) {

                userName = FindViewById<EditText>(Resource.Id.et_username).Text.ToString();
                age = FindViewById<EditText>(Resource.Id.age).Text.ToString();

                user.Update("userName", userName);
                user.Update("age", age);

                if (!transport.Equals("Walking") && !transport.Equals("Bike") && !transport.Equals("Bus"))
                {
                    user.Update("primaryTransportCurrent", "Car");
                }
                else
                {
                    user.Update("primaryTransportCurrent", transport);
                }
            }

            //TODO: Error message must choose valid transport
        }

        public void DeleteCarAlert()
        {

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

            alert.SetTitle("Are you sure?");
            alert.SetMessage("Do you want to delete this car?");

            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                
                int index = transportItems.IndexOf(transport);

                Console.WriteLine("Index Delete " + index);
                Console.WriteLine("Transport Delete " + transport);
                Console.WriteLine("Count " + transportItems.Count);

                transportItems.RemoveAt(index);
                carList.RemoveAt(index-4);
                adapter.Remove(transport);
                adapter.NotifyDataSetChanged();
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
            });

            Dialog dialog = alert.Create();
            dialog.Show();
    }

        public void SaveCar() { 
            
            carName = FindViewById<EditText>(Resource.Id.carName).Text.ToString();
            carKmPL = FindViewById<EditText>(Resource.Id.kmPerL).Text.ToString();
            try
            {
                if (transport.Equals("New Car"))
                {

                    if (carName != "" && carType != "" && carKmPL != "")
                    {
                        if (!transportItems.Contains(carName))
                        {
                            CarClass car = new CarClass(carName, carType, carKmPL, user);
                            carList.Add(car);
                            transportItems.Add(car.CarName);
                            FindViewById<EditText>(Resource.Id.et_username).SetText(user.UserName, TextView.BufferType.Editable);
                            FindViewById<EditText>(Resource.Id.age).SetText(user.UserAge, TextView.BufferType.Editable);
                            CreateUserPopUp.Visibility = Android.Views.ViewStates.Gone;

                            // Update Adapter
                            adapter.Add(car.CarName);
                            adapter.NotifyDataSetChanged();

                            spinner.SetSelection(transportItems.Count - 1);

                            transport = carName;

                            FindViewById<TextView>(Resource.Id.ErrorMissingInformationCar).Visibility = Android.Views.ViewStates.Gone;
                        }

                        //TODO: Error message for same car name
                    }
                    else
                    {
                        // Diplay that there's missing information
                        FindViewById<TextView>(Resource.Id.ErrorMissingInformationCar).Visibility = Android.Views.ViewStates.Visible;
                    }
                }
                else
                {
                    if (carName != "" && carType != "" && carKmPL != "")
                    {

                        int index = transportItems.IndexOf(transport);
                        Console.WriteLine("Transport " + transport);
                        Console.WriteLine("New Carname " + carName);
                        Console.WriteLine("Old Car Name " + carList[index - 4].CarName);

                        transportItems.RemoveAt(index);
                        transportItems.Insert(index, carName);
                        carList[index - 4].Update("carName", carName);
                        carList[index - 4].Update("carType", carType);
                        carList[index - 4].Update("KMPerL", carKmPL);
                        adapter.Remove(transport);
                        adapter.Insert(carName.ToString(), index);
                        adapter.NotifyDataSetChanged();

                        transport = carName;

                        Console.WriteLine("1: " + carList[index - 4].CarName);
                        Console.WriteLine("2: " + transportItems[index]);
                        Console.WriteLine("3: " + adapter.GetItem(index));
                        Console.WriteLine("4: " + index);
                    }
                }
            } catch (Exception e) {

                Console.WriteLine(e.StackTrace);
                Console.WriteLine(transport);
            }

        }


        private void CarTypeSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            carType = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string temp = spinner.GetItemAtPosition(e.Position).ToString();

            if (!temp.Equals("Walking") && !temp.Equals("Bike") && !temp.Equals("Bus"))
            {

                CarName.Visibility = Android.Views.ViewStates.Visible;
                CarType.Visibility = Android.Views.ViewStates.Visible;
                CarKmPL.Visibility = Android.Views.ViewStates.Visible;
                saveCarButton.Visibility = Android.Views.ViewStates.Visible;

                if (!temp.Equals("New Car"))
                {
                    deleteCarButton.Visibility = Android.Views.ViewStates.Visible;
                    CarClass selectedCar = carList[e.Position-4];
                    FindViewById<EditText>(Resource.Id.carName).Text = selectedCar.CarName;
                    FindViewById<EditText>(Resource.Id.kmPerL).Text = selectedCar.KMPerLProp;

                    switch (selectedCar.CarType) {
                        case "Gasoline":
                            carTypeSpinner.SetSelection(0);
                            break;
                        case "Diesel":
                            carTypeSpinner.SetSelection(1);
                            break;
                        case "Hybrid":
                            carTypeSpinner.SetSelection(2);
                            break;
                        case "Fully Electric":
                            carTypeSpinner.SetSelection(3);
                            break;
                    }
                }
                else
                {
                    deleteCarButton.Visibility = Android.Views.ViewStates.Gone;
                    FindViewById<EditText>(Resource.Id.carName).Text = "";
                    FindViewById<EditText>(Resource.Id.kmPerL).Text = "";
                    carTypeSpinner.SetSelection(0);
                }

            }
            else
            {

                CarName.Visibility = Android.Views.ViewStates.Gone;
                CarType.Visibility = Android.Views.ViewStates.Gone;
                CarKmPL.Visibility = Android.Views.ViewStates.Gone;
                saveCarButton.Visibility = Android.Views.ViewStates.Gone;
                deleteCarButton.Visibility = Android.Views.ViewStates.Gone;
            }

            transport = temp;

        }











        public void ChangeBackBuildings()
        {
            if (backbuildings_7.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_7.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_8.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_6.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_7.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_5.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_6.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_4.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_5.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_3.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_4.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_2.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_3.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (backbuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_1.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_2.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }

            ChangeBackgroundColor();
        }

        public void ChangeMiddleBuildings()
        {

            if (middleBuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_6.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_7.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (middleBuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_5.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_6.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (middleBuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_4.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_5.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (middleBuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_3.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_4.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (middleBuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_2.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_3.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (middleBuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_1.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_2.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }

            ChangeBackgroundColor();
        }

        public void ChangeFrontBuildings()
        {
            if (frontbuildings_8.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_8.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_9.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_7.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_7.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_8.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_6.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_7.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_5.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_6.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_4.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_5.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_3.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_4.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_2.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_3.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (frontbuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_1.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_2.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }

            ChangeBackgroundColor();
        }

        public void ChangeRoad()
        {

            if (road9.Visibility != Android.Views.ViewStates.Gone)
            {
                road9.Visibility = Android.Views.ViewStates.Gone;
                road10.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road8.Visibility != Android.Views.ViewStates.Gone)
            {
                road8.Visibility = Android.Views.ViewStates.Gone;
                road9.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road7.Visibility != Android.Views.ViewStates.Gone)
            {
                road7.Visibility = Android.Views.ViewStates.Gone;
                road8.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road6.Visibility != Android.Views.ViewStates.Gone)
            {
                road6.Visibility = Android.Views.ViewStates.Gone;
                road7.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road5.Visibility != Android.Views.ViewStates.Gone)
            {
                road5.Visibility = Android.Views.ViewStates.Gone;
                road6.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road4.Visibility != Android.Views.ViewStates.Gone)
            {
                road4.Visibility = Android.Views.ViewStates.Gone;
                road5.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road3.Visibility != Android.Views.ViewStates.Gone)
            {
                road3.Visibility = Android.Views.ViewStates.Gone;
                road4.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road2.Visibility != Android.Views.ViewStates.Gone)
            {
                road2.Visibility = Android.Views.ViewStates.Gone;
                road3.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }
            else if (road1.Visibility != Android.Views.ViewStates.Gone)
            {
                road1.Visibility = Android.Views.ViewStates.Gone;
                road2.Visibility = Android.Views.ViewStates.Visible;
                sceneChange++;
            }

            ChangeBackgroundColor();
        }

        public void ChangeBackgroundColor()
        {

            int levelChange = 5;

            if (sceneChange == levelChange)
            {
                background_1.Visibility = Android.Views.ViewStates.Gone;
                background_2.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (sceneChange == levelChange * 2)
            {
                background_2.Visibility = Android.Views.ViewStates.Gone;
                background_3.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (sceneChange == levelChange * 3)
            {
                background_3.Visibility = Android.Views.ViewStates.Gone;
                background_4.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (sceneChange == levelChange * 4)
            {
                background_4.Visibility = Android.Views.ViewStates.Gone;
                background_5.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (sceneChange == levelChange * 5)
            {
                background_5.Visibility = Android.Views.ViewStates.Gone;
                background_6.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (sceneChange == levelChange * 6)
            {
                background_6.Visibility = Android.Views.ViewStates.Gone;
                background_7.Visibility = Android.Views.ViewStates.Visible;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}