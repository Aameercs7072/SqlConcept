using SqlConcept.DbAccess;

namespace SqlConcept { 
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
            string result = userDb.CloneCopyUser();
            Console.WriteLine(result);




        }
    }
}
