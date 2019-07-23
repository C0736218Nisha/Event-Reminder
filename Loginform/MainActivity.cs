using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
namespace Loginform
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        EditText myUserName;
        EditText myUserPassword;
        Button myBtn;
        DBHelper dbhelper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            myUserName = FindViewById<EditText>(Resource.Id.userNameID);
            myUserPassword = FindViewById<EditText>(Resource.Id.PasswordID);
            myBtn = FindViewById<Button>(Resource.Id.button1);
            dbhelper = new DBHelper(this);
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Error");
            alert.SetMessage(" Please Enter A Value....");

            alert.SetPositiveButton("OK", (senderAlert, args) => {

                Toast.MakeText(this, "Please Enter a Valid! Value", ToastLength.Short).Show();
            });
            Dialog dialog = alert.Create();
            myBtn.Click += delegate {
            var value = myUserName.Text;
                var passwordvalue = myUserPassword.Text;
            if (value == " " || passwordvalue =="")
                {
                    //set alert for executing the task
                    dialog.Show();
                }
                
                else
                {
                    dbhelper.selectMyValues(myUserName.Text, myUserPassword.Text);
                  /*  System.Console.WriteLine("My Value is --> " + value);
                    myBtn.Text = "Submit";
                    Intent myNewScreen = new Intent(this, typeof(welcomescreen));
                    StartActivity(myNewScreen);*/
                }
            };
            TextView newuser = FindViewById<TextView>(Resource.Id.newuser);
            newuser.Click += delegate {
                Intent myNewScreen = new Intent(this, typeof(registration));
                StartActivity(myNewScreen);
            };
        }

    }
}

