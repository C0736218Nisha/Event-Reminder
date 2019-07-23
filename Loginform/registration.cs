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

namespace Loginform
{
    [Activity(Label = "registration")]
    public class registration : Activity
    {
        DBHelper dbhelper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.registration);

            EditText name = FindViewById<EditText>(Resource.Id.usernameID);
            EditText email = FindViewById<EditText>(Resource.Id.EmailID);
            EditText password = FindViewById<EditText>(Resource.Id.PasswordID);
            dbhelper = new DBHelper(this);
            
            // Create your application here
            
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Error");
            alert.SetMessage("Please enter the value");

            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                Toast.MakeText(this, "Please Enter a Valid!Value", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();

            Button register = FindViewById<Button>(Resource.Id.submit);
            register.Click += delegate {
                if (name.Text == "" || email.Text == "" || password.Text == "" )
                {
                    dialog.Show();
                }
                else
                {
                    dbhelper.insertValue(name.Text, email.Text, password.Text);
                    Android.App.AlertDialog.Builder alert2 = new Android.App.AlertDialog.Builder(this);
                    alert2.SetTitle("Success");
                    alert2.SetMessage("Registered Successfully!");
                    alert2.SetPositiveButton("OK", (senderAlert, args) =>
                    {
                        StartActivity(typeof(MainActivity));
                    });
                    alert2.Create();
                    alert2.Show();
                }
            };
        }
    }
    }
