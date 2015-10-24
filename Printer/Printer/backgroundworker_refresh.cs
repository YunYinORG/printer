using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Printer
{
    class backgroundworker_refresh
    {
        static BackgroundWorker bw = new BackgroundWorker();
        login_download form;

        public backgroundworker_refresh(login_download form)
        {
            this.form = form;
        }

        private void refresh_fun(object sender, DoWorkEventArgs e)
        {
            database.jsonlist_refresh();   //获取文件列表    

        }

        void after_refresh_first(object sender, RunWorkerCompletedEventArgs e)
        {
            display.display_list(form, database.jsonlist);
            download_list_class download_jsonlist = new download_list_class(form, database.jsonlist);
            download_jsonlist.download();
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
            //this.label4.Text = e.Result.ToString();
        }

        private void after_refresh(object sender, RunWorkerCompletedEventArgs e)
        {
            download_list_class download_list = new download_list_class(form, database.jsonlist);
            download_list.download();
        }

        public void refresh_first()
        {
            if (!bw.IsBusy)
            {
                bw = new BackgroundWorker();
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(after_refresh_first);
                bw.DoWork += new DoWorkEventHandler(refresh_fun);
                bw.RunWorkerAsync();
            }
        }

        public void refresh_other()
        {
            if (!bw.IsBusy)
            {
                bw = new BackgroundWorker();
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(after_refresh);
                bw.DoWork += new DoWorkEventHandler(refresh_fun);

                bw.RunWorkerAsync();
            }
        }
    }
}
