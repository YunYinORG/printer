using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Printer
{
    /// <summary>
    /// 登录窗体 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 单例模式实例化数据库
        /// </summary>
        static public DataBase database;
        public MainWindow()
        {
            InitializeComponent();
            if (database == null)
            {
                database = new DataBase();
            }
        }
    }
}
