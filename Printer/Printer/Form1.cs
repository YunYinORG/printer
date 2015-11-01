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
            this.Size = new Size(300, 300);
        }
        /// <summary>
        /// 窗体生成后执行控件的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_download_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            login.Visible = true;        //登录控件可见
            download.Visible = false;    //下载控件隐藏
            login.Enabled = true;        //登录控件使能
            checkbox.Checked = true;
            login_class.myLogin = remember.ReadTextFileToList(@"pwd.sjc");
            if (login_class.myLogin.Count == 2)
            {
                password.Text = login_class.myLogin[0];
                printerAcount.Text = login_class.myLogin[1];
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


        /// <summary>
        /// 执行登录线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginbutton_Click_1(object sender, EventArgs e)
        {
            loginbutton.Enabled = false;     //关闭登录按钮使能
            load();
        }


        /// <summary>
        /// 执行登录函数
        /// </summary>
        private void load()
        {
            login_class.type = "2";        //打印店的type为2
            List<string> myRem = new List<string>();       //此处可以进行修改和简化        
            if (printerAcount.Text.Length == 0 || password.Text.Length == 0)
            {
                if (error.InvokeRequired)
                {
                    my1 = new boxDelegate(boxChange);
                    error.Invoke(my1, new object[] { error, "请输入您的个人信息", true, true });
                    loginbutton.Invoke(my1, new object[] { loginbutton, "登录", true, true });
                    Thread.Sleep(100);
                }
                else
                {
                    error.Text = "请输入您的个人信息";
                    error.Visible = true;
                    loginbutton.Enabled = true;
                }
            }
            else
            {
                IAsyncResult result = login_class.login_del.BeginInvoke(printerAcount.Text, password.Text, null, null);
                string r = login_class.login_del.EndInvoke(result);
                login_to_show(r);
            }
        }

        public void login_to_show(string r)
        {
            List<string> myRem = new List<string>();
            if (r.Contains("sid"))
            {
                JObject toke = JObject.Parse(r);
                location_settings.my.sid = (string)toke["info"]["sid"];//也能够得到token
                location_settings.my.name = (string)toke["info"]["printer"]["name"];
                location_settings.my.id = (string)toke["info"]["printer"]["id"];
                location_settings.my.sch_id = (string)toke["info"]["printer"]["sch_id"];
                API.sid = location_settings.my.sid;
                //判断是否保存用户名
                if (checkbox.Checked)
                {
                    myRem.Add(login_class.password_save);
                    myRem.Add(printerAcount.Text);
                }
                File.WriteAllText(@"pwd.sjc", "");
                remember.WriteListToTextFile(myRem, @"pwd.sjc");
                showdownload();        //传递参数，显示下载控件
                timer_init();
            }
            else
            {
                //登录失败

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


        private void showdownload()
        {

            login.Hide();
            this.Size = new Size(1300, 400);
            this.Location = new Point(50, 200);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            set_default_printer.Hide();
            printers_setting_dialog.Hide();
            download.Show();

            String Date = (DateTime.Now.ToLongDateString());
            location_settings.file_path = @"D:\云印南开\" + Date;
            location_settings.ibook_path = @"D:\云印南开_本店电子书\";
            location_settings.creat_path();

            backgroundworker_refresh br = new backgroundworker_refresh(this);
            br.refresh_first();

        }




        //-----------------------------------------------------------------

        //关于下载panel的所有代码

        //-----------------------------------------------------------------


        private static System.Timers.Timer aTimer;

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
                    ToJsonMy file = database.find_myjson(id);
                    switch (mydata.Rows[e.RowIndex].Cells["operation"].Value.ToString())
                    {
                        case "通知已打印":
                            file.changeStatusById("4");
                            mydata.Rows[e.RowIndex].Cells["status"].Value = "已打印";
                            mydata.Rows[e.RowIndex].Cells["operation"].Value = "确认付款";
                            break;
                        case "确认付款":
                            DialogResult dr = MessageBox.Show("确认付款？", "", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                file.ensure_payed();
                                mydata.Rows.Remove(mydata.Rows[e.RowIndex]);
                            }
                            break;
                        case "手动下载":
                            download_errfile_class download_class = new download_errfile_class(this, file, e.RowIndex);
                            download_class.download();
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
            backgroundworker_refresh br = new backgroundworker_refresh(this);
            br.refresh_other();
        }

        private void 版本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("云印南天打印店客户端");
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
                ToJsonMy file = database.find_myjson(id);

                userInfo user = new userInfo();
                user = usermessage(file.use_id);
                MessageBox.Show("用户：" + user.name + "  学号：" + user.student_number + "  手机号：" + user.phone);

            }

            else if ((e.ColumnIndex >= 2) && (e.ColumnIndex < 3))
            {
                string id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                ToJsonMy file = database.find_myjson(id);
                if (file != null)
                {
                    if (!file.is_ibook)
                    {
                        string filename = "";
                        filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;

                        string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                        if (doc_extension != ".pdf")
                        {
                            filename += ".pdf";
                        }

                        if (File.Exists(@filename))
                        {
                            System.Diagnostics.Process.Start(filename);

                        }
                        else
                        {
                            download_single_single_class file_download = new download_single_single_class(this, file);
                            file_download.download();
                        }

                    }
                    else
                    {
                        string filename = "";
                        filename = location_settings.ibook_path + file.name.Substring(0, file.name.Length - "【店内书】".Length);
                        if (File.Exists(@filename))
                        {
                            System.Diagnostics.Process.Start(filename);

                        }
                        else
                        {
                            MessageBox.Show("本店电子书路径有误，请改正");
                        }

                    }
                }

            }
        }

        private void 一分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aTimer.Interval = 60 * 1000;
            一分钟ToolStripMenuItem.Checked = true;
            十分钟ToolStripMenuItem.Checked = false;
            三十分钟ToolStripMenuItem.Checked = false;
        }

        private void 十分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aTimer.Interval = 60 * 1000 * 10;
            一分钟ToolStripMenuItem.Checked = false;
            十分钟ToolStripMenuItem.Checked = true;
            三十分钟ToolStripMenuItem.Checked = false;
        }

        private void 三十分钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aTimer.Interval = 60 * 1000 * 30;
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
            string jsonUrl = API.GetMethod("/printer/user/" + use_id);
            JObject jo = JObject.Parse(jsonUrl);
            userInfo user = new userInfo();
            user.name = jo["info"]["name"].ToString();
            user.sch_id = jo["info"]["sch_id"].ToString();
            user.student_number = jo["info"]["number"].ToString();
            jsonUrl = API.GetMethod("/printer/user/" + use_id + "/phone");
            jo = JObject.Parse(jsonUrl);
            user.phone = jo["info"].ToString();
            return user;
        }

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
            ToJsonMy file = database.find_myjson(current_id);
            if (file != null)
            {
                if (file.requirements != null)
                {
                    MessageBox.Show(file.requirements, "备注信息");
                }
            }
            //}

        }

        /// <summary>
        /// 单击后判断该文件是否有备注信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mydata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex > -1) && (e.RowIndex > -1))
            {
                string current_id = mydata.Rows[e.RowIndex].Cells["id"].Value.ToString();
                ToJsonMy file = database.find_myjson(current_id);
                if (file != null)
                {
                    if (file.requirements == "")
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

        private void dicret_download_Click(object sender, EventArgs e)
        {
            try
            {

                string current_id = mydata.Rows[mydata.CurrentRow.Index].Cells["id"].Value.ToString();
                ToJsonMy file = database.find_myjson(current_id);
                if (file != null)
                {
                    if (!file.is_ibook)
                    {
                        string filename = "";
                        filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;

                        string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                        if ((doc_extension == ".doc") || (doc_extension == ".docx"))
                        {
                            filename += ".pdf";
                        }
                        if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
                        {
                            filename += ".pdf";
                            throw new Exception("ppt文件请设置文件后打印");
                        }

                        if (File.Exists(@filename))
                        {

                            print_class.direct_print_file(file, this);
                        }
                        else
                        {
                            download_single_single_class file_download = new download_single_single_class(this, file);
                            file_download.download();
                        }

                    }
                    else
                    {
                        print_class.direct_print_ibook(file, this);
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

        private void set_before_print_Click(object sender, EventArgs e)
        {


            try
            {
                string current_id = mydata.Rows[mydata.CurrentRow.Index].Cells["id"].Value.ToString();
                ToJsonMy file = database.find_myjson(current_id);
                if (file != null)
                {
                    if (!file.is_ibook)
                    {
                        string filename = "";
                        filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;
                        string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                        if ((doc_extension == ".doc") || (doc_extension == ".docx"))
                        {
                            filename += ".pdf";

                        }
                        if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
                        {
                            filename += ".pdf";
                        }


                        if (File.Exists(@filename))
                        {
                            print_class.setbefore_print_file(file, this);


                        }
                        else
                        {
                            download_single_single_class file_download = new download_single_single_class(this, file);
                            file_download.download();
                        }

                    }
                    else
                    {
                        print_class.setbefore_print_ibook(file, this);

                    }
                }

            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打印");
            }

        }

        public void timer_init()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            aTimer.Interval = 60000;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            backgroundworker_refresh br = new backgroundworker_refresh(this);
            br.refresh_other();
        }

        private void 打印机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printer_setting_dialog_show();
        }

        public void printer_setting_dialog_show()
        {
            ///
            /// 1：对4种情况分别添加计算机有的打印机:
            /// 2：如果之前设置过打印机，显示设为默认初值
            ///
            //List<string> printer_use_list = new List<string>();
            location_settings.printer_setting_list = remember.ReadTextFileToList(@"printer_setting.sjc");
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {

                if (!noduplex_nocolor_combox.Items.Contains(printname))
                {
                    noduplex_nocolor_combox.Items.Add(printname);
                }
            }

            foreach (string printname in PrinterSettings.InstalledPrinters)
            {

                if (!duplex_nocolor_combox.Items.Contains(printname))
                {
                    duplex_nocolor_combox.Items.Add(printname);
                }
            }
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {

                if (!noduplex_color_combox.Items.Contains(printname))
                {
                    noduplex_color_combox.Items.Add(printname);
                }
            }
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {

                if (!duplex_color_combox.Items.Contains(printname))
                {
                    duplex_color_combox.Items.Add(printname);
                }
            }
            //若果已经存在了保存的打印机选择列表，显示在combox中
            int i = 0;
            if (location_settings.printer_setting_list.Count == 4)
            {
                ///
                /// 已经有打印店选择的信息后printer_setting.sjc，直接显示在combox中
                ///
                for (i = 0; i < noduplex_nocolor_combox.Items.Count; i++)
                {
                    if (location_settings.printer_setting_list[0] == noduplex_nocolor_combox.Items[i].ToString())
                    {
                        noduplex_nocolor_combox.SelectedIndex = i;
                        break;
                    }
                }
                for (i = 0; i < duplex_nocolor_combox.Items.Count; i++)
                {
                    if (location_settings.printer_setting_list[1] == duplex_nocolor_combox.Items[i].ToString())
                    {
                        duplex_nocolor_combox.SelectedIndex = i;
                        break;
                    }
                }
                for (i = 0; i < noduplex_color_combox.Items.Count; i++)
                {
                    if (location_settings.printer_setting_list[2] == noduplex_color_combox.Items[i].ToString())
                    {
                        noduplex_color_combox.SelectedIndex = i;
                        break;
                    }
                }
                for (i = 0; i < duplex_color_combox.Items.Count; i++)
                {
                    if (location_settings.printer_setting_list[3] == duplex_color_combox.Items[i].ToString())
                    {
                        duplex_color_combox.SelectedIndex = i;
                        break;
                    }
                }
            }
            printers_setting_dialog.Show();
        }

        private void setting_printers_ensure_Click(object sender, EventArgs e)
        {

            if ((noduplex_nocolor_combox.SelectedItem != null) && (duplex_nocolor_combox.SelectedItem != null) && (noduplex_color_combox.SelectedItem != null) && (duplex_color_combox.SelectedItem != null))
            {
                location_settings.set_printer_setting_list(noduplex_nocolor_combox.SelectedItem.ToString(), duplex_nocolor_combox.SelectedItem.ToString(), noduplex_color_combox.SelectedItem.ToString(), duplex_color_combox.SelectedItem.ToString());
                printers_setting_dialog.Hide();
                MessageBox.Show("打印机设置成功");
            }
            else
            {
                MessageBox.Show("打印机设置失败，请确认已全部设置");
            }
        }

        private void setting_printer_exit_Click(object sender, EventArgs e)
        {
            printers_setting_dialog.Hide();
        }

    }
}