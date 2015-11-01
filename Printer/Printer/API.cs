using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace Printer
{
    class API
    {
        public static string sid = "";
        public static int myPage = 1;
        private static string server_url = Program.serverUrl;
        public static string PutMethod(string metodUrl, string para, Encoding dataEncode)
        {
            string down = server_url + metodUrl;
            //string down = @"http://yunyin.org/api.php/Index/put";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(down);
            if (sid != "")
            {
                request.Headers.Add("Session-ID", sid);
            }
            request.Method = "PUT";
            string s = "1";
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            request.Accept = "Accept: application/json";
            try
            {
                //byte[] byteArray = dataEncode.GetBytes(para); //转化
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.Accept = "Accept: application/json";
                //request.ContentLength = byteArray.Length;
                //Stream newStream = request.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();

                //Console.WriteLine("\nThe HttpHeaders are \n\n\tName\t\tValue\n{0}", request.Headers);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                //s = sr.ReadToEnd();
                //sr.Close();
                //response.Close();
                //newStream.Close();
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(para);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //while (reader.Peek() != -1)
                    //{
                    //    Console.WriteLine(reader.ReadLine());
                    //}
                    s = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return s;

        }

        public static string DeleteMethod(string metodUrl)
        {
            string s = "1";

            string url = server_url + metodUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "Accept: Application/json";
            request.Method = "delete";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                MessageBox.Show(e.Message + " - " + getRestErrorMessage(response));
                return default(string);
            }
            return s;
        }

        //GetMEthod用来完成Accept: application/json
        /// <summary>
        /// GetMEthod用来完成Accept: application/json(string metodUrl)
        /// </summary>
        /// <param name="metodUrl"></param>
        /// <returns></returns>
        public static string GetMethod(string metodUrl)
        {
            string down = server_url + metodUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(down);
            if (sid != "")
            {
                request.Headers.Add("Session-ID", sid);//修改Headers,添加
            }
            request.Method = "get";
            request.Accept = "Accept: application/json";
            //request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                MessageBox.Show(e.Message + " - " + getRestErrorMessage(response));
                return default(string);
            }
            //
            //Console.WriteLine("\nThe HttpHeaders are \n\n\tName\t\tValue\n{0}", request.Headers);
            string json = getResponseString(response);
            return json;
        }

        // REST @GET 方法，根据泛型自动转换成实体，支持List<T>
        /// <summary>
        /// REST @GET 方法，根据泛型自动转换成实体，支持List<T>(string metodUrl)
        /// </summary>
        /// <param name="metodUrl"></param>
        /// <returns></returns>
        public static string doGetMethodToObj(string metodUrl)
        {
            string down = server_url + metodUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(down);
            request.Method = "get";
            request.Accept = "Accept:application/json";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                MessageBox.Show(e.Message + " - " + getRestErrorMessage(response));
                return default(string);
            }

            string json = getResponseString(response);
            return json;
        }

        //post方法来从服务器访问数据
        /// <summary>
        /// post方法来从服务器访问数据
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static string PostMethod(string postUrl, string paramData, Encoding dataEncode)
        {
            postUrl = server_url + postUrl;
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Accept = "Accept: application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        public static string PostMethod_noparam(string postUrl, Encoding dataEncode)
        {
            postUrl = server_url + postUrl;
            string ret = string.Empty;

            try
            {
                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Accept = "Accept: application/json";
                if (sid != "")
                {
                    webReq.Headers.Add("Session-ID", sid);//修改Headers,添加
                }
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    ret = sr.ReadLine();
                    sr.Close();
                }

                response.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private static string getResponseString(HttpWebResponse response)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }
        // 获取异常信息
        /// <summary>
        /// 获取异常信息
        /// </summary>
        /// <param name="errorResponse"></param>
        /// <returns></returns>
        private static string getRestErrorMessage(HttpWebResponse errorResponse)
        {
            string errorhtml = getResponseString(errorResponse);
            string errorkey = "spi.UnhandledException:";
            errorhtml = errorhtml.Substring(errorhtml.IndexOf(errorkey) + errorkey.Length);
            errorhtml = errorhtml.Substring(0, errorhtml.IndexOf("\n"));
            return errorhtml;
        }
    }
}
