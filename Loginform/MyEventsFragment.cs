
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
    public class MyEventsFragment : Fragment
    {
        private welcomescreen welcomescreen;

        ListView myList;
        SearchView mySearch;

        List<String> list;

        ArrayAdapter myAdapter;
        DBHelper myDB;
        string userEmail;

        public MyEventsFragment(welcomescreen welcomescreen, DBHelper myDB, string userEmail)
        {
            this.welcomescreen = welcomescreen;
            this.myDB = myDB;
            this.userEmail = userEmail;
            list = new List<string>();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            View myView = inflater.Inflate(Resource.Layout.MyEventsLayout, container, false);
            list.Clear();

            myList = myView.FindViewById<ListView>(Resource.Id.myListView);

             
            ICursor result = myDB.getMyEvents(userEmail);

            if (result.Count > 0) {
                while (result.MoveToNext())
                {
                    string myEvents = String.Format("\nEvent Name: {0}\n" +
                        "Event Creater: {1}\n" +
                        "Sent to: {2}\n" +
                        "Event Date: {4}\n" +
                        "Accepted : {5}\n" +
                        "Event Detail:\n" +
                        "{3}\n\n"
                        , result.GetString(2), result.GetString(0), result.GetString(1), result.GetString(3), result.GetString(4), result.GetString(5).Equals("0") ? "Not yet" : "Accepted");

                    list.Add(myEvents);


                }

            }
            else {
                list.Add("No Events Created by you!");
            }




            myAdapter = new ArrayAdapter
                (welcomescreen, Android.Resource.Layout.SimpleListItem1, list);
            myList.Adapter = myAdapter;
            myList.ItemClick += myIteamClickMethod;

            mySearch = myView.FindViewById<SearchView>(Resource.Id.searchID);
            //Search Events
            mySearch.QueryTextChange += mySearchMethod;

            return myView;

        }

        public void mySearchMethod(object sender, SearchView.QueryTextChangeEventArgs e)
        {

            var searchText = e.NewText;

            List<string> listFiltered = (from items in list
                    where items.Contains(searchText)
                    select items).ToList<string>();


            myList.Adapter = new ArrayAdapter
                (welcomescreen, Android.Resource.Layout.SimpleListItem1, listFiltered);

        }

        public void myIteamClickMethod(object sender, AdapterView.ItemClickEventArgs e)
        {
            System.Console.WriteLine("I am clicking on the list item \n\n");
            var indexValue = e.Position;
            var myValue = list[indexValue];
            System.Console.WriteLine("Value is \n\n " + myValue);
        }
    }
}
