
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
    public class ReceivedEventsFragment : Fragment
    {
        private welcomescreen welcomescreen;
        DBHelper myDB;

        ListView myList;
        SearchView mySearch;

        List<EventObject> list;

        MyCustomAdapter myAdapter;
        string userEmail;

        public ReceivedEventsFragment(welcomescreen welcomescreen, DBHelper myDB, string userEmail)
        {
            this.welcomescreen = welcomescreen;
            this.myDB = myDB;
            this.userEmail = userEmail;
            list = new List<EventObject>();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View myView = inflater.Inflate(Resource.Layout.ReceivedEventsLayout, container, false);
            list.Clear();

            myList = myView.FindViewById<ListView>(Resource.Id.myListView);


            ICursor result = myDB.getReceivedEvents(userEmail);


            while (result.MoveToNext())
            {

                
                string myEvents = String.Format("Event Name: {0}\n" +
                    "Event Creater: {1}\n" +
                    "Sent to: {2}\n" +
                    "Event Detail:\n" +
                    "{3}\n\n" +
                    "Event Date: {4}\n" +
                    "Accepted : {5}\n", result.GetString(2), result.GetString(0), result.GetString(1), result.GetString(3), result.GetString(4), result.GetString(5).Equals("0") ? "Not yet" : "Accepted");

                list.Add(new EventObject(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetString(5)));


            }




            myAdapter = new MyCustomAdapter(welcomescreen, list);
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

            List<EventObject> listFiltered = (from items in list
                                         where items.name.Contains(searchText) ||
                                         items.details.Contains(searchText) ||
                                         items.createdby.Contains(searchText) ||
                                         items.date.Contains(searchText)
                                              select items).ToList<EventObject>();


            myList.Adapter = new MyCustomAdapter(welcomescreen, listFiltered);

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
