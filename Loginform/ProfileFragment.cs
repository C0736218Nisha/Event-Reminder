
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
    public class ProfileFragment : Fragment
    {
        private welcomescreen welcomescreen;
        private DBHelper myDB;
        private string v;

        public ProfileFragment(welcomescreen welcomescreen, DBHelper myDB, string v)
        {
            this.welcomescreen = welcomescreen;
            this.myDB = myDB;
            this.v = v;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View myView = inflater.Inflate(Resource.Layout.ProfileLayout, container, false);

            EditText name = myView.FindViewById<EditText>(Resource.Id.usernameID);
            EditText email = myView.FindViewById<EditText>(Resource.Id.EmailID);
            EditText password = myView.FindViewById<EditText>(Resource.Id.PasswordID);

            ICursor result = myDB.getUser(v);

            while (result.MoveToNext())
            {

                string NamefromDB = result.GetString(0);
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);
                name.Text = NamefromDB;

                string EmailfromDB = result.GetString(1);
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);
                email.Text = EmailfromDB;

                string PasswordfromDB = result.GetString(2);
                System.Console.WriteLine(" Value Of ID FROM DB --> " + PasswordfromDB);
                password.Text = PasswordfromDB;

            }

            // Create your application here

            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(welcomescreen);
            alert.SetTitle("Error");
            alert.SetMessage("Please enter the value");

            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                Toast.MakeText(welcomescreen, "Please Enter a Valid!Value", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();

            Button register = myView.FindViewById<Button>(Resource.Id.submit);
            register.Click += delegate {
                if (name.Text == "" || email.Text == "" || password.Text == "")
                {
                    dialog.Show();
                }
                else
                {
                    myDB.updateValue(name.Text, email.Text, password.Text);
                    Android.App.AlertDialog.Builder alert2 = new Android.App.AlertDialog.Builder(welcomescreen);
                    alert2.SetTitle("Success");
                    alert2.SetMessage("Update Successfully!");
                    alert2.SetPositiveButton("OK", (senderAlert, args) =>
                    {
                       
                    });
                    alert2.Create();
                    alert2.Show();
                }
            };

            return myView;
        }
    }
}
