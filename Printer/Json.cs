using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;

namespace Printer
{
    /// <summary>
    /// 进行JSON数据的解析与转换
    /// </summary>
    public class Json
    {
        /// <summary>
        /// 将JSON转换为JObject
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static public JObject JsonToObject(string content)
        {
            return (JObject)JsonConvert.DeserializeObject(content);
        }

        /// <summary>
        /// 将Json转换为JArray
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static public JArray JsonToArray(string content)
        {
            return (JArray)JsonConvert.DeserializeObject(content);
        }

        /// <summary>
        /// 将FileList转换为JSON
        /// </summary>
        /// <param name="filelist"></param>
        /// <returns></returns>
        static public string FileListToJson(FileList filelist)
        {
            JsonSerializer JS = new JsonSerializer();
            StringWriter sw = new StringWriter();
            JS.Serialize(new JsonTextWriter(sw), filelist.getList());
            return sw.GetStringBuilder().ToString();
        }
    }
}
