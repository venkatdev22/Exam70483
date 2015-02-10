using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //System.Data.OracleClient.OracleConnectionStringBuilder

                //Examples();
                // Task readTable = new Task(SelectDataFromTable);
                // readTable.Start();
                //Task readMutiSets = new Task(SelectMultipleResultSets);
                //readMutiSets.Start();

                //Task InsertValues = new Task(InsertRowWithParameterizedQuery);
                //InsertValues.Start();

                //UsingaTransactionScope();

                ObjectRelationMapper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }

        static void Examples()
        {
            try
            {
                ///LISTING 4-27  A SqlConnection with a using statement to automatically close it
                using (var connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Exam70483Database"].ConnectionString))
                {
                    connection.Open();

                    /*Execute operations against the database */

                } // Connection is automatically closed. 
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        /// <summary>
        /// LISTING 4-32  Executing a SQL select command
        /// </summary>
        /// <returns></returns>
        static async void SelectDataFromTable()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Exam70483Database"].ConnectionString;
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    var command = new System.Data.SqlClient.SqlCommand("SELECT * FROM People", connection);
                    await connection.OpenAsync();

                    System.Data.SqlClient.SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    await ReadQueryResults(dataReader);
                    //while (await dataReader.ReadAsync())
                    //{
                    //    string formatStringWithMiddleName = "Person ({0}) is named {1} {2} {3}";
                    //    string formatStringWithoutMiddleName = "Person ({0}) is named {1} {3}";

                    //    if ((dataReader["middlename"] == null))
                    //    {
                    //        Console.WriteLine(formatStringWithoutMiddleName,
                    //            dataReader["id"],
                    //            dataReader["firstname"],
                    //            dataReader["lastname"]);
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine(formatStringWithMiddleName,
                    //            dataReader["id"],
                    //            dataReader["firstname"],
                    //            dataReader["middlename"],
                    //            dataReader["lastname"]);
                    //    }
                    //}
                    dataReader.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        /// <summary>
        /// LISTING 4-33  Executing a SQL query with multiple result sets
        /// </summary>
        /// <returns></returns>
        public static async void SelectMultipleResultSets()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Exam70483Database"].ConnectionString;
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var command = new System.Data.SqlClient.SqlCommand("SELECT * FROM People; SELECT TOP 1 * FROM People ORDER BY LastName", connection);

                await connection.OpenAsync();
                System.Data.SqlClient.SqlDataReader dataReader = await command.ExecuteReaderAsync();
                await ReadQueryResults(dataReader);
                await dataReader.NextResultAsync();
                await ReadQueryResults(dataReader);
                dataReader.Close();
            }
        }

        /// <summary>
        /// Read table rows asynchronously
        /// </summary>
        public static async Task ReadQueryResults(System.Data.SqlClient.SqlDataReader dataReader)
        {
            while (await dataReader.ReadAsync())
            {
                string formatStringWithMiddleName = "Person ({0}) is named {1} {2} {3}";
                string formatStringWithoutMiddleName = "Person ({0}) is named {1} {3}";

                if ((dataReader["middlename"] == null))
                {
                    Console.WriteLine(formatStringWithoutMiddleName,
                        dataReader["id"],
                        dataReader["firstname"],
                        dataReader["lastname"]);
                }
                else
                {
                    Console.WriteLine(formatStringWithMiddleName,
                        dataReader["id"],
                        dataReader["firstname"],
                        dataReader["middlename"],
                        dataReader["lastname"]);
                }
            }
        }

        /// <summary>
        /// LISTING 4-35  Inserting values with a parameterized query
        /// </summary>
        /// <returns></returns>
        public static async void InsertRowWithParameterizedQuery()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Exam70483Database"].ConnectionString;

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var command = new System.Data.SqlClient.SqlCommand(
                    "INSERT INTO People([FirstName], [LastName], [MiddleName]) VALUES(@firstName, @lastName, @middleName)"
                    , connection);
                command.Parameters.AddWithValue("@firstName", "John");
                command.Parameters.AddWithValue("@lastName", "Doe");
                command.Parameters.AddWithValue("@middleName", "Little");

                await connection.OpenAsync();
                int numberOfInsertedRows = await command.ExecuteNonQueryAsync();
                Console.WriteLine("Inserted {0} rows", numberOfInsertedRows);
            }

        }

        /// <summary>
        /// LISTING 4-36  Using a TransactionScope
        /// </summary>
        public static void UsingaTransactionScope()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerExam70483"].ConnectionString;
            using (var transactionScope = new System.Transactions.TransactionScope())
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    try
                    {
                    
                    connection.Open();

                    var command1 = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO People ([FirstName], [LastName], [MiddleName]) VALUES('Morne','Morkel','')",
                        connection);

                    var command2 = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO People ([FirstName], [LastName], [MiddleName]) VALUES('Que', 'theknock','')",
                        connection);

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();

                    transactionScope.Complete();
                    }
                    catch (System.Transactions.TransactionException dbex)
                    {
                        transactionScope.Dispose();
                        Console.WriteLine(dbex);
                    }
                }
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// LISTING 4-38  Using Code First to map a class to the database
        /// </summary>
        public static void ObjectRelationMapper()
        {
            try
            {
                using (PeopleContext ctx = new PeopleContext())
                {
                   
                    ctx.People.Add(new Person() { FirstName = "Shane", MiddleName = string.Empty, LastName = "Watson" });
                    ctx.SaveChanges();
                }
            }
            catch (System.Data.EntitySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    /// <summary>
    /// LISTING 4-37  A simple Person class
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }

    public class PeopleContext : System.Data.Entity.DbContext
    {
        public PeopleContext()
            : base("SqlServerExam70483") 
        { }
        public System.Data.Entity.IDbSet<Person> People { get; set; }

    }
}
