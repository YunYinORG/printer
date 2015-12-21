using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Printer
{
    abstract class download_class
    {
        public login_download form;
        public ToJsonMy file;

        public void download()
        {
            if (!database.jsonlist_err.Contains(file))
            {
                file.create_FilePath();
                Download();
            }
        }
        abstract public void Download();
    }
    class download_single_class : download_class
    {
        //public ToJsonMy file;
        public download_single_class(login_download form, ToJsonMy file)
        {
            this.form = form;
            this.file = file;

        }

        public override void Download()
        {
            //string filename = this.file.id + "_" + this.file.copies + "_" + this.file.double_side + "_" + this.file.student_number + "_" + this.file.name; 
            //string file_selfpath = this.file.time.Split(' ')[0];
            this.file.url = JObject.Parse(API.GetMethod("/printer/task/" + this.file.id))["info"]["url"].ToString();
            Thread piThread1 = new Thread(delegate()
            {
                WebClient webClient = new WebClient();
                //String pathDoc = "";

                //string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                //if (doc_extension != ".pdf")
                //{
                //    pathDoc = location_settings.file_path + "/" + file_selfpath + "/" + filename + ".pdf";
                //}
                //else
                //{
                //    pathDoc = location_settings.file_path + "/" + file_selfpath + "/" + filename;
                //}
                //添加下载完成后的事件
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(this.file.url), this.file.file_SavePath + this.file.filename, this.file.id);

            });
            piThread1.Start();
        }

        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                //改变文件的状态
                //改变文件在jsonlist中的状态
                //将已经下载的文件添加到mydata中
                this.file.status = "2";
                display.display_single(form, this.file);
                this.file.changeStatusById("2");
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                if (database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Remove(file);
                }
                if (!database.jsonlist_downloaded.Contains(file))
                {
                    database.jsonlist_downloaded.Add(file);
                }

            }
            else
            {

                MessageBox.Show("id=" + file.id + "  " + e.Error.Message + @"\n" + "请手动下载");
                if (!database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Add(file);
                    display.display_single(form, this.file);
                }

            }
        }

    }

    class download_single_single_class : download_class
    {
        //public ToJsonMy file;
        public int RowIndex;
        public download_single_single_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void Download()
        {
            string jsonUrl = API.GetMethod("/printer/task/" + file.id);
            JObject jo = JObject.Parse(jsonUrl);
            ToJsonMy thisOne = new ToJsonMy();
            thisOne.url = (jo)["info"]["url"].ToString();
            WebClient webClient = new WebClient();
            //String pathDoc = filename;
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
            webClient.DownloadFileAsync(new Uri(thisOne.url), file.file_SavePath + file.filename, file.id);
            MessageBox.Show("正在下载该文件！\n请等待，稍后请再次点击打印按钮");
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            if (e.Error == null)//下载成功
            {
                MessageBox.Show("已下载完成\n请再次点击打印按钮");
                form.mydata.Rows[RowIndex].Cells["operation"].Value = "确认打印完成";
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
            }
        }
    }


    class download_list_class
    {
        public List<ToJsonMy> list;
        public login_download form;
        public download_list_class(login_download form, List<ToJsonMy> list)
        {
            this.form = form;
            this.list = list;
        }

        public void download()
        {
            foreach (var item in list)
            {
                if (item.status == "已上传")
                {
                    string file_url = "";
                    string file_more = "";
                    file_more = API.GetMethod("/printer/task/" + item.id);
                    file_url = JObject.Parse(file_more)["info"]["url"].ToString();
                    if (!file_url.Contains("book"))
                    {
                        item.is_ibook = false;
                        download_single_class file_download = new download_single_class(form, item);
                        file_download.download();
                    }
                    else
                    {
                        item.is_ibook = true;
                        item.status = "2";
                        item.changeStatusById("2");
                        this.form.notifyIcon1.Visible = true;
                        this.form.notifyIcon1.ShowBalloonTip(10000);
                        display.display_single(form, item);
                    }
                }
            }
        }
    }

    class download_rawfile_class
    {
        public ToJsonMy file;
        public login_download form;
        public int RowIndex;

        public void download()
        {
            //string filename = this.file.id + "_" + this.file.copies + "_" + this.file.double_side + "_" + this.file.student_number + "_" + this.file.name; ;
            //string file_selfpath = this.file.time.Split(' ')[0];
            file.create_FilePath();
            this.file.url = JObject.Parse(API.GetMethod("/printer/task/" + this.file.id + "/file"))["info"].ToString();
            Thread piThread1 = new Thread(delegate()
            {
                WebClient webClient = new WebClient();
                //添加下载完成后的事件
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(this.file.url), this.file.file_SavePath + this.file.name, this.file.id);

            });
            piThread1.Start();
        }

        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        virtual public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
        }
    }

    class download_rawfile_all_class : download_rawfile_class
    {
        public download_rawfile_all_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        override public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                //改变文件的状态
                //改变文件在jsonlist中的状态
                //将已经下载的文件添加到mydata中
                if (file.status == "已上传")
                {
                    this.file.status = "2";
                    this.file.changeStatusById("2");
                    form.mydata.Rows[RowIndex].Cells["status"].Value = "已下载";
                    file.MoveFromListToList(database.jsonlist_err, database.jsonlist_downloaded);
                }
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                if (File.Exists(this.file.file_SavePath + this.file.name))
                {
                    System.Diagnostics.Process.Start(this.file.file_SavePath + this.file.name);
                }
                else
                {
                    MessageBox.Show("文件下载路径存在问题");
                }
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                //database.err_list.Add(file.id);
            }
        }

    }

    class download_rawfile_downloading_class : download_rawfile_class
    {
        public download_rawfile_downloading_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        override public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                //改变文件的状态
                //改变文件在jsonlist中的状态
                //将已经下载的文件添加到mydata中
                if (file.status == "已上传")
                {
                    this.file.status = "2";
                    this.file.changeStatusById("2");
                    form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
                    file.MoveFromListToList(database.jsonlist_err, database.jsonlist_downloaded);
                }
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                if (File.Exists(this.file.file_SavePath + this.file.name))
                {
                    System.Diagnostics.Process.Start(this.file.file_SavePath + this.file.name);
                }
                else
                {
                    MessageBox.Show("文件下载路径存在问题");
                }
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                //database.err_list.Add(file.id);
            }
        }

    }

    class download_rawfile_others_class : download_rawfile_class
    {
        public download_rawfile_others_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        override public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                //改变文件的状态
                //改变文件在jsonlist中的状态
                //将已经下载的文件添加到mydata中
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                if (File.Exists(this.file.file_SavePath + this.file.name))
                {
                    System.Diagnostics.Process.Start(this.file.file_SavePath + this.file.name);
                }
                else
                {
                    MessageBox.Show("文件下载路径存在问题");
                }
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                //database.err_list.Add(file.id);
            }
        }

    }
        
   


    class download_errfile_class
    {
        public ToJsonMy file;
        public login_download form;
        public int RowIndex;

        public void download()
        {
            file.create_FilePath();
            this.file.url = JObject.Parse(API.GetMethod("/printer/task/" + this.file.id))["info"]["url"].ToString();
            Thread piThread1 = new Thread(delegate()
            {
                WebClient webClient = new WebClient();
                //添加下载完成后的事件
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(this.file.url), this.file.file_SavePath + file.filename, this.file.id);

            });
            piThread1.Start();
        }

        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        virtual public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
        }
    }


    class download_errfile_all_class : download_errfile_class
    {
        //public ToJsonMy file;
        //public login_download form;
        //public int RowIndex;

        public download_errfile_all_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;

        }

        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        override public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                this.file.changeStatusById("2");
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                form.mydata.Rows[RowIndex].Cells["status"].Value = "已下载";
                form.mydata.Rows[RowIndex].Cells["operation"].Value = "确认打印完成";

                if (database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Remove(file);
                }
                if (!database.jsonlist_downloaded.Contains(file))
                {
                    database.jsonlist_downloaded.Add(file);
                }
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                if (!database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Add(file);
                }

            }
        }
    }

    class download_errfile_downloading_class : download_errfile_class
    {
        //public ToJsonMy file;
        //public login_download form;
        //public int RowIndex;

        public download_errfile_downloading_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;

        }

        //webClient下载完成后相应的事件，下载完成后，调用改变状态函数
        override public void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)//下载成功
            {
                this.file.changeStatusById("2");
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);
                form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);

                if (database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Remove(file);
                }
                if (!database.jsonlist_downloaded.Contains(file))
                {
                    database.jsonlist_downloaded.Add(file);
                }
            }
            else
            {
                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);
                if (!database.jsonlist_err.Contains(file))
                {
                    database.jsonlist_err.Add(file);
                }
            }
        }
    }
}

