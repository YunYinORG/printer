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
            byte[] pword = Encoding.Default.GetBytes(strpassword.Trim());       //进行MD5的加密工作
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] out1 = md5.ComputeHash(pword);
            strpassword = BitConverter.ToString(out1).Replace("-", "");
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
                        myRem.Add(password.Text);
                        myRem.Add(printerAcount.Text);

                    }
                    File.WriteAllText(@"pwd.sjc", "");
                    remember.WriteListToTextFile(myRem, @"pwd.sjc");
                    Console.WriteLine(my.token);
                    Console.WriteLine(my.name);
                    Console.WriteLine(my.id);
                    showdownload(my);        //传递参数，显示下载控件
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
            
            if (json.copies == "现场打印")
            {
                buttontext = "确认付款";
                this.mydata.Rows.Add(json.id, userName1, json.name, json.copies, "-", "-", "-", json.time, json.status, buttontext);
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

                this.mydata.Rows.Add(json.id, userName1, json.name, json.copies, json.double_side, json.strcolor, json.ppt, json.time, json.status, buttontext);

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

        

        

        
       






    }
}
