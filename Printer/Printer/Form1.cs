using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Printer
{
    public partial class login_download : Form
    {
        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public login_download()
        {
            
            InitializeComponent();
            this.Size =new Size (300,300);
        }
        /// <summary>
        /// 窗体生成后执行控件的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_download_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Control.CheckForIllegalCrossThreadCalls = false;
            login.Visible = true;        //登录控件可见
            download.Visible = false;    //下载控件隐藏
            login.Enabled = true;        //登录控件使能
            autorefresh.Enabled = false;  //登录时关闭计时器
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            checkbox.Checked = true;
            myLogin = remember.ReadTextFileToList(@"pwd.sjc");
            if (myLogin.Count == 2)
            {
                password.Text = myLogin[0];
                printerAcount.Text = myLogin[1];
            }
        }
        //-----------------------------------------------------------------------------------------

        //关于登录panel的所有代码

        //-----------------------------------------------------------------------------------------



        //定义委托，执行控件的线程操作
        private delegate void boxDelegate(Control ctrl, string str, bool visuable, bool enable);
        boxDelegate my1;
       
        /// <summary>
        /// 用于控件的切换委托
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="str"></param>
        /// <param name="visuable"></param>
        /// <param name="enable"></param>
        private void boxChange(Control ctrl, string str, bool visuable, bool enable)
        {
            ctrl.Text = str; ctrl.Visible = visuable; ctrl.Enabled = enable;
        }

        List<string> myLogin = new List<string>();   //用于记住用户名和密码
        
        /// <summary>
        /// 执行登录线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginbutton_Click_1(object sender, EventArgs e)
        {
            loginbutton.Enabled = false;     //关闭登录按钮使能
            Thread load_Thread = new Thread(new ThreadStart(load));     //新建线程
            load_Thread.Start();      //开启登录线程
        }
        /// <summary>
        /// 执行登录函数
        /// </summary>
        private void load()
        {
            string type = "2";        //打印店的type为2
            List<string> myRem = new List<string>();       //此处可以进行修改和简化        
            string strusername = printerAcount.Text;            //用户名
            string strpassword = password.Text;            //密码
            if (myLogin.Count == 2)
            {
                if (myLogin[0].Length != strpassword.Length)
                {
                    byte[] pword = Encoding.Default.GetBytes(strpassword.Trim());       //进行MD5的加密工作
                    System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] out1 = md5.ComputeHash(pword);
                    strpassword = BitConverter.ToString(out1).Replace("-", "");
                }
            }
            else
            {
                byte[] pword = Encoding.Default.GetBytes(strpassword.Trim());       //进行MD5的加密工作
                System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
                byte[] out1 = md5.ComputeHash(pword);
                strpassword = BitConverter.ToString(out1).Replace("-", "");
            }
            if (strusername.Length == 0 || strpassword.Length == 0)
            {
                if (error.InvokeRequired)
                {
                    my1 = new boxDelegate(boxChange);
                    error.Invoke(my1, new object[]
                        {error,"请输入您的个人信息",true,true
                        });
                    loginbutton.Invoke(my1, new object[] { loginbutton, "登录", true, true });
                    Thread.Sleep(100);
                }
                else  //这里并没有看出有什么区别
                {
                    error.Text = "请输入您的个人信息";
                    error.Visible = true;
                    loginbutton.Enabled = true;
                }
            }
            else  
            {
                //获取token
                strpassword = strpassword.ToLower();
                string js = "type=" + type + "&account=" + strusername +
                "&pwd=" + strpassword;

                //POST得到要数据//登陆得到token
                string r = API.PostMethod("/Token", js, new UTF8Encoding());
                //从post得到的数据中得到token 
                Console.WriteLine(r);
                JObject toke = JObject.Parse(r);
                ToMyToken my = new ToMyToken();
                bool loginOk = r.Contains("token");
                if (loginOk == true)
                {
                    my.token = (string)toke["token"];//也能够得到token
                    my.name = (string)toke["name"];
                    my.id = (string)toke["id"];
                    my.version = (float)toke["version"];
                    //判断是否保存用户名
                    if (checkbox.Checked)
                    {
                        myRem.Add(strpassword);
                        //myRem.Add(password.Text);
                        myRem.Add(printerAcount.Text);

                    }
                    File.WriteAllText(@"pwd.sjc", "");
                    remember.WriteListToTextFile(myRem, @"pwd.sjc");
                    Console.WriteLine(my.token);
                    Console.WriteLine(my.name);
                    Console.WriteLine(my.id);
                    showdownload(my);        //传递参数，显示下载控件
                    autorefresh.Enabled = true;   //登录成功后启动计时器
                }
                else
                {
                    //登录失败
                    my1 = new boxDelegate(boxChange);
                    if (error.InvokeRequired)
                    {
                        my1 = new boxDelegate(boxChange);
                        error.Invoke(my1, new object[] { error, "登陆失败，请重新登录！", true, true });
                        loginbutton.Invoke(my1, new object[] { loginbutton, "登录", true, true });
                        Thread.Sleep(100);
                    }
                    else
                    {
                        error.Text = "登陆失败，请重新登录！";
                        error.Visible = true;
                        loginbutton.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        //-----------------------------------------------------------------

        //panel切换函数

        //-----------------------------------------------------------------

        //private delegate void hidedelegate();
        //private void hide(hidedelegate myDelegate)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(myDelegate);
        //    }
        //    else
        //    {
        //         myDelegate();
        //    }
        //}
        
        private void showdownload(ToMyToken my)
        {
            
            login.Hide();
            this.Size = new Size(1300, 400);
            this.Location = new Point(50, 200);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            set_default_printer.Hide();
            download.Show();

            downloadToken = my.token;
            printerName = my.name;
            printerId = my.id;
            version = my.version;


            String Date = (DateTime.Now.ToLongDateString());
            path = @"D:\云印南开\" + Date;
            //path = string.Empty;
            if (!File.Exists("json.sjc"))
            {
                File.Create("json.sjc");
            }
            myRefresh();   //获取文件列表
            
            refreshDataGrid();   //初次进入下载界面显示数据
            downloadfirst();
        }
        //-----------------------------------------------------------------

        //关于下载panel的所有代码

        //-----------------------------------------------------------------

        //下载文件的url
        private static string download_url = Program.serverUrl;
        public static string studentNum;

        //定义接收从NKprint_login窗体传值的参数
        public string downloadToken;
        public string printerName;
        public string printerId;
        public float version;

        public string path;   //存储文件路径

        public List<ToJsonMy>jsonlist = new List<ToJsonMy>();     //用于保存文件列表

        
        /// <summary>
        /// 用于向jsonlist添加数据
        /// </summary>
        /// <param name="ja"></param>
        public void addJson(JArray ja)
        {
            bool flag = true;
            int i = 0;//用于遍历的
            //向jsonList中添加数据，如果json的id已经存在，则flag置为false
            //即不添加，维护jsonList
            for (i = 0; i < ja.Count; i++)//遍历ja数组
            {
                bool flag2 = true;
                flag = true;
                foreach (var item in jsonlist)      //此处应当存在一个问题，即循环中jsonlist列表会发生改变，能否使用foreach
                {
                    //已存在的或是已支付的都可不予以考虑
                    if (item.id == ja[i]["id"].ToString())
                    {
                        flag = false;
                        break;
                    }
                }
                if (ja[i]["status"].ToString() == "5")
                {
                    flag = false;
                }
                if (ja[i]["status"].ToString() == "未下载")
                {
                    flag2 = false;
                }
                if (flag == true)
                {
                    //if (flag2 == false)
                    //{
                    //    this.notifyIcon1.Visible = true;
                    //    this.notifyIcon1.ShowBalloonTip(10000);
                    //}
                    ToJsonMy myJs = new ToJsonMy();
                    myJs.id = ja[i]["id"].ToString();
                    myJs.name = ja[i]["name"].ToString();
                    myJs.use_id = ja[i]["use_id"].ToString();
                    //myJs.pri_id = ja[i]["pri_id"].ToString();
                    //myJs.url = ja[i]["url"].ToString();
                    myJs.time = ja[i]["time"].ToString();
                    myJs.name = ja[i]["name"].ToString();
                    myJs.status = ja[i]["status"].ToString();
                    myJs.copies = ja[i]["copies"].ToString();
                    myJs.use_name = ja[i]["use_name"].ToString();
                    myJs.double_side = ja[i]["double_side"].ToString();
                    myJs.student_number = ja[i]["student_number"].ToString();
                    myJs.color = ja[i]["color"].ToString();
                    myJs.ppt_layout = ja[i]["ppt_layout"].ToString();
                    myJs.requirements = ja[i]["requirements"].ToString();
                    jsonlist.Add(myJs);
                }
            }
        }
        
        
        
        /// <summary>
        /// 只用于第一次获取文件列表
        /// </summary>
        private void myRefresh()
        {
            //每次都从文件列表的第一页开始访问
            API.myPage = 1;
            API.token = downloadToken;
            string myJsFile = API.GetMethod("/File/?page=" + API.myPage);
            API.myPage += 1;
#if DEBUG
            Console.WriteLine(myJsFile);    //这是为了调试么
#endif
            JObject jo = JObject.Parse(myJsFile);
            JArray ja = jo["files"] as JArray;
            //将JArray类型的ja转化为ToMyJohn对象数组 
            if (ja == null)
            {
                MessageBox.Show("当前没有要下载文件");
            }
            else
            {
                addJson(ja);
                bool myAdd = (ja.Count == 10);  //主要用于判断是否有下一页
                //这里的逻辑应当仔细考虑
                while (myAdd)                    
                {
                    API.token = downloadToken;
                    myJsFile = API.GetMethod("/File/?page=" + API.myPage);
                    API.myPage += 1;
                    jo = JObject.Parse(myJsFile);
                    ja = jo["files"] as JArray;
                    if (ja == null)
                    {
                        break;
                    }
                    if (ja != null)
                    {
                        addJson(ja);
                    }
                    if (ja.Count < 10)
                        myAdd = false;
                    else
                        myAdd = true;
                }
            }
        }
        /// <summary>
        /// 开始进入下载界面时执行第一次下载
        /// </summary>
        private void downloadfirst()
        {
            foreach (var item in jsonlist)
            {
                if (item.status == "未下载")
                {
                    Download(item.id);
                }
            }

        }
        
        /// <summary>
        /// 用于第一次显示数据
        /// </summary>
        private void refreshDataGrid()
        {
            foreach (var item in jsonlist)
            {
                if(item.status!="未下载")
                {
                    display(item);
                }
            }
        }

        
        /// <summary>
        /// 用于显示单个文件数据
        /// </summary>
        /// <param name="json"></param>
        public void display(ToJsonMy json)
        {
            string userName1 = json.student_number + json.use_name;
            string buttontext = "";
            string name = json.name;
            
             if (json.requirements != "")
                {
                    name = "(注)" + name;

                }
            if (json.copies == "现场打印")
            {
                buttontext = "确认付款";
               
                this.mydata.Rows.Add(json.id, userName1, name, json.copies, "-", "-", "-", json.time, json.status, buttontext);
                
            }
            else
            {
                switch (json.status)
                {
                    case "已下载":
                        buttontext = "通知已打印";
                        break;
                    case "已打印":
                        buttontext = "确认付款";
                        break;
                }
                
                this.mydata.Rows.Add(json.id, userName1, name, json.copies, json.double_side, json.strcolor, json.ppt, json.time, json.status, buttontext);
                
            }
            
        }

        /// <summary>
        /// 用于单个文件的下载
        /// </summary>
        /// <param name="id"></param>
        private void Download(string id)
        {
            //获取文件的索引;
            foreach (var item in jsonlist)
            {
                if (item.id == id)
                {
                    string s = null;
                    string filename = null;
                    string sides = null;
                    Thread piThread1 = new Thread(delegate()
                    {

                        s = API.GetMethod("/File/" + item.id);
                        JObject os = JObject.Parse(s);
                        ToJsonMy my = new ToJsonMy();
                        my.url = os["url"].ToString();
                        sides = item.double_side;
                        filename = item.id + "_" + item.copies + "_" + sides + "_" + item.student_number + "_" + item.name;
                        fileDownload(my.url, filename, item.id);




                    });
                    piThread1.Start();
                }
            }
        }

        //下载从服务器得到的json数据中的用户打印文件
        public bool fileDownload(string url, string fileName, string id)
        {
            //下载文件地址等于服务器地址加上文件地址

            //String Date = (DateTime.Now.ToLongTimeString());
            //path = @"D:\云印南开\" + Date;
            //使用Directory要用到System.IO
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            WebClient webClient = new WebClient();
            String pathDoc = path + "/" + fileName;
            //添加下载完成后的事件
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            try
            {
                webClient.DownloadFileAsync(new Uri(url), pathDoc, id);

            }
            catch
            {
                return false;
                //判断出错
            }
            return true;
        }
        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string id = (e.UserState.ToString());

            foreach (var item in jsonlist)
            {
                if (item.id == id)
                {

                    if (e.Error == null)//下载成功
                    {
                        //改变文件的状态
                        //改变文件在jsonlist中的状态
                        //将已经下载的文件添加到mydata中
                        item.status = "download";
                        display(item);
                        changeStatusById(id, "download");
                        this.notifyIcon1.Visible = true;
                        this.notifyIcon1.ShowBalloonTip(10000);
                        
                    }
                    else
                    {

                        MessageBox.Show("id=" + id + "  " + e.Error.Message);

                    }
                }
            }
        }
        
        /// <summary>
        /// 调用状态改变函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentStatus"></param>
        public void changeStatusById(string id, string currentStatus)
        {
            //put到服务器状态;/api.php/File/1234?token=xxxxxxxxxxxx 
            //将下载完成的文件id添加到下载完成myDown （ArrayList）中
            //参数： status=>文件状态'uploud','download','printing','printed','payed', 返回操作结果
            string putUrl = @"/File/" + id;
            string putPara = "status=" + currentStatus;
            string resualt = API.PutMethod(putUrl, putPara, new UTF8Encoding());
            //Console.WriteLine(out1);
            //添加事件


        }

        /// <summary>
        /// 编辑改变状态按钮函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mydata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == mydata.Columns["operation"].Index)
                {
                    string id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    switch (mydata.Rows[e.RowIndex].Cells["operation"].Value.ToString())
                    {
                        case "通知已打印":
                            changeStatusById(id, "printed");
                            mydata.Rows[e.RowIndex].Cells["status"].Value = "已打印";
                            mydata.Rows[e.RowIndex].Cells["operation"].Value = "确认付款";
                            break;
                        case "确认付款":
                            changeStatusById(id, "5");
                            mydata.Rows.Remove(mydata.Rows[e.RowIndex]);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 手动刷新，同时进行手动下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_Click(object sender, EventArgs e)
        {
            myRefresh();
            foreach (var item in jsonlist)
            {
                if (item.status == "未下载")
                {
                    Download(item.id);
                }
            }

        }

        private void 版本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("云印南开打印店客户端：\n     made by NKsjc 2015.01.08。\n    欢迎交流，qq：2634329276");
        }

        /// <summary>
        /// 双击打开文件，若不存在，则自动下载，双击ID显示用户基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mydata_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex >= 1) && (e.ColumnIndex < 2))
            {
                string id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                foreach (var item in jsonlist)
                {
                    if (id == item.id)
                    {
                        userInfo user = new userInfo();
                        user = usermessage(item.use_id);
                        MessageBox.Show("用户：" + user.name + "  手机号：" + user.phone);
                    }
                }
            }

            else
            {
                string id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                foreach (var item in jsonlist)
                {
                    if (id == item.id)
                    {
                        string filename = "";
                        filename = path + "\\" + item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                        if (File.Exists(@filename))
                        {
                            //filename = path + filename;
                            System.Diagnostics.Process.Start(filename);
                            break;
                        }
                        else
                        {
                            filename = item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                            //get 文件详细信息 URI操作示意: GET /File/1234
                            string jsonUrl = API.GetMethod("/File/" + item.id);
                            JObject jo = JObject.Parse(jsonUrl);
                            ToJsonMy thisOne = new ToJsonMy();
                            thisOne.url = (jo)["url"].ToString();
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            WebClient webClient = new WebClient();
                            String pathDoc = path + "/" + filename;

                            webClient.DownloadFileAsync(new Uri(thisOne.url), pathDoc, id);


                            //fileDownload(thisOne.url, filename, item.id);
                            MessageBox.Show("正在下载该文件！\n等待会儿再打开");
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置自动刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autorefresh_Tick(object sender, EventArgs e)
        {
            myRefresh();
            foreach (var item in jsonlist)
            {
                if (item.status == "未下载")
                {
                    Download(item.id);
                }
            }
        }

        private void 一分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autorefresh.Interval = 60 * 1000;
            一分钟ToolStripMenuItem.Checked = true;
            十分钟ToolStripMenuItem.Checked = false;
            三十分钟ToolStripMenuItem.Checked = false;
        }

        private void 十分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autorefresh.Interval = 60 * 1000 * 10;
            一分钟ToolStripMenuItem.Checked = false;
            十分钟ToolStripMenuItem.Checked = true;
            三十分钟ToolStripMenuItem.Checked = false;
        }

        private void 三十分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autorefresh.Interval = 60 * 1000 * 30;
            一分钟ToolStripMenuItem.Checked = false;
            十分钟ToolStripMenuItem.Checked = false;
            三十分钟ToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="use_id"></param>
        /// <returns></returns>
        public userInfo usermessage(string use_id)
        {
            string jsonUrl = API.GetMethod("/User/" + use_id);
            JObject jo = JObject.Parse(jsonUrl);
            userInfo user = new userInfo();
            user.name = jo["name"].ToString();
            user.phone = jo["phone"].ToString();
            user.email = jo["email"].ToString();
            return user;
        }

        ///// <summary>
        ///// 悬浮窗显示用户信息
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void mydata_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if ((e.ColumnIndex >= 1)&&(e.ColumnIndex<2))
        //    {
        //        string id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
        //        foreach (var item in jsonlist)
        //        {
        //            if (id == item.id)
        //            {
        //                userInfo user = new userInfo();
        //                user = usermessage(item.use_id);
        //                mydata[e.ColumnIndex, e.RowIndex].ToolTipText = "用户：" + user.name + "  手机号：" + user.phone;
        //            }
        //        }
        //    }
        //}
       
        /// <summary>
        /// 增加最小化到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_download_FormClosing(object sender, FormClosingEventArgs e)
        {
            //注意判断关闭事件Reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
                
            }
        }

        /// <summary>
        /// 双击托盘打开窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                
                this.Focus();
                this.ShowInTaskbar = true;
            }
        }

        /// <summary>
        /// 托盘右键关闭退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// 如果有备注信息则显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requirements_Click(object sender, EventArgs e)
        {
            string current_id = mydata.Rows[mydata.CurrentRow.Index].Cells["id"].Value.ToString();
            foreach (var item in jsonlist)
            {
                if (current_id == item.id)
                {
                    if (item.requirements != null)
                    {
                        MessageBox.Show(item.requirements,"备注信息");
                    }
                }
            }
            
        }

        /// <summary>
        /// 单击后判断该文件是否有备注信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mydata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex > -1)&&(e.RowIndex>-1))
            {
                string current_id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                foreach (var item in jsonlist)
                {
                    if (current_id == item.id)
                    {
                        if (item.requirements == "")
                        {
                            requirements.Visible = false;
                        }
                        else
                        {
                            requirements.Visible = true;
                        }
                    }
                }
            }
        }

        private void dicret_download_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("打印出错");
                string current_id = mydata.Rows[mydata.CurrentRow.Index].Cells["id"].Value.ToString();
                foreach (var item in jsonlist)
                {
                    if (current_id == item.id)
                    {
                        string filename = "";
                        filename = path + "\\" + item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                        if (File.Exists(@filename))
                        {
                            filename = item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                            PdfDocument doc = new PdfDocument();
                            doc.LoadFromFile(path + "/" + filename);
                            PrintDialog dialogprint = new PrintDialog();


                            List<string> printerlist = new List<string>();
                            List<string> printers_only_canduplex = new List<string>();
                            List<string> printers_only_supportcolor = new List<string>();
                            List<string> printers_canduplex_supportcolor = new List<string>();
                            List<string> printers_no_cando = new List<string>();
                            string defaultprinter = dialogprint.PrinterSettings.PrinterName;

                            foreach (string printname in PrinterSettings.InstalledPrinters)
                            {
                                if (!printerlist.Contains(printname))
                                {
                                    printerlist.Add(printname);
                                }
                            }

                            printerlist.Remove("Microsoft XPS Document Writer");
                            printerlist.Remove("Fax");

                            foreach (string printername in printerlist)
                            {
                                if (!printers_only_canduplex.Contains(printername))
                                {
                                    dialogprint.PrinterSettings.PrinterName = printername;
                                    if ((dialogprint.PrinterSettings.CanDuplex) && (!dialogprint.PrinterSettings.SupportsColor))
                                    {
                                        printers_only_canduplex.Add(printername);
                                    }
                                }
                            }

                            foreach (string printername in printerlist)
                            {
                                if (!printers_only_supportcolor.Contains(printername))
                                {
                                    dialogprint.PrinterSettings.PrinterName = printername;
                                    if ((dialogprint.PrinterSettings.SupportsColor) && (!dialogprint.PrinterSettings.CanDuplex))
                                    {
                                        printers_only_supportcolor.Add(printername);
                                    }
                                }
                            }

                            foreach (string printername in printerlist)
                            {
                                if (!printers_canduplex_supportcolor.Contains(printername))
                                {
                                    dialogprint.PrinterSettings.PrinterName = printername;
                                    if ((dialogprint.PrinterSettings.SupportsColor) && (dialogprint.PrinterSettings.CanDuplex))
                                    {
                                        printers_canduplex_supportcolor.Add(printername);
                                    }
                                }
                            }
                            foreach (string printername in printerlist)
                            {
                                if (!printers_no_cando.Contains(printername))
                                {
                                    dialogprint.PrinterSettings.PrinterName = printername;
                                    if ((!dialogprint.PrinterSettings.CanDuplex) && (!dialogprint.PrinterSettings.SupportsColor))
                                    {
                                        printers_no_cando.Add(printername);
                                    }
                                }
                            }
                            if (printerlist.Count == 0)
                            {
                                throw new Exception("该电脑并未安装打印机");
                            }
                            dialogprint.PrinterSettings.PrinterName = defaultprinter;
                            if ((item.color == "0") && (item.double_side == "单面"))
                            {
                                if (dialogprint.PrinterSettings.SupportsColor)
                                {
                                    if (printers_no_cando.Count == 0)
                                    {
                                        if (printers_only_canduplex.Count == 0)
                                        {
                                            if (printers_only_supportcolor.Count == 0)
                                            {
                                                dialogprint.PrinterSettings.PrinterName = printers_canduplex_supportcolor[0];
                                            }
                                            else
                                            {
                                                dialogprint.PrinterSettings.PrinterName = printers_only_supportcolor[0];
                                            }
                                        }
                                        else
                                        {
                                            dialogprint.PrinterSettings.PrinterName = printers_only_canduplex[0];
                                        }
                                    }
                                    else
                                    {
                                        dialogprint.PrinterSettings.PrinterName = printers_no_cando[0];
                                    }
                                }
                                else
                                {
                                    dialogprint.PrinterSettings.PrinterName = defaultprinter;
                                }

                                dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
                                dialogprint.PrinterSettings.DefaultPageSettings.Color = false;
                            }

                            else if ((item.color == "1") && (item.double_side == "单面"))
                            {


                                //正常设置
                                //if (printers_only_supportcolor.Contains(defaultprinter) || printers_canduplex_supportcolor.Contains(defaultprinter))
                                //{
                                //    dialogprint.PrinterSettings.PrinterName = defaultprinter;
                                //}
                                //else
                                //{
                                //    if (printers_only_supportcolor.Count == 0)
                                //    {
                                //        if (printers_canduplex_supportcolor.Count == 0)
                                //        {
                                //            throw new Exception("不存在满足要求的打印机");
                                //        }
                                //        else
                                //        {
                                //            dialogprint.PrinterSettings.PrinterName = printers_canduplex_supportcolor[0];
                                //        }
                                //    }
                                //    else
                                //    {
                                //        dialogprint.PrinterSettings.PrinterName = printers_only_supportcolor[0];
                                //    }
                                //}


                                //dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
                                //dialogprint.PrinterSettings.DefaultPageSettings.Color = true;

                                //光辉岁月单独设置
                                if (printers_canduplex_supportcolor.Contains(defaultprinter))
                                {
                                    dialogprint.PrinterSettings.PrinterName = defaultprinter;
                                }
                                else
                                {
                                    if (printers_canduplex_supportcolor.Count == 0)
                                    {
                                        throw new Exception("不存在满足要求的打印机");
                                    }
                                    else
                                    {
                                        dialogprint.PrinterSettings.PrinterName = printers_canduplex_supportcolor[0];
                                    }
                                }



                                dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
                                dialogprint.PrinterSettings.DefaultPageSettings.Color = true;

                            }
                            else if ((item.color == "0") && (item.double_side == "双面"))
                            {
                                if (!printers_only_supportcolor.Contains(defaultprinter) || !printers_canduplex_supportcolor.Contains(defaultprinter))
                                {
                                    dialogprint.PrinterSettings.PrinterName = defaultprinter;
                                }
                                else
                                {
                                    if (printers_only_canduplex.Count == 0)
                                    {
                                        if (printers_canduplex_supportcolor.Count == 0)
                                        {
                                            throw new Exception("不存在满足要求的打印机");
                                        }
                                        else
                                        {
                                            dialogprint.PrinterSettings.PrinterName = printers_canduplex_supportcolor[0];
                                        }
                                    }
                                    else
                                    {
                                        dialogprint.PrinterSettings.PrinterName = printers_only_canduplex[0];
                                    }
                                }

                                dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
                                dialogprint.PrinterSettings.DefaultPageSettings.Color = false;

                            }
                            else if ((item.color == "1") && (item.double_side == "双面"))
                            {
                                if (printers_canduplex_supportcolor.Contains(defaultprinter))
                                {
                                    dialogprint.PrinterSettings.PrinterName = defaultprinter;
                                }
                                else
                                {
                                    if (printers_canduplex_supportcolor.Count == 0)
                                    {
                                        throw new Exception("不存在满足要求的打印机");
                                    }
                                    else
                                    {
                                        dialogprint.PrinterSettings.PrinterName = printers_canduplex_supportcolor[0];
                                    }
                                }



                                dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
                                dialogprint.PrinterSettings.DefaultPageSettings.Color = true;
                            }

                            dialogprint.UseEXDialog = true;
                            dialogprint.AllowPrintToFile = true;
                            dialogprint.AllowSomePages = true;
                            dialogprint.PrinterSettings.MinimumPage = 1;
                            dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
                            dialogprint.PrinterSettings.FromPage = 1;
                            dialogprint.PrinterSettings.Collate = true;
                            //dialogprint.PrinterSettings.CanDuplex = true;
                            //dialogprint.PrinterSettings.
                            dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

                            string copy = item.copies.Substring(0, 1);
                            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);




                            //dialogprint.ShowDialog();
                            //if (dialogprint.ShowDialog() == DialogResult.OK)
                            //{
                            doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
                            doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
                            doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
                            PrintDocument printdoc = doc.PrintDocument;

                            dialogprint.Document = printdoc;
                            printdoc.Print();
                            //}
                            break;





                        }
                        else
                        {
                            filename = item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                            //get 文件详细信息 URI操作示意: GET /File/1234
                            string jsonUrl = API.GetMethod("/File/" + item.id);
                            JObject jo = JObject.Parse(jsonUrl);
                            ToJsonMy thisOne = new ToJsonMy();
                            thisOne.url = (jo)["url"].ToString();
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            WebClient webClient = new WebClient();
                            String pathDoc = path + "/" + filename;
                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
                            webClient.DownloadFileAsync(new Uri(thisOne.url), pathDoc, id);


                            //fileDownload(thisOne.url, filename, item.id);
                            MessageBox.Show("正在下载该文件！\n请等待，稍后请再次点击打印按钮");
                        }
                        break;
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打印");
            }

        }

        private void 设置默认打印机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {

                if (!printer_comboBox.Items.Contains(printname))
                {
                    printer_comboBox.Items.Add(printname);

                }
            }

            set_default_printer.Show();
        }

        private void ensure_printer_Click(object sender, EventArgs e)
        {
            if (printer_comboBox.SelectedItem != null)
            {
                if (Externs.SetDefaultPrinter(printer_comboBox.SelectedItem.ToString()))
                {
                    set_default_printer.Hide();
                    MessageBox.Show(printer_comboBox.SelectedItem.ToString() + "设置为默认打印机成功！");

                }
                else
                {
                    MessageBox.Show(printer_comboBox.SelectedItem.ToString() + "设置为默认打印机失败！");
                }
            }
        }

        private void close_printer_Click(object sender, EventArgs e)
        {
            set_default_printer.Hide();
        }

        class Externs
        {
            [DllImport("winspool.drv")]
            public static extern bool SetDefaultPrinter(String Name);
        }

        private void OnDownloadFileCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("已下载完成\n请再次点击打印按钮");

        }

        private void set_before_print_Click(object sender, EventArgs e)
        {


            try
            {
                //MessageBox.Show("打印出错");
                string current_id = mydata.Rows[mydata.CurrentRow.Index].Cells["id"].Value.ToString();
                foreach (var item in jsonlist)
                {
                    if (current_id == item.id)
                    {
                        string filename = "";
                        filename = path + "\\" + item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                        if (File.Exists(@filename))
                        {
                            filename = item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                            PdfDocument doc = new PdfDocument();
                            doc.LoadFromFile(path + "/" + filename);
                            PrintDialog dialogprint = new PrintDialog();




                            dialogprint.UseEXDialog = true;
                            dialogprint.AllowPrintToFile = true;
                            dialogprint.AllowSomePages = true;
                            dialogprint.PrinterSettings.MinimumPage = 1;
                            dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
                            dialogprint.PrinterSettings.FromPage = 1;
                            dialogprint.PrinterSettings.Collate = true;
                            //dialogprint.PrinterSettings.CanDuplex = true;
                            //dialogprint.PrinterSettings.
                            dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

                            string copy = item.copies.Substring(0, 1);
                            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);




                            //dialogprint.ShowDialog();
                            if (dialogprint.ShowDialog() == DialogResult.OK)
                            {
                            doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
                            doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
                            doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
                            PrintDocument printdoc = doc.PrintDocument;

                            dialogprint.Document = printdoc;
                            printdoc.Print();
                            }
                            break;





                        }
                        else
                        {
                            filename = item.id + "_" + item.copies + "_" + item.double_side + "_" + item.student_number + "_" + item.name;
                            //get 文件详细信息 URI操作示意: GET /File/1234
                            string jsonUrl = API.GetMethod("/File/" + item.id);
                            JObject jo = JObject.Parse(jsonUrl);
                            ToJsonMy thisOne = new ToJsonMy();
                            thisOne.url = (jo)["url"].ToString();
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            WebClient webClient = new WebClient();
                            String pathDoc = path + "/" + filename;
                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
                            webClient.DownloadFileAsync(new Uri(thisOne.url), pathDoc, id);


                            //fileDownload(thisOne.url, filename, item.id);
                            MessageBox.Show("正在下载该文件！\n请等待，稍后请再次点击打印按钮");
                        }
                        break;
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打印");
            }

        }






    }
}
