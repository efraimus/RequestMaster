using System.IO;

namespace RequestMaster
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
