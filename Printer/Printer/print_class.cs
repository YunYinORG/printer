using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace Printer
{
    //class print_class
    //{
    //    static public void direct_print_file(ToJsonMy file)
    //    {
    //        string filename = "";
    //        filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;

    //        string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
    //        //if ((doc_extension == ".doc") || (doc_extension == ".docx"))
    //        //{
    //        //    filename += ".pdf";
    //        //}
    //        if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
    //        {
    //            //filename += ".pdf";
    //            throw new Exception("ppt文件请设置文件后打印");
    //        }


    //        if (file.copies == "现场打印")
    //        {
    //            throw new Exception("请选择详细设置后打印");
    //        }

    //        PdfDocument doc = new PdfDocument();
    //        doc.LoadFromFile(file.file_SavePath + file.filename);

    //        PrintDialog dialogprint = new PrintDialog();


    //        List<string> printerlist = new List<string>();

    //        string defaultprinter = dialogprint.PrinterSettings.PrinterName;
    //        List<string> printer_use_list = new List<string>();
    //        printer_use_list = remember.ReadTextFileToList(@"printer_setting.sjc");
    //        if (printer_use_list.Count != 4)
    //        {
    //            throw new Exception("请先设置需要使用的打印机");
    //        }

    //        if ((file.color == "0") && (file.double_side == "单面"))
    //        {
    //            dialogprint.PrinterSettings.PrinterName = printer_use_list[0];
    //            dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
    //            dialogprint.PrinterSettings.DefaultPageSettings.Color = false;
    //        }

    //        else if ((file.color == "1") && (file.double_side == "单面"))
    //        {
    //            dialogprint.PrinterSettings.PrinterName = printer_use_list[2];
    //            dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
    //            dialogprint.PrinterSettings.DefaultPageSettings.Color = true;
    //        }
    //        else if ((file.color == "0") && (file.double_side == "双面"))
    //        {
    //            dialogprint.PrinterSettings.PrinterName = printer_use_list[1];
    //            dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
    //            dialogprint.PrinterSettings.DefaultPageSettings.Color = false;

    //        }
    //        else if ((file.color == "1") && (file.double_side == "双面"))
    //        {

    //            dialogprint.PrinterSettings.PrinterName = printer_use_list[3];
    //            dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
    //            dialogprint.PrinterSettings.DefaultPageSettings.Color = true;
    //        }

    //        dialogprint.UseEXDialog = true;
    //        dialogprint.AllowPrintToFile = true;
    //        dialogprint.AllowSomePages = true;
    //        dialogprint.PrinterSettings.MinimumPage = 1;
    //        dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
    //        dialogprint.PrinterSettings.FromPage = 1;
    //        dialogprint.PrinterSettings.Collate = true;
    //        dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

    //        string copy = file.copies.Substring(0, 1);
    //        dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);


    //        doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
    //        doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
    //        doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
    //        PrintDocument printdoc = doc.PrintDocument;

    //        dialogprint.Document = printdoc;
    //        printdoc.Print();
    //        //file.changeStatusById("4");
    //        //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["status"].Value = "已打印";
    //        //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["operation"].Value = "确认付款";


    //    }

    //    static public void setbefore_print_file(ToJsonMy file)
    //    {
    //        //string filename = "";
    //        //filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;
    //        //string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
    //        //if ((doc_extension == ".doc") || (doc_extension == ".docx"))
    //        //{
    //        //    filename += ".pdf";

    //        //}
    //        //if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
    //        //{
    //        //    filename += ".pdf";

    //        //}

    //        PdfDocument doc = new PdfDocument();
    //        doc.LoadFromFile(file.file_SavePath + file.filename);

    //        PrintDialog dialogprint = new PrintDialog();

    //        dialogprint.UseEXDialog = true;
    //        dialogprint.AllowPrintToFile = true;
    //        dialogprint.AllowSomePages = true;
    //        dialogprint.PrinterSettings.MinimumPage = 1;
    //        dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
    //        dialogprint.PrinterSettings.FromPage = 1;
    //        dialogprint.PrinterSettings.Collate = true;
    //        dialogprint.PrinterSettings.ToPage = doc.Pages.Count;
    //        if (file.copies != "现场打印")
    //        {
    //            string copy = file.copies.Substring(0, 1);
    //            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);


    //        }
    //        if (dialogprint.ShowDialog() == DialogResult.OK)
    //        {
    //            doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
    //            doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
    //            doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
    //            PrintDocument printdoc = doc.PrintDocument;

    //            dialogprint.Document = printdoc;
    //            printdoc.Print();
    //            //file.changeStatusById("4");
    //            //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["status"].Value = "已打印";
    //            //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["operation"].Value = "确认付款";
    //        }
    //    }

    //    static public void direct_print_ibook(ToJsonMy file)
    //    {
    //        string filename = "";
    //        filename = location_settings.ibook_path + file.name.Substring(0, file.name.Length - "【店内书】".Length);
    //        if (File.Exists(@filename))
    //        {

    //            PdfDocument doc = new PdfDocument();
    //            doc.LoadFromFile(filename);



    //            PrintDialog dialogprint = new PrintDialog();


    //            List<string> printer_use_list = new List<string>();
    //            printer_use_list = remember.ReadTextFileToList(@"printer_setting.sjc");
    //            if (printer_use_list.Count != 4)
    //            {
    //                throw new Exception("请先设置需要使用的打印机");
    //            }

    //            dialogprint.PrinterSettings.PrinterName = printer_use_list[1];

    //            dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
    //            dialogprint.PrinterSettings.DefaultPageSettings.Color = false;

    //            dialogprint.UseEXDialog = true;
    //            dialogprint.AllowPrintToFile = true;
    //            dialogprint.AllowSomePages = true;
    //            dialogprint.PrinterSettings.MinimumPage = 1;
    //            dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
    //            dialogprint.PrinterSettings.FromPage = 1;
    //            dialogprint.PrinterSettings.Collate = true;
    //            dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

    //            string copy = file.copies.Substring(0, 1);
    //            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);

    //            doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
    //            doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
    //            doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
    //            PrintDocument printdoc = doc.PrintDocument;

    //            dialogprint.Document = printdoc;
    //            printdoc.Print();
    //            //file.changeStatusById("4");
    //            //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["status"].Value = "已打印";
    //            //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["operation"].Value = "确认付款";
    //        }
    //        else
    //        {
    //            MessageBox.Show("本店电子书路径有误，请改正");
    //        }
    //    }

    //    static public void setbefore_print_ibook(ToJsonMy file)
    //    {
    //        string filename = "";

    //        filename = location_settings.ibook_path + file.name.Substring(0, file.name.Length - "【店内书】".Length);
    //        if (File.Exists(@filename))
    //        {

    //            PdfDocument doc = new PdfDocument();
    //            doc.LoadFromFile(filename);
    //            PrintDialog dialogprint = new PrintDialog();
    //            dialogprint.UseEXDialog = true;
    //            dialogprint.AllowPrintToFile = true;
    //            dialogprint.AllowSomePages = true;
    //            dialogprint.PrinterSettings.MinimumPage = 1;
    //            dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
    //            dialogprint.PrinterSettings.FromPage = 1;
    //            dialogprint.PrinterSettings.Collate = true;
    //            dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

    //            string copy = file.copies.Substring(0, 1);
    //            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);
    //            if (dialogprint.ShowDialog() == DialogResult.OK)
    //            {
    //                doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
    //                doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
    //                doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
    //                PrintDocument printdoc = doc.PrintDocument;

    //                dialogprint.Document = printdoc;
    //                printdoc.Print();
    //                //file.changeStatusById("4");
    //                //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["status"].Value = "已打印";
    //                //form.mydata.Rows[form.mydata.CurrentRow.Index].Cells["operation"].Value = "确认付款";
    //            }

    //        }
    //        else
    //        {
    //            MessageBox.Show("本店电子书路径有误，请改正");
    //        }



    //    }

    //}

    class file_print_class
    {
        public login_download form;
        public ToJsonMy file;
        public int RowIndex;

        public void file_print()
        {
            //string doc_extension = Path.GetExtension(file.file_SavePath + file.filename);
            //if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
            //{
            //    throw new Exception("ppt文件请设置文件后打印");
            //}
            //if (file.copies == "现场打印")
            //{
            //    throw new Exception("请选择详细设置后打印");
            //}

            is_print();

            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(file.file_SavePath + file.filename);

            PrintDialog dialogprint = new PrintDialog();


            List<string> printerlist = new List<string>();

            string defaultprinter = dialogprint.PrinterSettings.PrinterName;
            List<string> printer_use_list = new List<string>();
            printer_use_list = remember.ReadTextFileToList(@"printer_setting.sjc");
            if (printer_use_list.Count != 4)
            {
                throw new Exception("请先设置需要使用的打印机");
            }

            if ((file.color == "0") && (file.double_side == "单面"))
            {
                dialogprint.PrinterSettings.PrinterName = printer_use_list[0];
                dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
                dialogprint.PrinterSettings.DefaultPageSettings.Color = false;
            }

            else if ((file.color == "1") && (file.double_side == "单面"))
            {
                dialogprint.PrinterSettings.PrinterName = printer_use_list[2];
                dialogprint.PrinterSettings.Duplex = Duplex.Simplex;
                dialogprint.PrinterSettings.DefaultPageSettings.Color = true;
            }
            else if ((file.color == "0") && (file.double_side == "双面"))
            {
                dialogprint.PrinterSettings.PrinterName = printer_use_list[1];
                dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
                dialogprint.PrinterSettings.DefaultPageSettings.Color = false;

            }
            else if ((file.color == "1") && (file.double_side == "双面"))
            {

                dialogprint.PrinterSettings.PrinterName = printer_use_list[3];
                dialogprint.PrinterSettings.Duplex = Duplex.Vertical;
                dialogprint.PrinterSettings.DefaultPageSettings.Color = true;
            }

            dialogprint.UseEXDialog = true;
            dialogprint.AllowPrintToFile = true;
            dialogprint.AllowSomePages = true;
            dialogprint.PrinterSettings.MinimumPage = 1;
            dialogprint.PrinterSettings.MaximumPage = doc.Pages.Count;
            dialogprint.PrinterSettings.FromPage = 1;
            dialogprint.PrinterSettings.Collate = true;
            dialogprint.PrinterSettings.ToPage = doc.Pages.Count;

            string copy = file.copies.Substring(0, 1);
            dialogprint.PrinterSettings.Copies = (short)Int32.Parse(copy);


            //doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
            //doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
            //doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
            //PrintDocument printdoc = doc.PrintDocument;

            //dialogprint.Document = printdoc;
            //printdoc.Print();

            ensure_print(dialogprint, doc);
        }

        virtual public void is_print() 
        {

        }
        virtual public void ensure_print(PrintDialog dialogprint, PdfDocument doc) { }
    }

    class file_print_direct_class : file_print_class
    {
        public file_print_direct_class() { }
        public file_print_direct_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void is_print()
        {
            base.is_print();
            string doc_extension = Path.GetExtension(file.file_SavePath + file.filename);
            if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
            {
                throw new Exception("ppt文件请设置文件后打印");
            }
            if (file.copies == "现场打印")
            {
                throw new Exception("请选择详细设置后打印");
            }
            if (!file.is_exsist_file)
            {
                //form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                //throw new Exception("当前文件不存在，请重新下载");
                //MessageBox.Show("该文件不存在，正在自动下载！");
                download_single_single_class download_class = new download_single_single_class(form, file, RowIndex);
                download_class.download();
            }
        }
        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
            doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
            doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
            PrintDocument printdoc = doc.PrintDocument;

            dialogprint.Document = printdoc;
            printdoc.Print();
        }
    }

    class file_print_direct_all_class : file_print_direct_class
    {
        public file_print_direct_all_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;

        }
        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            if (file.status == "已下载")
            {
                file.status = "3";
                form.mydata.Rows[RowIndex].Cells["status"].Value = "已打印";
                file.MoveFromListToList(database.jsonlist_downloaded, database.jsonlist_printing);
                file.changeStatusById("3");
            }
        }
        public override void is_print()
        {
            base.is_print();
            if (file.status == "已上传")
            {
                throw new Exception("请先下载文件后打印");
            }
        }

    }

    class file_print_direct_downloaded_class : file_print_direct_class
    {
        public file_print_direct_downloaded_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            file.status = "3";
            form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
            file.MoveFromListToList(database.jsonlist_downloaded, database.jsonlist_printing);
            file.changeStatusById("3");
        }
    }

    class file_print_direct_others_class : file_print_direct_class
    {
        public file_print_direct_others_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
        }
    }



    class file_print_AfterSet_class : file_print_class
    {
        public file_print_AfterSet_class() { }
        public file_print_AfterSet_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void is_print()
        {
            base.is_print();
            if (!file.is_exsist_file)
            {
                //form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                //throw new Exception("当前文件不存在，请重新下载");
                //MessageBox.Show("该文件不存在，正在自动下载！");
                download_single_single_class download_class = new download_single_single_class(form, file, RowIndex);
                download_class.download();
            }
        }
        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
        }
    }

    class file_print_AfterSet_all_class : file_print_AfterSet_class
    {
        public file_print_AfterSet_all_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;

        }
        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            if (dialogprint.ShowDialog() == DialogResult.OK)
            {
                doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
                doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
                doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
                PrintDocument printdoc = doc.PrintDocument;

                dialogprint.Document = printdoc;
                printdoc.Print();
                if (file.status == "已下载")
                {
                    file.status = "3";
                    form.mydata.Rows[RowIndex].Cells["status"].Value = "已打印";
                    file.MoveFromListToList(database.jsonlist_downloaded, database.jsonlist_printing);
                    file.changeStatusById("3");
                }
            }
        }
        public override void is_print()
        {
            base.is_print();
            if (file.status == "已上传")
            {
                throw new Exception("请先下载文件后打印");
            }
        }
    }

    class file_print_AfterSet_downloaded_class : file_print_AfterSet_class
    {
        public file_print_AfterSet_downloaded_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            if (dialogprint.ShowDialog() == DialogResult.OK)
            {
                doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
                doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
                doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
                PrintDocument printdoc = doc.PrintDocument;

                dialogprint.Document = printdoc;
                printdoc.Print();
                file.status = "3";
                form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
                file.MoveFromListToList(database.jsonlist_downloaded, database.jsonlist_printing);
                file.changeStatusById("3");
            }
        }
    }

    class file_print_AfterSet_others_class : file_print_AfterSet_class
    {
        public file_print_AfterSet_others_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }

        public override void ensure_print(PrintDialog dialogprint, PdfDocument doc)
        {
            base.ensure_print(dialogprint, doc);
            if (dialogprint.ShowDialog() == DialogResult.OK)
            {
                doc.PrintFromPage = dialogprint.PrinterSettings.FromPage;
                doc.PrintToPage = dialogprint.PrinterSettings.ToPage;
                doc.PrintDocument.PrinterSettings = dialogprint.PrinterSettings;
                PrintDocument printdoc = doc.PrintDocument;

                dialogprint.Document = printdoc;
                printdoc.Print();
            }
        }
    }
}
