using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer
{
    class display
    {
        static public void display_list(login_download form, List<ToJsonMy> list)
        {
            foreach (var item in list)
            {
                if (item.status != "未下载")
                {
                    display_single(form, item);
                }
            }
        }

        static public void display_single(login_download form, ToJsonMy file)
        {
            mydata_form data = new mydata_form();
            data.mydata_userName = file.student_number + file.use_name;
            data.mydata_buttontext = "";
            data.mydata_name = file.name;
            data.mydata_id = file.id;
            data.mydata_time = file.time;
            data.mydata_copies = file.copies;
            data.mydata_status = file.status;
            data.mydata_doubleside = file.double_side;
            data.mydata_color = file.strcolor;
            data.mydata_ppt = file.ppt;


            if (file.requirements != "")
            {
                data.mydata_name = "(注)" + file.name;

            }
            if (file.isfirst == true)
            {
                data.mydata_name = "(首单！)" + file.name;
            }

            if (file.copies == "现场打印")
            {
                data.mydata_buttontext = "确认付款";

                //Printer.login_download.mydata.Rows.Add(json.id, userName1, name, json.copies, "-", "-", "-", json.time, json.status, buttontext);
                data.mydata_doubleside = "-";
                data.mydata_color = "-";
                data.mydata_ppt = "-";
                form.mydata.Rows.Add(data.mydata_id, data.mydata_userName, data.mydata_name, data.mydata_copies, data.mydata_doubleside, data.mydata_color, data.mydata_ppt, data.mydata_time, data.mydata_status, data.mydata_buttontext);

            }
            else
            {
                switch (file.status)
                {
                    case "已下载":
                        data.mydata_buttontext = "通知已打印";
                        break;
                    case "已打印":
                        data.mydata_buttontext = "确认付款";
                        break;
                }

                //this.mydata.Rows.Add(json.id, userName1, name, json.copies, json.double_side, json.strcolor, json.ppt, json.time, json.status, buttontext);
                form.mydata.Rows.Add(data.mydata_id, data.mydata_userName, data.mydata_name, data.mydata_copies, data.mydata_doubleside, data.mydata_color, data.mydata_ppt, data.mydata_time, data.mydata_status, data.mydata_buttontext);
            }
        }
    }
}
