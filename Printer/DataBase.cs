using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer
{
    public class DataBase
    {
        public FileList fileList_All;
        public FileList fileList_nondownload;
        public FileList fileList_download;
        public FileList fileList_print;
        public FileList fileList_error;

        /// <summary>
        /// 无参构造函数，初始化数据库
        /// </summary>
        public DataBase()
        {
            fileList_All = new FileList();
            fileList_nondownload = new FileList();
            fileList_download = new FileList();
            fileList_print = new FileList();
            fileList_error = new FileList();
        }

        /// <summary>
        /// 刷新数据库数据
        /// </summary>
        public void refreshAll()
        {
            fileList_nondownload.clearAll();
            fileList_download.clearAll();
            fileList_print.clearAll();
            fileList_error.clearAll();
            foreach (var item in fileList_All.getList())
            {
                switch (item.Status)
                {
                    case "nondownload":
                        fileList_nondownload.addFile(item);
                        break;
                    case "download":
                        fileList_download.addFile(item);
                        break;
                    case "print":
                        fileList_print.addFile(item);
                        break;
                    case "error":
                        fileList_error.addFile(item);
                        break;
                    default:
                        break;
                }
            }
        }



    }
}
