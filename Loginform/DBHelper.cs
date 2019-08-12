using System;
using Android.Content;
using Android.Database.Sqlite; // Step: 1 - 1
using Android.Database;
namespace Loginform
{
    public class DBHelper : SQLiteOpenHelper
    {
        // Step: 1 - 2 // Class that you need extend 
        private static string _DatabaseName = "mydatabase.db";
        private const string TableName = "Person";
        private const string ColumnName = "name";
        private const string ColumnEmail = "eMails";
        private const string ColumnPassword = "Password";

        public const string CreateUserTableQuery = "CREATE TABLE " +
        TableName + " ( " + ColumnName + " TEXT,"
            + ColumnEmail + " TEXT," + ColumnPassword + " TEXT);";  //Step: 1 - 4


        private const string EventsTableName = "Events";
        private const string UserName = "user_name";
        private const string partyName = "party_name";
        private const string EventName = "event_name";
        private const string EventDetails = "event_details";
        private const string EventTime = "event_time";
        private const string EventAccepted = "event_accept";

        public const string CreateEventsTableQuery = "CREATE TABLE " +
        EventsTableName + " ( " + UserName + " TEXT,"
            + partyName + " TEXT,"
            + EventName + " TEXT,"
            + EventDetails + " TEXT,"
            + EventTime + " DATETIME,"
            + EventAccepted + " TEXT);";  //Step: 1 - 4



        SQLiteDatabase myDBObj; // Step: 1 - 5
        Context myContext; // Step: 1 - 6

        public DBHelper(Context context) : base(context, name: _DatabaseName, factory: null, version: 1) //Step 2;
        {
            myContext = context;
            myDBObj = WritableDatabase; // Step:3 create a DB objects
        }

        public override void OnCreate(SQLiteDatabase db)  // Step: 1 - 2:1
        {

            db.ExecSQL(CreateUserTableQuery);  // Step: 4
            db.ExecSQL(CreateEventsTableQuery);

        }

        public void insertValue(string nameValue, string emailValue,string passwordValue)
        {
            //insert into users value(id, name, email)
            

            String insertSQL = "insert into " + TableName + " values ('" + nameValue + "'" + "," + "'"+ emailValue  + "'" + "," + "'"  + passwordValue + "');";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);

        }

        public void updateValue(String nameValue, string emailValue, string passValue)
        {
            String insertSQL = "update " + TableName + " set " + ColumnName + " = '" + nameValue + "', " + ColumnPassword + " = '" + passValue + "' where " + ColumnEmail + " = '" + emailValue + "';";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);

        }

        public void selectMyValues(string username,string password)
        {

            String sqlQuery = "Select * from " + TableName + " where " + ColumnName + "=" + "'" + username + "'" +" AND "+ ColumnPassword +"="+"'"+password+"';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            if (result.Count > 0) {


                while (result.MoveToNext())
                {

                    var NamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnName));
                    System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);

                    var EmailfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                    System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);

                    var PasswordfromDB = result.GetInt(result.GetColumnIndexOrThrow(ColumnPassword));
                    System.Console.WriteLine(" Value Of ID FROM DB --> " + PasswordfromDB);

                    Intent intent = new Intent(myContext, typeof(welcomescreen));
                    intent.PutExtra("name", NamefromDB);
                    intent.PutExtra("email", EmailfromDB);
                    myContext.StartActivity(intent);

                }
            }
            else {
                Android.App.AlertDialog.Builder alert2 = new Android.App.AlertDialog.Builder(myContext);
                alert2.SetTitle("Error");
                alert2.SetMessage("No user found try Signup!");
                alert2.SetPositiveButton("OK", (senderAlert, args) =>
                {
                   
                });
                alert2.Create();
                alert2.Show();
            }

            /*while (result.MoveToNext())
            {

                var NamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);

                var EmailfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);

                var PasswordfromDB = result.GetInt(result.GetColumnIndexOrThrow(ColumnPassword));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + PasswordfromDB);

            }*/
        }

        public ICursor getUser(string email)
        {

            String sqlQuery = "Select * from " + TableName + " where " + ColumnEmail + " = '" + email + "';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);


            return result;

        }


        public void insertEventValue(string UserName,
            string participant,
            string EventName,
             string EventDetails,
             string EventTime,
             string EventAccepted)
        {
            //insert into users value(id, name, email)


            String insertSQL = "insert into " + EventsTableName + " values ('" + UserName + "'" + "," + "'" + participant + "'" + "," + "'" + EventName + "'" + "," + "'" + EventDetails + "'" + "," + "'" + EventTime + "'" + "," + "'" + EventAccepted + "');";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);

        }

        public void setEventAccepted(string userName,
            string eventName,
            string eventAccepted)
        {

            string updateSQL = "update " + EventsTableName + " set " + EventAccepted + " = '" + eventAccepted + "' where " + partyName + " = '" + userName+ "' AND " + EventName + " = '" + eventName + "';";

            System.Console.WriteLine("Insert SQL " + updateSQL);
            myDBObj.ExecSQL(updateSQL);

        }

        public ICursor getEventsForUser(string username)
        {

            String sqlQuery = "Select * from " + EventsTableName + " where " + UserName + "=" + "'" + username + "';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            return result;


        }

        internal ICursor getMyEvents(string userEmail)
        {
            String sqlQuery = "Select * from " + EventsTableName + " where " + UserName + "=" + "'" + userEmail + "';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            return result;
        }

        internal ICursor getReceivedEvents(string userEmail)
        {
            String sqlQuery = "Select * from " + EventsTableName + " where " + partyName + "=" + "'" + userEmail + "';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            return result;
        }


        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) // Step: 1 - 2:2
        {
            throw new NotImplementedException();
        }
    }
}