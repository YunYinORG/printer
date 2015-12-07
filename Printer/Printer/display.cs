using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer
{
    class display
    {
        public delegate void Delegate_display(login_download form, mydata_form mydata);
        static public Delegate_display delegate_display;

        static public void display_list(login_download form, List<ToJsonMy> list)
        {
            foreach (var item in list)
            {
                if (item.status != "已上传")
                {
                    display_single(form, item);
                }
            }
        }

        static public void display_list_norefresh(login_download form, List<ToJsonMy> list)
        {
            foreach (var item in list)
            {
                display_single(form, item);
            }
        }

        static public void display_fun(login_download form, mydata_form data)
        {
            form.mydata.Rows.Add(false, data.mydata_id, data.mydata_status,data.mydata_pay_buttontext, data.mydata_requirements, data.mydata_name, data.mydata_setting, data.mydata_userName, data.mydata_time, "一键打印", "设置后打印",data.mydata_buttontext, "取消订单", "打开源文件" );
        }
        static public void display_single(login_download form, ToJsonMy file)
        {

            //mydata_form data = new mydata_form();
            //data.mydata_userName = file.student_number + file.use_name;
            //data.mydata_buttontext = "";
            //data.mydata_name = file.name;
            //data.mydata_id = file.id;
            //data.mydata_time = file.time;
            ////data.mydata_copies = file.copies;

            //if (database.jsonlist_err.Contains(file))
            //{
            //    data.mydata_status = "下载失败";
            //}
            //else
            //{
            //    data.mydata_status = file.status;
            //}

            ////data.mydata_doubleside = file.double_side;
            ////data.mydata_color = file.strcolor;
            ////data.mydata_ppt = file.ppt;


            //if (file.requirements != "")
            //{
            //    data.mydata_name = "(注)" + file.name;

            //}
            //if (file.isfirst == true)
            //{
            //    data.mydata_name = "(首单！)" + file.name;
            //}

            //if (file.copies == "现场打印")
            //{
            //    data.mydata_buttontext = "通知打印完成";

            //    //data.mydata_doubleside = "-";
            //    //data.mydata_color = "-";
            //    //data.mydata_ppt = "-";
            //    data.mydata_setting = "现场打印";
            //    delegate_display = display_fun;
            //    form.mydata.Invoke(delegate_display, new object[] { form, data });
            //}
            //else
            //{
            //    switch (file.status)
            //    {
            //        case "已下载":
            //            data.mydata_buttontext = "通知打印完成";
            //            break;
            //        case "已打印":
            //            data.mydata_buttontext = "通知打印完成";
            //            break;
            //        case "打印完成":
            //            data.mydata_buttontext = "通知打印完成";
            //            break;
            //        case "已上传":
            //            data.mydata_buttontext = "手动下载";
            //            break;
            //    }
            //    if (!file.is_exsist_file)
            //    {
            //        data.mydata_buttontext = "重新下载";
            //    }
            //    data.mydata_setting = file.copies + file.double_side + file.strcolor + file.ppt;

            //    delegate_display = display_fun;
            //    form.mydata.Invoke(delegate_display, new object[] { form, data });

            //}
            switch (form.display_mode)
            {
                case "mode_all":
                    display.Display_Single(form, file);
                    break;
                case "mode_downloading":
                    if (file.status == "已上传")
                    {
                        display.Display_Single(form, file);
                    }
                    break;
                case "mode_downloaded":
                    if (file.status == "已下载")
                    {
                        display.Display_Single(form, file);
                    }
                    break;
                case "mode_printing":
                    if (file.status == "已打印")
                    {
                        display.Display_Single(form, file);
                    }
                    break;
                case "mode_printed":
                    if (file.status == "打印完成")
                    {
                        display.Display_Single(form, file);
                    }
                    break;
                default:
                    break;
            }

        }

        static public void Display_Single(login_download form, ToJsonMy file)
        {
            mydata_form data = new mydata_form();
            data.mydata_userName = file.student_number + file.use_name;
            data.mydata_buttontext = "";
            data.mydata_name = file.name;
            data.mydata_id = file.id;
            data.mydata_time = file.time;
            data.mydata_requirements = file.requirements;
            //data.mydata_copies = file.copies;

            if (database.jsonlist_err.Contains(file))
            {
                data.mydata_status = "下载失败";
            }
            else
            {
                data.mydata_status = file.status;
            }

            //data.mydata_doubleside = file.double_side;
            //data.mydata_color = file.strcolor;
            //data.mydata_ppt = file.ppt;


            if (file.requirements != "")
            {
                data.mydata_name = "(注)" + file.name;

            }
            if (file.isfirst == true)
            {
                data.mydata_name = "(首单！)" + file.name;
            }
            if (file.ispayed == true)
            {
                data.mydata_pay_buttontext = "已支付";
            }
            else
            {
                data.mydata_pay_buttontext = "确认支付";
            }

            if (file.copies == "现场打印")
            {
                data.mydata_buttontext = "通知打印完成";

                //data.mydata_doubleside = "-";
                //data.mydata_color = "-";
                //data.mydata_ppt = "-";
                data.mydata_setting = "现场打印";
                delegate_display = display_fun;
                form.mydata.Invoke(delegate_display, new object[] { form, data });
            }
            else
            {
                switch (file.status)
                {
                    case "已下载":
                        data.mydata_buttontext = "通知打印完成";
                        break;
                    case "已打印":
                        data.mydata_buttontext = "通知打印完成";
                        break;
                    case "打印完成":
                        data.mydata_buttontext = "通知打印完成";
                        break;
                    case "已上传":
                        data.mydata_buttontext = "手动下载";
                        break;
                }
                if (!file.is_exsist_file)
                {
                    data.mydata_buttontext = "重新下载";
                }
                data.mydata_setting = file.copies + file.double_side + file.strcolor + file.ppt;

                delegate_display = display_fun;
                form.mydata.Invoke(delegate_display, new object[] { form, data });

            }
        }

    }



}
