using System.Collections.Generic;
using System.IO;

namespace ZYTools
{
    /// <summary>
    /// CSVFileReading
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSVFileReading<T> where T: new()
    {
        public List<T> Read(string path)
        {
            string[] myFile = File.ReadAllLines(path);
            return new CSVFilePasing<T>().Iniatilize(myFile);
        }
        public List<T> ReadLines(string path)
        {
            List<string> myFile = new List<string>();
            foreach (string item in File.ReadLines(path))
            {
                myFile.Add(item);
            }
            return new CSVFilePasing<T>().Iniatilize(myFile.ToArray());
        }
        public List<T> ReadLinesRange(string path,int start,int end)
        {
            List<string> myFile = new List<string>();
            IEnumerable<string> a = File.ReadLines(path);
            int i = 0;
            foreach (string item in a)
            {
                if (i <= start)
                {
                    continue;
                }
                i++;
                myFile.Add(item);
                if (i >= end)
                {
                    break;
                }
            }
            return new CSVFilePasing<T>().Iniatilize(myFile.ToArray());
        }
        public string[] ReadDirectory(string path)
        {
            List<string> filesName = new List<string>();
            foreach (FileInfo item in new DirectoryInfo(path).GetFiles())
            {
                if (item.Name.Contains(".meta"))
                {
                    continue;
                }
                filesName.Add(item.Name);
            }
            return filesName.ToArray();
        }
    }

}
