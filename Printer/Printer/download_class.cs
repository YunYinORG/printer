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
    class download_class
    {
        public login_download form;

        virtual public void download() { }
    }
    class download_single_class : download_class
    {
        public ToJsonMy file;
        public download_single_class(login_download form, ToJsonMy file)
        {
            this.form = form;
            this.file = file;
        }

        public override void download()
        {
            string filename = this.file.id + "_" + this.file.copies + "_" + this.file.double_side + "_" + this.file.student_number + "_" + this.file.name; ;
            this.file.url = JObject.Parse(API.GetMethod("/file/" + this.file.id))["url"].ToString();
            Thread piThread1 = new Thread(delegate()
            {
                WebClient webClient = new WebClient();
                String pathDoc = "";

                string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                if (doc_extension != ".pdf")
                {
                    pathDoc = location_settings.file_path + "/" + filename + ".pdf";
                }
                else
                {
                    pathDoc = location_settings.file_path + "/" + filename;
                }
                //添加下载完成后的事件
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(this.file.url), pathDoc, this.file.id);

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
                this.file.status = "download";
                display.display_single(form, this.file);
                this.file.changeStatusById("download");
                form.notifyIcon1.Visible = true;
                form.notifyIcon1.ShowBalloonTip(10000);

            }
            else
            {

                MessageBox.Show("id=" + file.id + "  " + e.Error.Message);

            }
        }

    }

    class download_single_single_class : download_class
    {
        public ToJsonMy file;

        public download_single_single_class(login_download form, ToJsonMy file)
        {
            this.form = form;
            this.file = file;
        }

        public override void download()
        {
            base.download();

            string filename = "";
            filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;
            string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
            if (doc_extension != ".pdf")
            {
                filename += ".pdf";
            }

            string jsonUrl = API.GetMethod("/file/" + file.id);
            JObject jo = JObject.Parse(jsonUrl);
            ToJsonMy thisOne = new ToJsonMy();
            thisOne.url = (jo)["url"].ToString();
            WebClient webClient = new WebClient();
            String pathDoc = filename;
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);
            webClient.DownloadFileAsync(new Uri(thisOne.url), pathDoc, file.id);
            MessageBox.Show("正在下载该文件！\n请等待，稍后请再次点击打印按钮");
        }

        private void OnDownloadFileCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("已下载完成\n请再次点击打印按钮");

        }
    }


    class download_list_class : download_class
    {
        public List<ToJsonMy> list;
        public download_list_class(login_download form, List<ToJsonMy> list)
        {
            this.form = form;
            this.list = list;
        }

        public override void download()
        {
            foreach (var item in list)
            {
                if (item.status == "未下载")
                {
                    string file_url = "";
                    string file_more = "";
                    file_more = API.GetMethod("/file/" + item.id);
                    file_url = JObject.Parse(file_more)["url"].ToString();
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
                        item.changeStatusById("download");
                        this.form.notifyIcon1.Visible = true;
                        this.form.notifyIcon1.ShowBalloonTip(10000);
                        display.display_single(form, item);
                    }
                }
            }
        }
    }
}
