using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Printer
{

    //定义获取api的token
    public struct ToMyToken
    {
        public string sid { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string sch_id { get; set; }
        //public float version { get; set; }
    }


    //声明用户的隐私信息
    //{
    //    "id": "用户id",
    //    "name": "姓名",
    //    "student_number": "学号",
    //    "gender": "性别",
    //    "phone": "真实手机号",
    //    "email": "真实邮箱",
    //    "status": "用户状态标识码"
    // }
    public class userInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string student_number { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string sch_id { get; set; }


    }

    public struct mydata_form
    {
        public string mydata_id { get; set; }
        public string mydata_status { get; set; }
        public string mydata_name { get; set; }
        public string mydata_setting { get; set; }
        public string mydata_userName { get; set; }
        public string mydata_time { get; set; }
        public string mydata_buttontext { get; set; }
        public string mydata_pay_buttontext { get; set; }
        public string mydata_requirements { get; set; }


        //public string mydata_copies { get; set; }



        //public string mydata_doubleside { get; set; }
        //public string mydata_color { get; set; }
        //public string mydata_ppt { get; set; }

    }




}
