using System;
using System.Collections.Generic;
using System.Linq;

namespace ZYTools
{
    /// <summary>
    /// CSV File Pasing with T type StructFile
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSVFilePasing<T> where T : new()
    {
        public List<T> Iniatilize(string[] data)
        {
            return IniatilizeData(data);
        }
        private List<T> IniatilizeData(string[] file)
        {
            List<T> nodes = new List<T>();
            foreach (var row in file.Skip(1).ToArray())
            {
                T tempDialogNode = new T();
                List<string> items = row.Split(',').ToList();
                foreach (var item in items)
                {
                    ParseData(items.IndexOf(item), tempDialogNode, item, file[0]);
                }
                nodes.Add(tempDialogNode);
            }
            return nodes;
        }
        private void ParseData(int index, T NodeStruct, string data, string file)
        {
            IDataParseType<T>[] a = GetParseType(file).ToArray();
            a[index].ParseData(NodeStruct, data);
        }
        private List<IDataParseType<T>> GetParseType(string vs)
        {
            List<IDataParseType<T>> a = new List<IDataParseType<T>>();
            string[] mytype = vs.Split(',');
            mytype[mytype.Length - 1] = mytype[mytype.Length - 1].Replace("\r", "");
            foreach (var item in mytype)
            {
                var type = Type.GetType(item);
                if (type != null)
                {
                    a.Add(Activator.CreateInstance(type) as IDataParseType<T>);
                }
            }
            return a;
        }
    }

}
