using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ILMergeGUI
{
    public class Loghelper
    {
        public static string path = System.AppDomain.CurrentDomain.BaseDirectory + "log";

        public static void BugLog(string className, string content, string StackTrace = "")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string filename = path + "/" + "log.log";

            FileInfo f = new FileInfo(filename);
            if (f.Exists)
                f.Delete();

            StreamWriter mySw = File.AppendText(filename);

            string write_content = time + " " + "ERROR" + " " + className + ": " + content + "\r\n" + StackTrace;
            mySw.WriteLine(write_content);

            mySw.Close();
        }

    }
}
