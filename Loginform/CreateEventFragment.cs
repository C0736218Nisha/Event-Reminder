
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Loginform
{
    public class CreateEventFragment : Fragment
    {
        private welcomescreen welcomescreen;
        EditText eventName, eventDetails, eventparticipants;
        Button _dateSelectButton, createEvent;
        DBHelper myDB;
        List<String> userList;
        string userEmail;

       
        public CreateEventFragment(welcomescreen welcomescreen, DBHelper myDB, string userEmail)
        {
            this.welcomescreen = welcomescreen;
            this.myDB = myDB;
            this.userEmail = userEmail;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View myView = inflater.Inflate(Resource.Layout.CreateEventLayout, container, false);

            eventparticipants = myView.FindViewById<EditText>(Resource.Id.eventparty);
            eventName = myView.FindViewById<EditText>(Resource.Id.eventname);
            eventDetails = myView.FindViewById<EditText>(Resource.Id.eventdetails);

            _dateSelectButton = myView.FindViewById<Button>(Resource.Id.date);
            _dateSelectButton.Click += DateSelect_OnClick;



            createEvent = myView.FindViewById<Button>(Resource.Id.create);
            createEvent.Click += delegate {

                if (eventparticipants.Text.Equals("") || eventName.Text.Equals("") || eventDetails.Text.Equals("") || _dateSelectButton.Text.Equals("Pick Date")) {

                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(welcomescreen);
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the value");

                    alert.SetPositiveButton("OK", (senderAlert, args) =>
                    {
                        Toast.MakeText(welcomescreen, "Please Enter a Valid!Value", ToastLength.Short).Show();
                    });

                    alert.Create();
                    alert.Show();

                }
                else {

                    string[] participants = eventparticipants.Text.Split(",");

                    foreach(string s in participants) {

                        myDB.insertEventValue(userEmail, s.Trim(), eventName.Text, eventDetails.Text, _dateSelectButton.Text, "0");
                        welcomescreen.ActionBar.SelectTab(welcomescreen.ActionBar.GetTabAt(1));
                    
                    }

                
                }
            };



            return myView;

           


        }
        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _dateSelectButton.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}
