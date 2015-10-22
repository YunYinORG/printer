using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace Printer
{
    class location_settings
    {
        static public ToMyToken my;
        static public string file_path { get; set; }
        static public string ibook_path { get; set; }
        static public List<string> printer_setting_list = new List<string>();

        static public void creat_path()
        {
            if (!Directory.Exists(ibook_path))
            {
                Directory.CreateDirectory(ibook_path);
            }
            if (!Directory.Exists(file_path))
            {
                Directory.CreateDirectory(file_path);
            }
        }

        static public void set_printer_setting_list(string noduplex_nocolor_printer, string duplex_nocolor_printer, string noduplex_color_printer, string duplex_color_printer)
        {
            printer_setting_list.Clear();
            printer_setting_list.Add(noduplex_nocolor_printer);
            printer_setting_list.Add(duplex_nocolor_printer);
            printer_setting_list.Add(noduplex_color_printer);
            printer_setting_list.Add(duplex_color_printer);
            File.WriteAllText(@"printer_setting.sjc", "");
            remember.WriteListToTextFile(printer_setting_list, @"printer_setting.sjc");
        }




    }
}
