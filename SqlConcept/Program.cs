using SqlConcept.DbAccess;

namespace SqlConcept
{
    public class Program
    {
        //public void CreatTable()
        //{

        //}


        public static void Main(String[] args)
        {
            UserDbAccess userDb = new UserDbAccess();


            //string result = userDb.CreateTable();
            //string result = userDb.InsertUser
            //string result = userDb.CrudUser();    
            //string result = userDb.SelectUser();
            //string result = userDb.SelectUsersData();
            //string result = userDb.DataSetUser();
            //string result = userDb.CreateDataTable();
            //string result = userDb.CloneCopyUser();
            //string result = userDb.DataSetTable();

            //Console.WriteLine("Enter Id of the user");
            //string? inpid = Console.ReadLine();
            //if (inpid != null)
            //{
            //    Guid id = Guid.Parse(inpid);
            //    string result = userDb.SelectUserDataSet(id);
            //    Console.WriteLine(result);
            //}
            //string result = userDb.DataViewGetUser();
            //string result = userDb.BulkDataUser();
            //string result = userDb.BatchUsers();
            //string result = userDb.SqlBuilder();

            //Console.WriteLine(result);
            userDb.TransformTable();





        }
    }
}
