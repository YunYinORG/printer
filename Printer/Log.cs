using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Printer
{
    /// <summary>
    /// 设置本地缓存
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 本地缓存路径
        /// </summary>
        private string LogPath;

        /// <summary>
        /// 本地缓存初始化
        /// </summary>
        /// <param name="logpath"></param>
        public Log(string logpath)
        {
            LogPath = logpath;
        }

        /// <summary>
        /// 更改本地缓存路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool changeLogPath(string path)
        {
            LogPath = path;
            return true;
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="content"></param>
        public void writeToLog(string content)
        {
            using (FileStream fs = new FileStream(LogPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                }
            }
        }

        /// <summary>
        /// 读出缓存
        /// </summary>
        /// <returns></returns>
        public string readFromLog()
        {
            string content = null;
            using (FileStream fs = new FileStream(LogPath, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }
    }
}
