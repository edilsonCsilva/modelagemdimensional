using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExtractorCore
{
    public static class Uteis
    {

        public static List<string> getFilesDir(string target)
        {
            List<string> files = new List<string>();
            try
            {
               
                DirectoryInfo Dir = new DirectoryInfo(target);
                FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
                foreach (FileInfo File in Files)
                {
                    string FileName = File.FullName.Replace(Dir.FullName, "");
                    files.Add(FileName.ToString());
                 }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return files;

        }


        public static string getNumbers(string value)
        {
            return String.Join("", System.Text.RegularExpressions.Regex.Split(value, @"[^\d]"));
        }

        public static string getDateTime()
        {
            DateTime aDate = DateTime.Now;
            return aDate.ToString("MM/dd/yyyy HH:mm:ss");

        }

        public static bool createdDir(string pathDir)
        {
            try
            {
                if (!Directory.Exists(pathDir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathDir);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

    }
}
