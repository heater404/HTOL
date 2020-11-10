using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTOL.Common
{
    public class CsvHelper
    {
        private string path = string.Empty;
        private const double MaxzSize = 1 / 1024.0;//单位MB
        private uint page = 0;
        /// <summary>
        /// 创建csv文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public CsvHelper(string path)
        {
            this.path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + $"({page}).csv");

            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        /// <summary>
        /// 写一行数据
        /// </summary>
        /// <param name="data">要写入的数据数据 每一列是一个元素</param>
        public void WirteLine(string[] data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string s = string.Empty;
                    foreach (var d in data)
                    {
                        s = s + d + ",";
                    }
                    sw.WriteLine(s);

                    double size = sw.BaseStream.Position / 1024.0 / 1024.0;//单位MB
                    if (size > MaxzSize)
                    {
                        this.path = path.Replace($"({page})", $"({++page})");
                    }
                }
            }
            catch (IOException ex)
            {
                NLogHelper.ErrorLog(ex, "WriteLine");
            }
        }
    }
}
