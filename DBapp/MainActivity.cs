using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Animation;

namespace DBapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        // Backgrounds
        private ImageView background_1;
        private ImageView background_2;
        private ImageView background_3;
        private ImageView background_4;
        private ImageView background_5;
        private ImageView background_6;
        private ImageView background_7;

        // Buildings and Road
        private ImageView backbuildings_1;
        private ImageView backbuildings_2;
        private ImageView backbuildings_3;
        private ImageView backbuildings_4;
        private ImageView backbuildings_5;
        private ImageView backbuildings_6;
        private ImageView backbuildings_7;
        private ImageView backbuildings_8;

        private ImageView middleBuildings_1;
        private ImageView middleBuildings_2;
        private ImageView middleBuildings_3;
        private ImageView middleBuildings_4;
        private ImageView middleBuildings_5;
        private ImageView middleBuildings_6;
        private ImageView middleBuildings_7;

        private ImageView frontbuildings_1;
        private ImageView frontbuildings_2;
        private ImageView frontbuildings_3;
        private ImageView frontbuildings_4;
        private ImageView frontbuildings_5;
        private ImageView frontbuildings_6;
        private ImageView frontbuildings_7;
        private ImageView frontbuildings_8;
        private ImageView frontbuildings_9;

        private ImageView road1;
        private ImageView road2;
        private ImageView road3;
        private ImageView road4;
        private ImageView road5;
        private ImageView road6;
        private ImageView road7;
        private ImageView road8;
        private ImageView road9;
        private ImageView road10;

        // Menu Buttons
        private ImageButton ProfileButton;
        private ImageButton MenuButton;
        private ImageButton TrophyButton;

        // Pop Ups
        private ScrollView ProfilePopUp;


        // Car Informations
        private LinearLayout CarNameCreate;
        private LinearLayout CarTypeCreate;
        private LinearLayout CarKmPLCreate;
        private LinearLayout CreateUserPopUp;
        private LinearLayout CarName;
        private LinearLayout CarType;
        private LinearLayout CarKmPL;
        private Button saveCarButton;
        private Button deleteCarButton;
        private string carName = "";
        private string carType = "";
        private string carKmPL = "";

        // These are the list that contains all possible transport the user can have
        // Cars that the user have saved
        private List<CarClass> carList = new List<CarClass>();
        // The names of the cars the user has made
        private List<string> transportItems = new List<string>();
        // The main UI dropdown (spinner) with all the transport available (shows in the profile pop up)
        private Spinner mainTransportSpinner;
        // The adapter that contains the available cars
        private ArrayAdapter<string> adapter;
        // The spinner that contains all car types
        private Spinner carTypeSpinner;

        // Constant that decides if the background should be changed.
        int level = 0;

        // User Informations
        private string transport = "";
        private string userName = "";
        private string age = "";
        private UserClass user;

        // Animal Variables
        // The bool values will be in preferences later on.
        private bool koalaSceneVisible = false;
        private bool pbSceneVisible = false;

        // Layout for animals
        private LinearLayout chooseAnimalPopUp;
        private RelativeLayout koalaBearScene;
        private RelativeLayout polarBearScene;

        // Constants used to level up the animal scenes
        private int koalaLevel = 0;
        private int polarBearLevel = 0;

        // Buttons used to change the screen scenes
        private ImageButton cityChangeScene;
        private ImageButton koalaChangeScene;
        private ImageButton PolarBearChangeScene;


        // Animation for cars
        private ImageView car1;
        private ObjectAnimator carAnimator;

        // Trips
        private RelativeLayout tripPopUp;
        private Spinner tripSpinner;
        private List<string> tripNameList;
        private List<TripClass> tripElementsList;
        private Spinner tripTransportSpinner;
        private List<string> tripTransportItems;
        private ArrayAdapter tripAdapter;
        private ArrayAdapter tripTransportAdapter;
        private string chosenTripTransport;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Connect to database
            DBConnect connection = new DBConnect();

            user = new UserClass("Maria", "21", "Walking");

            // Create user
            CreateUser();

            

            // Initialize the car spinners
            CarSpinnerInitialization();

            // Connect the layout with this main class
            BackgroundInitialization();

            // All components that belongs to the profile pop up
            ProfilePopUpInitialization();

            // All components that belongs to the trip pop up
            TripPopUpInitialization();

            // All components that belongs to the pop up where you choose your first animal
            AnimalPopUpInitialization();

            // All buttons that are "always visible (seen on the main scene)
            MainButtons();

            // Car Animations
            car1 = FindViewById<ImageView>(Resource.Id.car1);
            carAnimator = ObjectAnimator.OfFloat(car1, "x", 1200);
            CarAnimation();

            // Buttons that changes the background
            ChangeBackgroundButtons();
  
        }

        private void TripPopUpInitialization() {

            tripPopUp = FindViewById<RelativeLayout>(Resource.Id.TripPopUp);

            tripNameList = new List<string>();
            tripElementsList = new List<TripClass>();
            tripTransportItems = new List<string>();

            tripNameList.Add("New Trip");

            tripTransportItems.Add("Walking");
            tripTransportItems.Add("Bike");
            tripTransportItems.Add("Bus");

            tripSpinner = FindViewById<Spinner>(Resource.Id.tripsSpinner);
            tripSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(TripSpinnerItemSelected);
            tripAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, tripNameList);
            tripSpinner.Adapter = tripAdapter;

            tripTransportSpinner = FindViewById<Spinner>(Resource.Id.tripTransportSpinner);
            tripTransportSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(TripTransportSpinnerItemSelected);
            tripTransportAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, tripTransportItems);
            tripTransportSpinner.Adapter = tripTransportAdapter;


            FindViewById<Button>(Resource.Id.tripSaveButton).Click += (o, e) =>
                SaveTrip();

            FindViewById<Button>(Resource.Id.tripCloseButton).Click += (o, e) =>
                tripPopUp.Visibility = Android.Views.ViewStates.Gone;
        }

        private void SaveTrip() {

            string distance = FindViewById<EditText>(Resource.Id.distanceValue).Text;
            string timeStamp = FindViewById<EditText>(Resource.Id.tripDateValue).Text;
            string tripName = "";
            TripClass trip;

            if (!chosenTripTransport.Equals("Walking") && !chosenTripTransport.Equals("Bike") && !chosenTripTransport.Equals("Bus"))
            {
                int index = tripTransportItems.IndexOf(chosenTripTransport);

                CarClass car = carList[index-3];

                trip = new TripClass(distance, timeStamp, user, car);
                tripName = trip.TimeStamp + " " + chosenTripTransport + " " + trip.Distance;

            }
            else 
            {
                trip = new TripClass(distance, timeStamp, user, chosenTripTransport);
                tripName = trip.TimeStamp + " " + trip.OtherTransport + " " + trip.Distance;
            }

            tripNameList.Add(tripName);
            tripElementsList.Add(trip);
            tripAdapter.Add(tripName);
            tripAdapter.NotifyDataSetChanged();
        }

        private void TripSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e) {

            Spinner spinner = (Spinner)sender;
            string temp = spinner.GetItemAtPosition(e.Position).ToString();

            if (!temp.Equals("New Trip"))
            {
                if (tripElementsList.Count >= 1) {

                    FindViewById<Button>(Resource.Id.tripDeleteButton).Visibility = Android.Views.ViewStates.Visible;
                    TripClass selectedTrip = tripElementsList[e.Position - 1];
                    FindViewById<EditText>(Resource.Id.tripDateValue).Text = selectedTrip.TimeStamp;
                    FindViewById<EditText>(Resource.Id.distanceValue).Text = selectedTrip.Distance;
                }
            }
            else
            {
                FindViewById<Button>(Resource.Id.tripDeleteButton).Visibility = Android.Views.ViewStates.Gone;
                FindViewById<EditText>(Resource.Id.tripDateValue).Text = DateTime.Now.ToString();
                FindViewById<EditText>(Resource.Id.distanceValue).Text = "";
                tripTransportSpinner.SetSelection(0);
            }

        }

        private void TripTransportSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e) {

            Spinner spinner = (Spinner)sender;
            string temp = spinner.GetItemAtPosition(e.Position).ToString();

            chosenTripTransport = temp;

        }

        private void CarAnimation() {


            carAnimator.SetDuration(5000);
            carAnimator.Start();
            carAnimator.RepeatCount = ObjectAnimator.Infinite;

        }

        private void CarSpinnerInitialization() {

            transportItems.Add("Walking");
            transportItems.Add("Bike");
            transportItems.Add("Bus");
            transportItems.Add("New Car");

            //Transport spinner 
            mainTransportSpinner = FindViewById<Spinner>(Resource.Id.spinner);
            mainTransportSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(TransportSpinnerItemSelected);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, transportItems);
            mainTransportSpinner.Adapter = adapter;

            // Car type spinner
            carTypeSpinner = FindViewById<Spinner>(Resource.Id.carTypeSpinner);
            carTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CarTypeSpinner);
            var carTypeAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.car_type_array, Android.Resource.Layout.SimpleSpinnerItem);
            carTypeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            carTypeSpinner.Adapter = carTypeAdapter;
        }

        private void BackgroundInitialization() {
            
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
        }

        private void ProfilePopUpInitialization() {

            ProfileButton = FindViewById<ImageButton>(Resource.Id.profileIcon);
            MenuButton = FindViewById<ImageButton>(Resource.Id.menuIcon);
            TrophyButton = FindViewById<ImageButton>(Resource.Id.trophyIcon);

            ProfilePopUp = FindViewById<ScrollView>(Resource.Id.ProfilePopUp);

            CarName = FindViewById<LinearLayout>(Resource.Id.carNamePrompt);
            CarType = FindViewById<LinearLayout>(Resource.Id.carTypePrompt);
            CarKmPL = FindViewById<LinearLayout>(Resource.Id.carKmPLPrompt);
            saveCarButton = FindViewById<Button>(Resource.Id.saveCarButton);
            deleteCarButton = FindViewById<Button>(Resource.Id.deleteCarButton);

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

        private void AnimalPopUpInitialization() {

            chooseAnimalPopUp = FindViewById<LinearLayout>(Resource.Id.chooseAnimalPopUp);
            koalaBearScene = FindViewById<RelativeLayout>(Resource.Id.koalaScene);
            polarBearScene = FindViewById<RelativeLayout>(Resource.Id.PBScene);
            cityChangeScene = FindViewById<ImageButton>(Resource.Id.cityChangeSceneBtn);
            koalaChangeScene = FindViewById<ImageButton>(Resource.Id.koalaChangeSceneBtn);
            PolarBearChangeScene = FindViewById<ImageButton>(Resource.Id.PBChangeSceneBtn);

            FindViewById<Button>(Resource.Id.polarBearChoice).Click += (o, e) =>
            {
                polarBearScene.Visibility = Android.Views.ViewStates.Visible;
                pbSceneVisible = true;
                chooseAnimalPopUp.Visibility = Android.Views.ViewStates.Gone;
                PolarBearChangeScene.Visibility = Android.Views.ViewStates.Visible;
            };

            FindViewById<Button>(Resource.Id.koalaChoice).Click += (o, e) =>
            {
                koalaBearScene.Visibility = Android.Views.ViewStates.Visible;
                koalaSceneVisible = true;
                chooseAnimalPopUp.Visibility = Android.Views.ViewStates.Gone;
                koalaChangeScene.Visibility = Android.Views.ViewStates.Visible;
            };
        }

        // Buttons seen on the main page
        private void MainButtons() {

            ProfileButton.Click += (o, e) =>
            {
                if (CreateUserPopUp.Visibility != Android.Views.ViewStates.Visible)
                {
                    ProfilePopUp.Visibility = Android.Views.ViewStates.Visible;
                }
            };

            MenuButton.Click += (o, e) =>
                tripPopUp.Visibility = Android.Views.ViewStates.Visible;

            cityChangeScene.Click += (o, e) =>
            {
                koalaBearScene.Visibility = Android.Views.ViewStates.Gone;
                polarBearScene.Visibility = Android.Views.ViewStates.Gone;
            };

            koalaChangeScene.Click += (o, e) =>
            {
                koalaBearScene.Visibility = Android.Views.ViewStates.Visible;
                polarBearScene.Visibility = Android.Views.ViewStates.Gone;
            };

            PolarBearChangeScene.Click += (o, e) =>
            {
                koalaBearScene.Visibility = Android.Views.ViewStates.Gone;
                polarBearScene.Visibility = Android.Views.ViewStates.Visible;
            };
        }

        // Spinner for making the car EditText visible and chooses the transport of the user (User creation)
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

        // Spinner/dropdown of car type  and chooses the type of the car (User Creation).
        private void CreateCarTypeSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            carType = spinner.GetItemAtPosition(e.Position).ToString();
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
                CreateUserEvent();
        }

        // This is the event that happens when a user presses the "create user button"
        private void CreateUserEvent()
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

                        // Create car if everything is satisfied
                        CarClass car = new CarClass(carName, carType, carKmPL, user);
                            
                        // Change the text in the EditText boxes in the profile pop up to the user's name and age 
                        FindViewById<EditText>(Resource.Id.et_username).SetText(user.UserName, TextView.BufferType.Editable);
                        FindViewById<EditText>(Resource.Id.age).SetText(user.UserAge, TextView.BufferType.Editable);

                        // Hide the creation page and show the choose animal page
                        CreateUserPopUp.Visibility = Android.Views.ViewStates.Gone;
                        chooseAnimalPopUp.Visibility = Android.Views.ViewStates.Visible;

                        // Add the new car to the lists of transport
                        carList.Add(car);
                        transportItems.Add(car.CarName);
                        adapter.Add(car.CarName);
                        adapter.NotifyDataSetChanged();

                        tripTransportItems.Add(car.CarName);
                        tripTransportAdapter.Add(car.CarName);
                        tripTransportAdapter.NotifyDataSetChanged();

                        // Set the spinner in the profile pop up to show the newly created car
                        mainTransportSpinner.SetSelection(4);
                        
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
                    FindViewById<EditText>(Resource.Id.et_username).SetText(user.UserName, TextView.BufferType.Editable);
                    FindViewById<EditText>(Resource.Id.age).SetText(user.UserAge, TextView.BufferType.Editable);

                    // Set the spinner to be on the transport the user has chosed
                    switch (transport) {
                        case "Walking":
                            mainTransportSpinner.SetSelection(0);
                            break;
                        case "Bike":
                            mainTransportSpinner.SetSelection(1);
                            break;
                        case "Bus":
                            mainTransportSpinner.SetSelection(2);
                            break;
                    }
                        
                    // Close creation pop up and show the choose animal pop up
                    CreateUserPopUp.Visibility = Android.Views.ViewStates.Gone;
                    chooseAnimalPopUp.Visibility = Android.Views.ViewStates.Visible;
                    
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
            alert.SetMessage("Do you want to delete your user? All progress will forever be lost.");

            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                user.Delete();
                user = null;

                // TODO: Check if this works

                carList.Clear();
                transportItems.Clear();
                transportItems.Add("Walking");
                transportItems.Add("Bike");
                transportItems.Add("Bus");
                transportItems.Add("New Car");

                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, transportItems);
                mainTransportSpinner.Adapter = adapter;

                ProfilePopUp.Visibility = Android.Views.ViewStates.Gone;
                CreateUserPopUp.Visibility = Android.Views.ViewStates.Visible;

                // Reset all values to empty in the creation pop up
                FindViewById<EditText>(Resource.Id.et_usernameCreate).Text = "";
                FindViewById<EditText>(Resource.Id.ageCreate).Text = "";
                FindViewById<EditText>(Resource.Id.carNameCreate).Text = "";
                FindViewById<EditText>(Resource.Id.kmPerLCreate).Text = "";
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
            // The user has to choose any other transport than "new car"
            if (!transport.Equals("New Car")) {

                userName = FindViewById<EditText>(Resource.Id.et_username).Text.ToString();
                age = FindViewById<EditText>(Resource.Id.age).Text.ToString();

                // Update the user
                user.Update("userName", userName);
                user.Update("age", age);

                // If it is neither walking, bike, bus, then the user is using a car.
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

        // Delete the current selected car
        public void DeleteCarAlert()
        {

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

            alert.SetTitle("Are you sure?");
            alert.SetMessage("Do you want to delete this car? Notice, this car will still be connected to your profile.");

            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                // Find the index of the selected item.
                int index = transportItems.IndexOf(transport);

                // Remove at the different lists 
                transportItems.RemoveAt(index);
                carList.RemoveAt(index-4);
                adapter.Remove(transport);
                adapter.NotifyDataSetChanged();

                tripTransportItems.RemoveAt(index - 1);
                tripTransportAdapter.Remove(transport);
                tripTransportAdapter.NotifyDataSetChanged();
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
            });

            Dialog dialog = alert.Create();
            dialog.Show();
    }

        // Save/create the car
        public void SaveCar() { 
            
            carName = FindViewById<EditText>(Resource.Id.carName).Text.ToString();
            carKmPL = FindViewById<EditText>(Resource.Id.kmPerL).Text.ToString();
           
            // If they have pressed save on the choice "New Car" a new car will be created
            if (transport.Equals("New Car"))
            {
                // Check if they have filled everything out
                if (carName != "" && carType != "" && carKmPL != "")
                {
                    // We don't want the user to create the a car with the same name for the ease of use later on
                    if (!transportItems.Contains(carName))
                    {
                        CarClass car = new CarClass(carName, carType, carKmPL, user);

                        // Add the car to the lists
                        carList.Add(car);
                        transportItems.Add(car.CarName);

                        // Update Adapter
                        adapter.Add(car.CarName);
                        adapter.NotifyDataSetChanged();

                        tripTransportItems.Add(car.CarName);
                        tripTransportAdapter.Add(car.CarName);
                        tripTransportAdapter.NotifyDataSetChanged();

                        // Set the spinner to select the newly created car.
                        mainTransportSpinner.SetSelection(transportItems.Count - 1);

                        // Change the transport to the newly created car
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

                    // Remove the element and add it to the same index
                    transportItems.RemoveAt(index);
                    transportItems.Insert(index, carName);
                    carList[index - 4].Update("carName", carName);
                    carList[index - 4].Update("carType", carType);
                    carList[index - 4].Update("KMPerL", carKmPL);
                    adapter.Remove(transport);
                    adapter.Insert(carName.ToString(), index);
                    adapter.NotifyDataSetChanged();

                    tripTransportItems.RemoveAt(index-1);
                    transportItems.Insert(index-1, carName);
                    tripTransportAdapter.Remove(transport);
                    tripTransportAdapter.Insert(carName.ToString(), index-1);
                    tripTransportAdapter.NotifyDataSetChanged();


                    // Change the transport to the curent car with the new name
                    transport = carName;
                }
            }
        }

        // The spinner that selects the current cars type
        private void CarTypeSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            carType = spinner.GetItemAtPosition(e.Position).ToString();
        }

        // The spinner that chooses which transport the user has picked.
        private void TransportSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string temp = spinner.GetItemAtPosition(e.Position).ToString();

            if (!temp.Equals("Walking") && !temp.Equals("Bike") && !temp.Equals("Bus"))
            {
                // Make the different car options visible.
                CarName.Visibility = Android.Views.ViewStates.Visible;
                CarType.Visibility = Android.Views.ViewStates.Visible;
                CarKmPL.Visibility = Android.Views.ViewStates.Visible;
                saveCarButton.Visibility = Android.Views.ViewStates.Visible;

                // If they have chosen an existing car.
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
                    // If the user has chosen new car then no text should be shown, the delete button is gone and the car type spinner will just stand on gasoline
                    deleteCarButton.Visibility = Android.Views.ViewStates.Gone;
                    FindViewById<EditText>(Resource.Id.carName).Text = "";
                    FindViewById<EditText>(Resource.Id.kmPerL).Text = "";
                    carTypeSpinner.SetSelection(0);
                }

            }
            else
            {
                // If they have not chosen any car vehicle then the car options should be gone
                CarName.Visibility = Android.Views.ViewStates.Gone;
                CarType.Visibility = Android.Views.ViewStates.Gone;
                CarKmPL.Visibility = Android.Views.ViewStates.Gone;
                saveCarButton.Visibility = Android.Views.ViewStates.Gone;
                deleteCarButton.Visibility = Android.Views.ViewStates.Gone;
            }

            // Change the transport of the user to the one that they have picked in the spinner
            transport = temp;

        }

        // This function is called whenever a user has chosen what they want to level up and it levels up the koala and polar bear as well.
        private void AnimalLevelUp()
        {
            // Update the levels/layout for the ones that are visible
            if (koalaChangeScene.Visibility == Android.Views.ViewStates.Visible)
            {
                koalaLevel++;
            }

            if (PolarBearChangeScene.Visibility == Android.Views.ViewStates.Visible)
            {
                polarBearLevel++;
            }

            // If one animal is level 25 then the other animal should show up as well.
            if (koalaLevel >= 25)
            {
                PolarBearChangeScene.Visibility = Android.Views.ViewStates.Visible;
            }

            if (polarBearLevel >= 25)
            {
                koalaChangeScene.Visibility = Android.Views.ViewStates.Visible;
            }

            // Update the different scenes depending on if the level is high enough.
            UpdateKoalaScene();
            UpdatePolarBearScene();
        }

        private void UpdateKoalaScene()
        {

            switch (koalaLevel)
            {
                case 5:
                    FindViewById<ImageView>(Resource.Id.koalaBackground1).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 10:
                    FindViewById<ImageView>(Resource.Id.koalaBackground2).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 15:
                    FindViewById<ImageView>(Resource.Id.koalaBackground3).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 20:
                    FindViewById<ImageView>(Resource.Id.koalaBackground4).Visibility = Android.Views.ViewStates.Gone;
                    break;
            }
        }

        private void UpdatePolarBearScene()
        {

            switch (polarBearLevel)
            {
                case 5:
                    FindViewById<ImageView>(Resource.Id.PBBackground1).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 10:
                    FindViewById<ImageView>(Resource.Id.PBBackground2).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 15:
                    FindViewById<ImageView>(Resource.Id.PBBackground3).Visibility = Android.Views.ViewStates.Gone;
                    break;
                case 20:
                    FindViewById<ImageView>(Resource.Id.PBBackground4).Visibility = Android.Views.ViewStates.Gone;
                    break;
            }
        }

        private void ChangeBackgroundButtons() {
            
            FindViewById<Button>(Resource.Id.backbuildingsButton).Click += (o, e) =>
               ChangeBackBuildings();

            FindViewById<Button>(Resource.Id.middleBuildingsButton).Click += (o, e) =>
               ChangeMiddleBuildings();

            FindViewById<Button>(Resource.Id.frontbuildingsButton).Click += (o, e) =>
               ChangeFrontBuildings();

            FindViewById<Button>(Resource.Id.roadButton).Click += (o, e) =>
               ChangeRoad();
        }

        private void ChangeBackBuildings()
        {
            if (backbuildings_7.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_7.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_8.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_6.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_7.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_5.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_6.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_4.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_5.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_3.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_4.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_2.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_3.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (backbuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                backbuildings_1.Visibility = Android.Views.ViewStates.Gone;
                backbuildings_2.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }

            AnimalLevelUp();

            ChangeBackgroundColor();
        }

        private void ChangeMiddleBuildings()
        {

            if (middleBuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_6.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_7.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (middleBuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_5.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_6.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (middleBuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_4.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_5.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (middleBuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_3.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_4.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (middleBuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_2.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_3.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (middleBuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                middleBuildings_1.Visibility = Android.Views.ViewStates.Gone;
                middleBuildings_2.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }

            AnimalLevelUp();
            ChangeBackgroundColor();
        }

        private void ChangeFrontBuildings()
        {
            if (frontbuildings_8.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_8.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_9.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_7.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_7.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_8.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_6.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_6.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_7.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_5.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_5.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_6.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_4.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_4.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_5.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_3.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_3.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_4.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_2.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_2.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_3.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (frontbuildings_1.Visibility != Android.Views.ViewStates.Gone)
            {
                frontbuildings_1.Visibility = Android.Views.ViewStates.Gone;
                frontbuildings_2.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }

            AnimalLevelUp();
            ChangeBackgroundColor();
        }

        private void ChangeRoad()
        {

            if (road9.Visibility != Android.Views.ViewStates.Gone)
            {
                road9.Visibility = Android.Views.ViewStates.Gone;
                road10.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road8.Visibility != Android.Views.ViewStates.Gone)
            {
                road8.Visibility = Android.Views.ViewStates.Gone;
                road9.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road7.Visibility != Android.Views.ViewStates.Gone)
            {
                road7.Visibility = Android.Views.ViewStates.Gone;
                road8.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road6.Visibility != Android.Views.ViewStates.Gone)
            {
                road6.Visibility = Android.Views.ViewStates.Gone;
                road7.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road5.Visibility != Android.Views.ViewStates.Gone)
            {
                road5.Visibility = Android.Views.ViewStates.Gone;
                road6.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road4.Visibility != Android.Views.ViewStates.Gone)
            {
                road4.Visibility = Android.Views.ViewStates.Gone;
                road5.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road3.Visibility != Android.Views.ViewStates.Gone)
            {
                road3.Visibility = Android.Views.ViewStates.Gone;
                road4.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road2.Visibility != Android.Views.ViewStates.Gone)
            {
                road2.Visibility = Android.Views.ViewStates.Gone;
                road3.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }
            else if (road1.Visibility != Android.Views.ViewStates.Gone)
            {
                road1.Visibility = Android.Views.ViewStates.Gone;
                road2.Visibility = Android.Views.ViewStates.Visible;
                level++;
            }

            AnimalLevelUp();
            ChangeBackgroundColor();
        }

        private void ChangeBackgroundColor()
        {

            int levelChange = 5;

            if (level == levelChange)
            {
                background_1.Visibility = Android.Views.ViewStates.Gone;
                background_2.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (level == levelChange * 2)
            {
                background_2.Visibility = Android.Views.ViewStates.Gone;
                background_3.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (level == levelChange * 3)
            {
                background_3.Visibility = Android.Views.ViewStates.Gone;
                background_4.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (level == levelChange * 4)
            {
                background_4.Visibility = Android.Views.ViewStates.Gone;
                background_5.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (level == levelChange * 5)
            {
                background_5.Visibility = Android.Views.ViewStates.Gone;
                background_6.Visibility = Android.Views.ViewStates.Visible;
            }
            else if (level == levelChange * 6)
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