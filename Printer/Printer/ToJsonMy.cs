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
    public class ToJsonMy
    {
        private string m_double_side;
        private string m_copies;
        private string m_status;


        //public string file_SavePath { get; set; }
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

        public string file_SavePath
        {
            get
            {
                string file_selfpath = time.Split(' ')[0];
                //String pathDoc = "";
                file_selfpath = location_settings.file_path + file_selfpath + "\\";
                return file_selfpath;
            }
            set { }
        }

        public string filename
        {
            get
            {
                string fileName = id + "_" + copies + "_" + double_side + "_" + student_number + "_" + name;
                string doc_extension = Path.GetExtension(fileName);
                if (doc_extension != ".pdf")
                {
                    fileName = fileName + ".pdf";
                }
                return fileName;
            }
        }


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
                    case"0":
                        return "用户取消";
                    case "1":
                        return "已上传";
                    case "2":
                        return "已下载";
                    case "3":
                        return "已打印";
                    case "4":
                        return "打印完成";
                    case"-1":
                        return "打印店取消";

                    //case "5":
                    //case "payed":
                    //    return "已付款";
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
        public bool changeStatusById(string currentStatus)
        {
            //put到服务器状态;/api.php/File/1234?token=xxxxxxxxxxxx 
            //将下载完成的文件id添加到下载完成myDown （ArrayList）中
            //参数： status=>文件状态'uploud','download','printing','printed','payed', 返回操作结果
            string putUrl = @"/printer/task/" + id;
            string putPara = "status=" + currentStatus;
            string resualt = API.PutMethod(putUrl, putPara, new UTF8Encoding());
            if (JObject.Parse(resualt)["status"].ToString() != "1")
            {
                MessageBox.Show((string)JObject.Parse(resualt)["info"]);
                return false;
            }
            else
            {
                return true;
            }

        }

        public void ensure_payed()
        {
            string r = API.PostMethod_noparam("/printer/task/" + id + "/pay", new UTF8Encoding());
            if (JObject.Parse(r)["status"].ToString() != "1")
            {
                MessageBox.Show((string)JObject.Parse(r)["info"]);
            }
            else
            {
                if (database.jsonlist.Contains(this))
                {
                    database.jsonlist.Remove(this);
                }
                if (database.jsonlist_downloaded.Contains(this))
                {
                    database.jsonlist_downloaded.Remove(this);
                }
                if (database.jsonlist_err.Contains(this))
                {
                    database.jsonlist_err.Remove(this);
                }
                if (database.jsonlist_printed.Contains(this))
                {
                    database.jsonlist_printed.Remove(this);
                }
                if (database.jsonlist_printing.Contains(this))
                {
                    database.jsonlist_printing.Remove(this);
                }
            }
        }

        public bool cancel()
        {
            //string r = API.PostMethod_noparam("/printer/task/" + id + "/pay", new UTF8Encoding());
            //if (JObject.Parse(r)["status"].ToString() != "1")
            //{
            //    MessageBox.Show((string)JObject.Parse(r)["info"]);
            //}
            bool result = this.changeStatusById("-1");
            if (result)
            {
                if (database.jsonlist.Contains(this))
                {
                    database.jsonlist.Remove(this);
                }
                if (database.jsonlist_downloaded.Contains(this))
                {
                    database.jsonlist_downloaded.Remove(this);
                }
                if (database.jsonlist_err.Contains(this))
                {
                    database.jsonlist_err.Remove(this);
                }
                if (database.jsonlist_printed.Contains(this))
                {
                    database.jsonlist_printed.Remove(this);
                }
                if (database.jsonlist_printing.Contains(this))
                {
                    database.jsonlist_printing.Remove(this);
                }
            }
            return result;
        }

        public bool is_exsist_file
        {
            get
            {
                if (File.Exists(file_SavePath+filename))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool is_exsist_rawfile
        {
            get
            {
                if (File.Exists(file_SavePath + name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void create_FilePath()
        {
            if(!Directory.Exists(this.file_SavePath))
            {
                Directory.CreateDirectory(this.file_SavePath);
            }
        }

        public void MoveFromListToList(List<ToJsonMy> fromlist, List<ToJsonMy> tolist)
        {
            if (fromlist.Contains(this))
            {
                fromlist.Remove(this);
                if (!tolist.Contains(this))
                {
                    tolist.Add(this);
                }
                else
                {
                    MessageBox.Show("列表中已包含该文件");
                }
            }
            else
            {
                MessageBox.Show("列表中不存在该文件");
            }
        }

        public bool OpenFile()
        {
            if (!this.is_exsist_file)
            {
                return false;
            }
            else
            {
                System.Diagnostics.Process.Start(file_SavePath + filename);
                return true;
            }
        }

        public bool OpenRawFile()
        {
            if (!this.is_exsist_rawfile)
            {
                return false;
            }
            else
            {
                System.Diagnostics.Process.Start(file_SavePath + name);
                return true;
            }
        }

        public userInfo UserMessage
        {
            get
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
        }

    }
}
