using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
namespace Loginform
{
    [Activity(Label = "WELCOMESCREEN")]
    public class welcomescreen : Activity
    {
        Fragment[] _fragmentsArray;
        DBHelper myDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(Android.Views.WindowFeatures.ActionBar);
            //enable navigation mode to support tab layout
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            this.ActionBar.Title = "Welcome " + Intent.GetStringExtra("name");

            myDB = new DBHelper(this);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.welcomescreen);

            _fragmentsArray = new Fragment[]
           {
            new CreateEventFragment(this, myDB,Intent.GetStringExtra("email")),
            new MyEventsFragment(this, myDB, Intent.GetStringExtra("email")),
            new ReceivedEventsFragment(this, myDB, Intent.GetStringExtra("email")),
            new ProfileFragment(this, myDB, Intent.GetStringExtra("email"))

           };



            AddTabToActionBar("Create\nEvent"); //First Tab
            AddTabToActionBar("My\nEvents"); //First Tab
            AddTabToActionBar("Received\nEvents"); //First Tab
            AddTabToActionBar("Profile"); //First Tab
        }

        void AddTabToActionBar(string tabTitle)
        {
            Android.App.ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(tabTitle);

            tab.SetIcon(Android.Resource.Drawable.IcInputAdd);

            tab.TabSelected += TabOnTabSelected;

            ActionBar.AddTab(tab);
        }



        void TabOnTabSelected(object sender, Android.App.ActionBar.TabEventArgs tabEventArgs)
        {
            Android.App.ActionBar.Tab tab = (Android.App.ActionBar.Tab)sender;

            //Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragmentsArray[tab.Position];

            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.logout:
                    {
                        StartActivity(typeof(MainActivity));
                        Finish();
                        return true;
                    }
                
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

                   
