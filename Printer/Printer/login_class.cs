using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading;

namespace Printer
{
    class login_class
    {
        static public List<string> myLogin = new List<string>();
        static public string password_save;
        static public string type;        //打印店的type为2
        public delegate string login_delegate(string username, string password);
        //static public event login_delegate login_event;

        static public login_delegate login_del = new login_delegate(login);
        static public string login(string username, string password)
        {
            if (myLogin.Count == 2)
            {
                if (myLogin[0].Length != password.Length)
                {
                    password = md5_encoding(password);
                }
            }
            else
            {
                password = md5_encoding(password);
            }
            password = password.ToLower();
            password_save = password;
            string r = get_token(type, username, password);
            return r;

        }

        private static string md5_encoding(string strpassword)
        {
            byte[] pword = Encoding.Default.GetBytes(strpassword.Trim());       //进行MD5的加密工作
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] out1 = md5.ComputeHash(pword);
            strpassword = BitConverter.ToString(out1).Replace("-", "");
            return strpassword;
        }

        private static string get_token(string type, string strusername, string strpassword)
        {
            string js = "type=" + type + "&account=" + strusername + "&pwd=" + strpassword;

            //POST得到要数据//登陆得到token
            string r = API.PostMethod("/Token", js, new UTF8Encoding());
            return r;
        }

    }
}
