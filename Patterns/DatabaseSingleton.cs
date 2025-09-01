
using OrdersApp.Databases.OrdersDatabase;

namespace RequestMaster.Patterns
{
    class DatabaseSingleton
    {
        static OrdersContext db;
        public static OrdersContext CreateInstance()
        {
            if (db == null) 
            { 
                db = new OrdersContext();
                return db;
            }
            else return db;
        }
    }
}
