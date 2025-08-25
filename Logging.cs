using System.IO;

namespace OrdersApp
{
    class Logging
    {
        static StreamWriter? streamWriter;
        public static StreamWriter createFileForLogging() {

            if (streamWriter == null) 
            {
                streamWriter = new StreamWriter(App.pathForLogging, true);
                return streamWriter;
            }
            else return streamWriter;
        }
    }
}
