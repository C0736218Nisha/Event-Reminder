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

        }

        public void insertValue(string nameValue, string emailValue,string passwordValue)
        {
            //insert into users value(id, name, email)
            

            String insertSQL = "insert into " + TableName + " values ('" + nameValue + "'" + "," + "'"+ passwordValue  + "'" + "," + "'"  + emailValue + "');";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);

        }

        public void selectMyValues(string username,string password)
        {

            String sqlQuery = "Select * from " + TableName;//+ " where " + ColumnName + "=" + "'" + username + "'";//+" AND "+ ColumnPassword +"="+"'"+password+"';";
            System.Console.WriteLine("Select SQL " + sqlQuery);
            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            while (result.MoveToNext())
            {

                var NamefromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);

                var EmailfromDB = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);

                var PasswordfromDB = result.GetInt(result.GetColumnIndexOrThrow(ColumnPassword));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + PasswordfromDB);

            }
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) // Step: 1 - 2:2
        {
            throw new NotImplementedException();
        }
    }
}