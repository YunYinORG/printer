using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace Printer
{
    public class ToJsonMy
    {
        private string m_double_side;
        private string m_copies;
        private string m_status;



        public string id { get; set; }//文件编号
        public string use_id { get; set; }//
        public string use_name { get; set; }//
        public string student_number { get; set; }//
        public string pri_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string time { get; set; }
        public string lujing { get; set; }
        public bool is_ibook;
        public bool isfirst;

        public string copies
        {
            get
            {
                if (m_copies == "0")
                {
                    return "现场打印";
                }
                else
                    return m_copies + "份";
            }
            set { m_copies = value; }

        }

        public string double_side
        {
            get
            {
                if (m_double_side == "0")
                {
                    return "单面";
                }
                else if (m_double_side == "1")
                {
                    return "双面";
                }
                else
                {
                    return "-";
                }
            }
            set { m_double_side = value; }
        }
        //根据属性的特点，将存储的和得到的status建立起映射关系；
        public string status
        {
            get
            {
                switch (m_status)
                {
                    case "1":
                        return "未下载";
                    case "2":
                    case "download":
                        return "已下载";
                    case "3":
                    case "printing":
                        return "正打印";
                    case "4":
                    case "printed":
                        return "已打印";
                    case "5":
                    case "payed":
                        return "已付款";
                }
                return "0";
            }
            set
            {
                m_status = value;
            }
        }
        public string color { get; set; }
        public string ppt_layout { get; set; }
        public string requirements { get; set; }
        public string strcolor
        {
            get
            {
                if (color == "0")
                {
                    return "黑白";
                }
                else if (color == "1")
                {
                    return "彩印";
                }
                else
                {
                    return "-";
                }
            }
            set
            {
                strcolor = value;
            }
        }
        public string ppt
        {
            get
            {
                switch (ppt_layout)
                {
                    case "0":
                        return "-";
                    case "1":

                        return "1X1";
                    case "2":

                        return "2X3";
                    case "3":

                        return "2X4";
                    case "4":

                        return "3X3";
                }
                return "-";
            }
            set
            {
                ppt = value;
            }
        }



        /// <summary>
        /// 调用状态改变函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentStatus"></param>
        public void changeStatusById(string currentStatus)
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



    }
}
