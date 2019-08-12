using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace Loginform
{
    public class MyCustomAdapter : BaseAdapter<EventObject>
    {
        List<EventObject> myEventsList;
        Activity localContext;
        DBHelper myDB;

        public MyCustomAdapter(Activity myContext, List<EventObject> myUsers) : base()
        {
            localContext = myContext;
            myEventsList = myUsers;

            myDB = new DBHelper(myContext);
        }


        public override EventObject this[int position]
        {
            get { return myEventsList[position]; }
        }

        public override int Count
        {
            get { return myEventsList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }



        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            EventObject myObject = myEventsList[position];

            View myView = convertView; // re-use an existing view, if one is


                myView = localContext.LayoutInflater.Inflate(Resource.Layout.ReceivedEventItemLayout, null);

                myView.FindViewById<TextView>(Resource.Id.name).Text = "Event Name: " + myObject.name;
                myView.FindViewById<TextView>(Resource.Id.sentBy).Text = "Sent By: " + myObject.createdby;
                myView.FindViewById<TextView>(Resource.Id.date).Text = "Date: " + myObject.date;
                myView.FindViewById<TextView>(Resource.Id.details).Text = "Event Details:\n" + myObject.details;

                if (myObject.accepted.Equals("0")) {
                myView.FindViewById<Button>(Resource.Id.reject).Enabled = false;

            }
            else {
                myView.FindViewById<Button>(Resource.Id.accept).Enabled = false;
            }

                myView.FindViewById<Button>(Resource.Id.accept).Click += delegate {

                    myDB.setEventAccepted(myObject.sentto, myObject.name, "1");
                    myView.FindViewById<Button>(Resource.Id.accept).Enabled = false;
                    myView.FindViewById<Button>(Resource.Id.reject).Enabled = true;


                };

                myView.FindViewById<Button>(Resource.Id.reject).Click += delegate {

                    myDB.setEventAccepted(myObject.sentto, myObject.name, "0");
                    myView.FindViewById<Button>(Resource.Id.accept).Enabled = true;
                    myView.FindViewById<Button>(Resource.Id.reject).Enabled = false;
                };





            return myView;
        }
    
}
}
