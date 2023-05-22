
using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());


static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True; ");

    try
    {
        connection.Open();
        Console.WriteLine("DB found");
    }
    catch
    {
        Console.WriteLine("DB not found");
    }
return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;
    
    command =myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader= command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDOB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; DOB {readerStringDOB}");
    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, DOB;
    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter dadte of birth (mm-dd-yyyy):");
    DOB = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText= $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{DOB}')";


    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelite;
    Console.WriteLine("Enter an id to delite a customer:");
    idToDelite = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELITE FROM customer WHERE rowid = {idToDelite}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}