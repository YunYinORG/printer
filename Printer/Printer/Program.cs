using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Printer
{
    static class Program
    {
        static public string serverUrl = @"http://yunyin.org/api.php";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login_download());
        }
    }
}
