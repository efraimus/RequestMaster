
using RequestMaster.Databases.MainDatabase;

namespace RequestMaster.Patterns
{
    class DatabaseSingleton
    {
        static RequestsContext db;
        public static RequestsContext CreateInstance()
        {
            if (db == null) 
            { 
                db = new RequestsContext();
                return db;
            }
            else return db;
        }
    }
}
