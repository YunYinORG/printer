using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer
{
    /// <summary>
    /// 文件信息管理
    /// </summary>
    public class File
    {
        /// <summary>
        /// 文件信息集合
        /// </summary>
        private string id;
        private string status;
        private string requirement;
        private string file_name;
        private string print_message;
        private string user_name;
        private string time_up;
        private string time_down;
        private string file_path;

        public File(string id, string status, string requirement, string file_name, string print_message, string user_name, string time_up, string time_down, string file_path)
        {
            this.id = id;
            this.status = status;
            this.requirement = requirement;
            this.file_name = file_name;
            this.print_message = print_message;
            this.user_name = user_name;
            this.time_up = time_up;
            this.time_down = time_down;
            this.file_path = file_path;
        }


        public string Id
        {
            get
            {
                return id;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string Requirement
        {
            get
            {
                return requirement;
            }
        }
        public string File_name
        {
            get
            {
                return file_name;
            }
        }
        public string Print_message
        {
            get
            {
                return print_message;
            }
        }
        public string User_name
        {
            get
            {
                return user_name;
            }
        }
        public string Time_up
        {
            get
            {
                return time_up;
            }
        }
        public string Time_down
        {
            get
            {
                return time_down;
            }
            set
            {
                time_down = value;
            }
        }
        public string File_path
        {
            get
            {
                return file_path;
            }
            set
            {
                file_path = value;
            }
        }



    }
}
