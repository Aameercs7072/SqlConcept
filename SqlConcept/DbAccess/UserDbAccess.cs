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
using Microsoft.IdentityModel.Tokens;

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
                number.Caption = "Mobile Number";
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
                    cloneDataTable.Clear();
                    if (cloneDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in cloneDataTable.Rows)
                        {
                            Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("After Clear No Data is Their...");
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
        public string DataSetTable()
        {
            try
            {
                DataTable customerdt = new DataTable("Customer");

                DataColumn id = new DataColumn("Id", typeof(Guid));
                id.Caption = "Customer Id";
                customerdt.Columns.Add(id);

                DataColumn name = new DataColumn("Name", typeof(string));
                customerdt.Columns.Add(name);

                DataColumn number = new DataColumn("MobileNumber", typeof(long));
                number.Caption = "Mobile Number";
                number.Unique = true;
                customerdt.Columns.Add(number);

                customerdt.PrimaryKey = new DataColumn[] { id };
                Guid id1 = Guid.NewGuid();
                Guid id2 = Guid.NewGuid();
                customerdt.Rows.Add(id1, "Farhan", 9941753070);
                customerdt.Rows.Add(id2, "Umar", 9940395756);

                DataTable orderdt = new DataTable("Order");

                DataColumn orderid = new DataColumn("OrderId", typeof(Guid));
                orderid.Caption = "Order Id";
                orderdt.Columns.Add(orderid);

                DataColumn customerId = new DataColumn("CustomerId", typeof(Guid));
                customerId.Caption = "Customer Id";
                orderdt.Columns.Add(customerId);

                DataColumn amount = new DataColumn("Amount", typeof(float));
                orderdt.Columns.Add(amount);

                orderdt.PrimaryKey = new DataColumn[] { orderid };


                orderdt.Rows.Add(Guid.NewGuid(), id1, 1500.50);
                orderdt.Rows.Add(Guid.NewGuid(), id2, 2000);


                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(customerdt);
                dataSet.Tables.Add(orderdt);

                Console.WriteLine("Customer Table:\n");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Console.WriteLine(row["Id"] + " | " + row["Name"] + " | " + row["MobileNumber"]);
                }
                Console.WriteLine("\nOrder Table:\n");

                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    Console.WriteLine(row["OrderId"] + " | " + row["CustomerId"] + " | " + row["Amount"]);
                }
                Console.WriteLine("Remove table");
                if (dataSet.Tables.Contains("Order") && dataSet.Tables.CanRemove(dataSet.Tables["Order"]))
                {
                    Console.WriteLine("Deleting order table");

                    dataSet.Tables.Remove(dataSet.Tables["Order"]);
                }
                if (dataSet.Tables.Contains("Order"))
                {
                    foreach (DataRow row in dataSet.Tables["Order"].Rows)
                    {
                        Console.WriteLine(row["OrderId"] + " | " + row["CustomerId"] + " | " + row["Amount"]);
                    }
                }
                else
                {
                    Console.WriteLine("Order Data Is Deleted");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Thank you";

        }
        //Using StoreProcedure
        public string SelectUserDataSet(Guid id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetUserById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", id);
                    sqlConnection.Open();
                    Console.WriteLine("Connection Succed");
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Name " + reader["Name"] + " Email " + reader["Email"]);
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
        public string DataViewGetUser()
        {
            string connectionString = "Server=.;Database=EmployeeDB;User Id=system;Password=sqlserver;TrustServerCertificate=true;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from [Employee]", sqlConnection);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DataView dataView1 = new DataView(dataTable);
                    dataView1.Sort = "DEpartment Desc,Name ASC";

                    DataView dataView2 = dataTable.DefaultView;
                    //dataView2.RowFilter = "Age>25 and Department='IT'";
                    Console.WriteLine("DAta Acces using For Loop");
                    for (int i = 0; i < dataView1.Count; i++)
                    {
                        Console.WriteLine($"Id:{dataView1[i][0]},Name:{dataView1[i]["Name"]},Email: {dataView1[i]["Email"]},Department : {dataView1[i]["Department"]}");
                    }

                    Console.WriteLine("DAta Acces using ForEach Loop");

                    foreach (DataRowView row in dataView2)
                    {

                        Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}, Department: {row["Department"]}");

                    }
                    //dataView2.AllowNew=true;
                    DataRowView newRow = dataView2.AddNew();
                    newRow["Id"] = 108;
                    newRow["Name"] = "Aameer";
                    newRow["Mobile"] = "9940395756";
                    newRow["Age"] = 20;
                    newRow["Salary"] = 10000;
                    newRow["Department"] = "IT";
                    newRow.EndEdit();
                    Console.WriteLine("\n New roww add by Data view");
                    foreach (DataRowView row in dataView2)
                    {
                        Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}, Department: {row["Department"]}");

                    }

                    Console.WriteLine("\nUpdate Employe Data");
                    //dataView1.AllowEdit=true;
                    foreach (DataRowView row in dataView2)
                    {
                        Console.WriteLine(Convert.ToString(row["Department"]) == "IT");
                        if (Convert.ToString(row["Department"]) == "IT")
                        {

                            row["Salary"] = Convert.ToInt32(row["Salary"]) + 1000;
                        }
                    }

                    Console.WriteLine("\n Data After an Update");
                    foreach (DataRowView row in dataView2)
                    {
                        Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}, Department: {row["Department"]}, Salary: {row["Salary"]}");

                    }

                    Console.WriteLine("\nDelete Employe Data");
                    //dataView1.AllowEdit=true;
                    foreach (DataRowView row in dataView2)
                    {
                        Console.WriteLine(Convert.ToString(row["Department"]) == "IT");
                        if (Convert.ToString(row["Department"]) == "IT")
                        {

                            row.Delete();
                        }
                    }

                    Console.WriteLine("\n Data After an Update");
                    foreach (DataRowView row in dataView2)
                    {
                        Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}, Department: {row["Department"]}, Salary: {row["Salary"]}");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Thankyou";
        }
        public string BulkDataUser()
        {
            try
            {
                DataTable newData = new DataTable();
                DataColumn id = new DataColumn("Id");
                id.DataType = typeof(Guid);
                newData.Columns.Add(id);
                DataColumn name = new DataColumn("Name");
                newData.Columns.Add(name);
                DataColumn email = new DataColumn("Email");
                newData.Columns.Add(email);
                DataColumn password = new DataColumn("Password");
                newData.Columns.Add(password);
                DataColumn createdOn = new DataColumn("CreatedOn");
                createdOn.DataType = typeof(DateTime);
                newData.Columns.Add(createdOn);

                newData.Rows.Add(Guid.NewGuid(), "Farhan Khan", "farhankhan@gmail.com", "farhan342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Umar Ali", "umarali@gmail.com", "umar342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Renuka Sharma", "renukasharma@gmail.com", "renuka342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Danny Patel", "dannypatel@gmail.com", "danny342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "John Mehta", "johnmehta@gmail.com", "john342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Dinesh Singh", "dineshsingh@gmail.com", "dinesh342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Shankar Gupta", "shankargupta@gmail.com", "shankar342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Sandeep Verma", "sandeepverma@gmail.com", "sandeep342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Aameer Reddy", "aameerreddy@gmail.com", "aameer342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Ali Yadav", "aliyadav@gmail.com", "ali342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Sara Mishra", "saramishra@gmail.com", "sara342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Ravi Kumar", "ravikumar@gmail.com", "ravi342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Kiran Shukla", "kiranshukla@gmail.com", "kiran342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Maya Raj", "mayaraj@gmail.com", "maya342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Farhan Khan", "farhankhan1@gmail.com", "farhan342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Umar Ali", "umarali2@gmail.com", "umar342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Renuka Sharma", "renukasharma2@gmail.com", "renuka342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Danny Patel", "dannypatel2@gmail.com", "danny342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "John Mehta", "johnmehta2@gmail.com", "john342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Dinesh Singh", "dineshsingh2@gmail.com", "dinesh342", DateTime.Now);

                //int batchSize = 3;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("Connection succed");
                    try
                    {
                        sqlConnection.Open();
                        using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                        {
                            try
                            {
                                //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                                {
                                    bulkCopy.BulkCopyTimeout = 500;
                                    bulkCopy.DestinationTableName = "dbo.[User]";
                                    bulkCopy.ColumnMappings.Add("Id", "Id");
                                    bulkCopy.ColumnMappings.Add("Name", "Name");
                                    bulkCopy.ColumnMappings.Add("Email", "Email");
                                    bulkCopy.ColumnMappings.Add("Password", "Password");
                                    bulkCopy.ColumnMappings.Add("CreatedOn", "CreatedOn");
                                    Console.WriteLine("Data Inserting in Process...");
                                    bulkCopy.WriteToServerAsync(newData);

                                }
                                sqlTransaction.Commit();
                                Console.WriteLine("Data Inserted Succesfully");
                            }
                            catch (Exception ex)
                            {
                                if (sqlTransaction != null)
                                {
                                    sqlTransaction.Rollback();
                                    Console.WriteLine("Data Rollbackd dueto" + ex.Message);
                                }


                            }
                            finally
                            {
                                sqlConnection.Close();
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        try
                        {

                        }
                        catch (Exception de)
                        {
                            Console.WriteLine(de.Message);
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                        Console.WriteLine("Eroor occured in insert to Data base" + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return "Thankyou";
        }
        public string BatchUsers()
        {
            try
            {
                DataTable newData = new DataTable();
                DataColumn id = new DataColumn("Id");
                id.DataType = typeof(Guid);
                newData.Columns.Add(id);
                DataColumn name = new DataColumn("Name");
                newData.Columns.Add(name);
                DataColumn email = new DataColumn("Email");
                newData.Columns.Add(email);
                DataColumn password = new DataColumn("Password");
                newData.Columns.Add(password);
                DataColumn createdOn = new DataColumn("CreatedOn");
                createdOn.DataType = typeof(DateTime);
                newData.Columns.Add(createdOn);

                newData.Rows.Add(Guid.NewGuid(), "Farhan Khan", "farhankhan@gmail.com", "farhan342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Umar Ali", "umarali@gmail.com", "umar342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Renuka Sharma", "renukasharma@gmail.com", "renuka342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Danny Patel", "dannypatel@gmail.com", "danny342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "John Mehta", "johnmehta@gmail.com", "john342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Dinesh Singh", "dineshsingh@gmail.com", "dinesh342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Shankar Gupta", "shankargupta@gmail.com", "shankar342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Sandeep Verma", "sandeepverma@gmail.com", "sandeep342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Aameer Reddy", "aameerreddy@gmail.com", "aameer342", DateTime.Now);
                newData.Rows.Add(Guid.NewGuid(), "Ali Yadav", "aliyadav@gmail.com", "ali342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Sara Mishra", "saramishra@gmail.com", "sara342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Ravi Kumar", "ravikumar@gmail.com", "ravi342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Kiran Shukla", "kiranshukla@gmail.com", "kiran342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Maya Raj", "mayaraj@gmail.com", "maya342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Farhan Khan", "farhankhan1@gmail.com", "farhan342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Umar Ali", "umarali2@gmail.com", "umar342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Renuka Sharma", "renukasharma2@gmail.com", "renuka342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Danny Patel", "dannypatel2@gmail.com", "danny342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "John Mehta", "johnmehta2@gmail.com", "john342", DateTime.Now);
                //newData.Rows.Add(Guid.NewGuid(), "Dinesh Singh", "dineshsingh2@gmail.com", "dinesh342", DateTime.Now);

                int batchSize = 2;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("Connection succed");
                    try
                    {
                        sqlConnection.Open();
                        using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
                        {
                            try
                            {

                                SqlDataAdapter adapter = new SqlDataAdapter();

                                string insertQuery = "Insert into [User] (Id, Name, Email, Password, createdOn) Values (@Id,@Name,@Email,@Password,@CreatedOn)";
                                adapter.InsertCommand = new SqlCommand(insertQuery, sqlConnection, sqlTransaction);
                                adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.UniqueIdentifier, 36, "Id");
                                adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 250, "Name");
                                adapter.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 250, "Email");
                                adapter.InsertCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 250, "Password");
                                adapter.InsertCommand.Parameters.Add("@CreatedOn", SqlDbType.DateTime, 8, "createdOn");
                                adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                                adapter.UpdateBatchSize = batchSize;
                              
                                adapter.Update(newData);
                                sqlTransaction.Commit();
                                Console.WriteLine("Batch Inserted Succesfully");
                            }
                            catch (Exception ex)
                            {
                                if (sqlTransaction != null)
                                {
                                    sqlTransaction.Rollback();
                                    Console.WriteLine("Data Rollbackd dueto" + ex.Message);
                                }


                            }
                            finally
                            {
                                sqlConnection.Close();
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        try
                        {

                        }
                        catch (Exception de)
                        {
                            Console.WriteLine(de.Message);
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                        Console.WriteLine("Eroor occured in insert to Data base" + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return "Thankyou";
        }
        public string SqlBuilder()
        {
            string connectionString = "Server=.;Database=EmployeeDB;User Id=system;Password=sqlserver;TrustServerCertificate=true;";
            try
            {
                using (SqlConnection sqlConnection=new SqlConnection(connectionString))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Employee where Department ='IT'",sqlConnection);
                    SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    foreach(DataRow row in dataSet.Tables[0].Rows){
                        Console.WriteLine($"Id: {row["Id"]}");
                    }
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    dataRow["Name"] = "Preethi v";
                    dataRow["Email"] = "prethi.hr@gmail.com";
                    dataRow["Department"] = "HR";
                    int rowupdated = dataAdapter.Update(dataSet, dataSet.Tables[0].TableName);
                    Console.WriteLine($"\nUpdated Command: {builder.GetUpdateCommand().CommandText}");
                    if (rowupdated == 0)
                    {
                        Console.WriteLine("No row Updated");
                    }
                    else
                    {
                        Console.WriteLine($"{rowupdated} row updated");
                    }
                    DataTable table = dataSet.Tables[0];
                    DataRow newRow= table.NewRow();
                    newRow["Id"] = Guid.NewGuid();
                    newRow["Name"] = "Senthil";
                    newRow["Email"] = "senthil @gmail.com";
                    newRow["Mobile"] = "7865093452";
                    newRow["Age"] = 27;
                    newRow["Salary"] = 27000;
                    newRow["Department"] = "IT";
                    table.Rows.Add(newRow);
                    int insertResponse = dataAdapter.Update(dataSet, dataSet.Tables[0].TableName);
                    Console.WriteLine($"\nInsert Command: {builder.GetInsertCommand().CommandText}");
                    if (insertResponse == 0)
                    {
                        Console.WriteLine("No Row Inserted");
                    }
                    else
                    {
                        Console.WriteLine($"\n{insertResponse} row inserted");
                    }
                }
            }
            catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
            return "Thankyou";
        }
    }

}
