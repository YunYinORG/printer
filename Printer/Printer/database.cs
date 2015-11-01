using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.IO;

namespace Printer
{


    class database
    {
        static public List<ToJsonMy> jsonlist = new List<ToJsonMy>();
        //static public string number_nouse_page = "1";
        static public List<string> err_list = new List<string>();


        static public bool jsonlist_add(JArray ja)
        {
            bool flag = true;
            bool flag_allpaid = true;
            //用于遍历的
            //向jsonList中添加数据，如果json的id已经存在，则flag置为false
            //即不添加，维护jsonList
            for (int i = 0; i < ja.Count; i++)//遍历ja数组
            {
                //bool flag2 = true;
                flag = true;
                foreach (var item in jsonlist)      //此处应当存在一个问题，即循环中jsonlist列表会发生改变，能否使用foreach
                {
                    //已存在的或是已支付的都可不予以考虑
                    if (item.id == ja[i]["id"].ToString())
                    {
                        flag = false;
                        break;
                    }
                }
                if (ja[i]["payed"].ToString() == "1")
                {
                    flag = false;
                }
                if (flag == true)
                {
                    ToJsonMy myJs = new ToJsonMy();
                    myJs.id = ja[i]["id"].ToString();
                    myJs.name = ja[i]["name"].ToString();
                    //myJs.use_id = ja[i]["use"].ToString();
                    //myJs.pri_id = ja[i]["pri_id"].ToString();
                    //myJs.url = ja[i]["url"].ToString();
                    myJs.time = ja[i]["time"].ToString();
                    //myJs.name = ja[i]["name"].ToString();
                    myJs.status = ja[i]["status"].ToString();
                    myJs.copies = ja[i]["copies"].ToString();
                    myJs.use_name = ja[i]["user"].ToString();
                    myJs.double_side = ja[i]["double"].ToString();
                    //myJs.student_number = ja[i]["student_number"].ToString();
                    myJs.color = ja[i]["color"].ToString();
                    myJs.ppt_layout = ja[i]["format"].ToString();
                    myJs.requirements = ja[i]["requirements"].ToString();

                    if (ja[i].ToString().Contains("pro"))
                    {
                        myJs.isfirst = true;
                    }
                    else
                    {
                        myJs.isfirst = false;
                    }
                    string file_url = "";
                    string file_more = "";
                    file_more = API.GetMethod("/printer/task/" + myJs.id);
                    file_url = JObject.Parse(file_more)["info"]["url"].ToString();
                    myJs.use_id = JObject.Parse(file_more)["info"]["use_id"].ToString();
                    if (!file_url.Contains("book"))
                    {
                        myJs.is_ibook = false;

                    }
                    else
                    {
                        myJs.is_ibook = true;

                    }
                    //file_url = "";
                    jsonlist.Add(myJs);
                }
                if (ja[i]["payed"].ToString() != "1")
                {
                    flag_allpaid = false;
                }
            }
            return flag_allpaid;
        }

        static public void jsonlist_refresh()
        {
            //API.myPage = Int32.Parse(number_nouse_page);
            //API.token = location_settings.my.token;
            API.myPage = 1;
            string myJsFile = API.GetMethod("/printer/task?page=" + API.myPage);
            API.myPage += 1;
#if DEBUG
            Console.WriteLine(myJsFile);    //这是为了调试么
#endif
            JObject jo = JObject.Parse(myJsFile);
            JArray ja = jo["info"] as JArray;
            //将JArray类型的ja转化为ToMyJohn对象数组 
            if (ja != null)
            {
                jsonlist_add(ja);
                bool myAdd = (ja.Count == 10);  //主要用于判断是否有下一页
                //这里的逻辑应当仔细考虑
                while (myAdd)
                {
                    //API.token = location_settings.my.token;
                    myJsFile = API.GetMethod("/printer/task?page=" + API.myPage);
#if DEBUG
            Console.WriteLine(myJsFile);    //这是为了调试么
#endif
                    API.myPage += 1;
                    jo = JObject.Parse(myJsFile);
                    ja = jo["info"] as JArray;
                    if (ja == null)
                    {
                        break;
                    }
                    if (ja != null)
                    {
                        jsonlist_add(ja);
                    }
                    if (ja.Count < 10)
                        myAdd = false;
                    else
                        myAdd = true;
                }
            }
        }

        static public ToJsonMy find_myjson(string id)
        {
            ToJsonMy result = null;
            foreach (var item in jsonlist)
            {
                if (item.id == id)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }



    }
}
