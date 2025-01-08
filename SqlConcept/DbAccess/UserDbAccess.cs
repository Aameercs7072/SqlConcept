using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace SqlConcept.DbAccess
{
    internal class UserDbAccess
    {

        string connectionString = "Server=.;Database=UserDb;User Id=system;Password=sqlserver;TrustServerCertificate=true;";

        public string CreateTable()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Create table [User] (Id uniqueidentifier primary key,Name Nvarchar(100),Email Nvarchar(200),Password Nvarchar(250), CreatedOn DateTime)", sqlConnection);
                    sqlConnection.Open();
                    Console.WriteLine("Connection Succed");
                    int response = cmd.ExecuteNonQuery();
                    return response > 0 ? "Table Created" : "Table not created";
                }
                catch (Exception e)
                {
                    return "Error Ocurred while creating table due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
        public string InsertUser()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = sqlConnection.CreateCommand();
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    Console.WriteLine("Connection Succed");
                    cmd.CommandText = "insert into [User] (ID,[Name],Email,[Password],CreatedOn) Output Inserted.Id " +
                        "values(NewId(),'sakthi','sakthi23@gmail.com','sakthi123',GetDate());"; ;
                    Guid response = (Guid)cmd.ExecuteScalar();
                    return response.ToString();
                }
                catch (Exception e)
                {
                    return "Error Ocurred while Inserting user due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
        public string SelectUser()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from [User]", sqlConnection);

                    sqlConnection.Open();
                    Console.WriteLine("Connection Succed");
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Name " + reader["Name"] + " Email " + reader["Email"]);
                        }
                        return "Total " + reader.FieldCount + " rows Selected";
                    }

                }
                catch (Exception e)
                {
                    return "Error Ocurred while Inserting user due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
                return "NotFound";

            }


        }
        public string CrudUser()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;

                    cmd.CommandText = "insert into [User] Values (NewId(),'Vijay','vijay23@gmail.com','vijay123',GetDate());";
                    connect.Open();
                    int insertResponse = cmd.ExecuteNonQuery();

                    cmd.CommandText = "Update [User] set Email='sakthi123@gmail.com' where Id='21B7E603-984E-4B33-A2DD-4780E5FAAF73';";

                    int UpdateResponse = cmd.ExecuteNonQuery();

                    cmd.CommandText = "delete from [User] where Id='276B9E7B-949C-4F81-B192-A3A6EAB5A4A1';";

                    int DeleteResponse = cmd.ExecuteNonQuery();

                    return "insertResponse= " + insertResponse + "\nUpdateResponse= " + UpdateResponse + "\nDeleteResponse= " + DeleteResponse;
                }
                catch (Exception e)
                {
                    return "Error Occured due to " + e.Message;
                }
            }

        }

        public string SelectUsersData()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from [User];Select * from Employee", sqlConnection);

                    sqlConnection.Open();
                    Console.WriteLine("Connection Succed");
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Name: " + reader["Name"] + " Email: " + reader["Email"]);
                        }

                        while (reader.NextResult())
                        {

                            while (reader.Read())
                            {
                                Console.WriteLine("Name: " + reader["Name"] + " Designation: " + reader["Designation"] + " Email: " + reader["Email"]);
                            }

                        }
                        return "Data fetched";
                    }

                }
                catch (Exception e)
                {
                    return "Error Ocurred while Inserting user due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
                return "NotFound";

            }


        }
        public string DataSetUser()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from [user];Select * from Employee", sqlConnection);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);
                    Console.WriteLine("Using Data Table");

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine(row["Name"] + " , " + row["Email"]);
                    }
                    Console.WriteLine("\nUsing Data Set");

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    Console.WriteLine("\nUser table \n");
                    //Console.WriteLine(adapter.GetSchema());
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine(row["Name"] + " , " + row["Email"]);
                    }
                    Console.WriteLine("\nEmployee table \n");
                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        Console.WriteLine(row["Name"] + " , " + row["Email"] + " , " + row["Designation"]);
                    }

                }
                catch (Exception e)
                {
                    return "Error Ocurred while Inserting user due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
                return "NotFound";

            }


        }
        public string CreateDataTable()
        {
            try
            {
                DataTable dt = new DataTable("Student");

                DataColumn id = new DataColumn("Id");
                id.DataType = typeof(Guid);
                id.AllowDBNull = false;
                id.Unique = true;
                id.Caption = "Student Id";
                dt.Columns.Add(id);

                DataColumn name = new DataColumn("Name");
                name.DataType = typeof(string);
                name.AllowDBNull = false;
                name.MaxLength = 100;
                dt.Columns.Add(name);

                DataColumn number = new DataColumn("MobileNumber");
                number.DataType = typeof(long);
                number.AllowDBNull = false;
                name.Caption = "Mobile Number";
                dt.Columns.Add(number);

                dt.PrimaryKey = new DataColumn[] { id };

                DataRow dr = dt.NewRow();
                dr["id"] = Guid.NewGuid();
                dr["name"] = "Umar";
                dr["MobileNumber"] = 9940395756;
                dt.Rows.Add(dr);

                dt.Rows.Add(Guid.NewGuid(), "Farhan", 9941753070);

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["Id"] + " | " + row["Name"] + " | " + row["MobileNumber"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Thank you";

        }

        public string CloneCopyUser()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from [user];", sqlConnection);
                    DataTable orginalDataTable = new DataTable();

                    adapter.Fill(orginalDataTable);
                    Console.WriteLine("Orginal Data Table");

                    foreach (DataRow row in orginalDataTable.Rows)
                    {
                        Console.WriteLine(row["Name"] + " , " + row["Email"]);
                    }
                    Console.WriteLine("\nUsing Data Set");

                    DataTable copyDataTable = orginalDataTable.Copy();

                    Console.WriteLine("\nUser copy Data Table \n");
                    //Console.WriteLine(adapter.GetSchema());
                    if (copyDataTable != null)
                    {
                        foreach (DataRow row in copyDataTable.Rows)
                        {
                            Console.WriteLine(row["Name"] + " , " + row["Email"]);
                        }
                    }
                    Console.WriteLine("\nUser Clone Data Table \n");
                    DataTable cloneDataTable = orginalDataTable.Clone();
                    if (cloneDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in cloneDataTable.Rows)
                        {
                            Console.WriteLine(row["Name"] + " , " + row["Email"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAdding Data to cloneDataTable");
                        cloneDataTable.Rows.Add(Guid.NewGuid(), "Senthil", "senthil@gmail.com", "senthil435");
                        cloneDataTable.Rows.Add(Guid.NewGuid(), "Venkat", "venkat@gmail.com", "venkat435");
                        foreach (DataRow row in cloneDataTable.Rows)
                        {
                            Console.WriteLine(row["Name"] + " , " + row["Email"]);
                        }
                    }
                }
                catch (Exception e)
                {
                    return "Error Ocurred while Inserting user due to " + e.Message;
                }
                finally
                {
                    sqlConnection.Close();
                }
                return "NotFound";

            }


        }
    }
}
