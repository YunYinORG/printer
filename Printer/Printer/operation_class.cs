using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Printer
{
    class operation_class
    {
        public login_download form;
        public ToJsonMy file;
        public int RowIndex;
        virtual public void do_operation() { }
    }

    class operation_TellPrinted_class : operation_class
    {
        public operation_TellPrinted_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        override public void do_operation()
        {

            switch (form.display_mode)
            {
                case "mode_downloaded":
                case "mode_printing":
                    file.changeStatusById("4");
                    if (database.jsonlist_downloaded.Contains(file))
                    {
                        database.jsonlist_downloaded.Remove(file);
                    }
                    if (database.jsonlist_printing.Contains(file))
                    {
                        database.jsonlist_printing.Remove(file);
                    }
                    if (!database.jsonlist_printed.Contains(file))
                    {
                        database.jsonlist_printed.Add(file);
                    }
                    form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
                    file.status = "4";
                    break;
                case "mode_all":
                    file.changeStatusById("4");
                    form.mydata.Rows[RowIndex].Cells["status"].Value = "打印完成";
                    if (database.jsonlist_downloaded.Contains(file))
                    {
                        database.jsonlist_downloaded.Remove(file);
                    }
                    if (database.jsonlist_printing.Contains(file))
                    {
                        database.jsonlist_printing.Remove(file);
                    }
                    if (!database.jsonlist_printed.Contains(file))
                    {
                        database.jsonlist_printed.Add(file);

                    }
                    file.status = "4";
                    //form.mydata.Rows[RowIndex].Cells["operation"].Value = "确认付款";
                    break;
                case "mode_downloading":
                    MessageBox.Show("请先下载文件，打印后通知用户");
                    break;
                case "mode_printed":
                    //file.changeStatusById("4");
                    MessageBox.Show("已通知打印完成");
                    break;
                default:
                    MessageBox.Show("err:此操作方式不存在");
                    break;
            }

        }
    }

    class operation_ErrDownload_class : operation_class
    {
        public operation_ErrDownload_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        override public void do_operation()
        {
            switch (form.display_mode)
            {
                case "mode_downloading":
                    download_errfile_downloading_class downloading_class = new download_errfile_downloading_class(form, file, RowIndex);
                    downloading_class.download();
                    break;
                case "mode_all":
                    download_errfile_all_class all_class = new download_errfile_all_class(form, file, RowIndex);
                    all_class.download();
                    break;
                default:
                    MessageBox.Show("err:此种操作方式不存在！");
                    break;
            }
        }
    }

    class operation_ReDownload_class : operation_class
    {
        public operation_ReDownload_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            download_single_single_class download_class = new download_single_single_class(form, file, RowIndex);
            download_class.download();
        }
    }

    class operation_PrintDirect_class : operation_class
    {
        public operation_PrintDirect_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        override public void do_operation()
        {
            try
            {
                //if (!file.is_ibook)
                //{
                //    string filename = "";
                //    filename = location_settings.file_path + "\\" + file.id + "_" + file.copies + "_" + file.double_side + "_" + file.student_number + "_" + file.name;

                //    string doc_extension = Path.GetExtension(location_settings.file_path + "/" + filename);
                //    //if ((doc_extension == ".doc") || (doc_extension == ".docx"))
                //    //{
                //    //    filename += ".pdf";
                //    //}
                //    if ((doc_extension == ".ppt") || (doc_extension == ".pptx"))
                //    {
                //        //filename += ".pdf";
                //        throw new Exception("ppt文件请设置文件后打印");
                //    }

                //    if (file.is_exsist)
                //    {
                //        print_class.direct_print_file(file);
                //        file.changeStatusById("3");
                //        if (!database.jsonlist_printing.Contains(file))
                //        {
                //            database.jsonlist_printing.Add(file);
                //        }
                //        if (database.jsonlist_downloaded.Contains(file))
                //        {
                //            database.jsonlist_downloaded.Remove(file);
                //        }
                //    }
                //    else
                //    {
                //        //download_single_single_class file_download = new download_single_single_class(form, file);
                //        //file_download.download();
                //        form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                //        MessageBox.Show("当前文件不存在，请重新下载");
                //    }
                //}
                //else
                //{
                //    print_class.direct_print_ibook(file);
                //}
                switch (form.display_mode)
                {
                    case "mode_all":
                        file_print_direct_all_class print_all_class = new file_print_direct_all_class(form, file, RowIndex);
                        print_all_class.file_print();
                        break;
                    case "mode_downloaded":
                        file_print_direct_downloaded_class print_downloaded_class = new file_print_direct_downloaded_class(form, file, RowIndex);
                        print_downloaded_class.file_print();
                        break;
                    case "mode_downloading":
                        MessageBox.Show("请先下载文件后打印");
                        break;
                    case "mode_printed":
                    case "mode_printing":
                        file_print_direct_others_class print_others_class = new file_print_direct_others_class(form, file, RowIndex);
                        print_others_class.file_print();
                        break;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打印");
            }
        }
    }

    class operation_PrintAfterSet_class : operation_class
    {
        public operation_PrintAfterSet_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            try
            {
                //if (!file.is_ibook)
                //{
                //    if (file.is_exsist)
                //    {
                //        print_class.setbefore_print_file(file);
                //        file.changeStatusById("3");
                //        if (!database.jsonlist_printing.Contains(file))
                //        {
                //            database.jsonlist_printing.Add(file);
                //        }
                //        if (database.jsonlist_downloaded.Contains(file))
                //        {
                //            database.jsonlist_downloaded.Remove(file);
                //        }
                //    }
                //    else
                //    {
                //        //download_single_single_class file_download = new download_single_single_class(form, file);
                //        //file_download.download();
                //        form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                //        MessageBox.Show("当前文件不存在，请重新下载");
                //    }
                //}
                //else
                //{
                //    print_class.setbefore_print_ibook(file);
                //}
                switch (form.display_mode)
                {
                    case "mode_all":
                        file_print_AfterSet_all_class print_all_class = new file_print_AfterSet_all_class(form, file, RowIndex);
                        print_all_class.file_print();
                        break;
                    case "mode_downloaded":
                        file_print_AfterSet_downloaded_class print_downloaded_class = new file_print_AfterSet_downloaded_class(form, file, RowIndex);
                        print_downloaded_class.file_print();
                        break;
                    case "mode_downloading":
                        MessageBox.Show("请先下载文件后打印");
                        break;
                    case "mode_printed":
                    case "mode_printing":
                        file_print_AfterSet_others_class print_others_class = new file_print_AfterSet_others_class(form, file, RowIndex);
                        print_others_class.file_print();
                        break;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打印");
            }
        }
    }

    class operation_cancel_class : operation_class
    {
        public operation_cancel_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            if (file.cancel())
            {
                form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
            }
        }
    }

    class operation_EnsurePayed_class : operation_class
    {
        public operation_EnsurePayed_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            DialogResult dr = MessageBox.Show("确认付款？", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                file.ensure_payed();
                form.mydata.Rows.Remove(form.mydata.Rows[RowIndex]);
            }
        }
    }

    class operation_GetRowFile_class : operation_class
    {
        public operation_GetRowFile_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            try
            {
                switch (form.display_mode)
                {
                    case "mode_all":
                        download_rawfile_all_class download_all_class = new download_rawfile_all_class(form, file, RowIndex);
                        download_all_class.download();
                        break;
                    case "mode_downloading":
                        download_rawfile_downloading_class download_downloading_class = new download_rawfile_downloading_class(form, file, RowIndex);
                        download_downloading_class.download();
                        break;
                    case "mode_downloaded":
                    case "mode_printing":
                    case "mode_printed":
                        download_rawfile_others_class download_others_class = new download_rawfile_others_class(form, file, RowIndex);
                        download_others_class.download();
                        break;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法获取源文件");
            }

        }
    }

    class operation_OpenFile_class : operation_class
    {
        public operation_OpenFile_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            try
            {
                switch (form.display_mode)
                {
                    case "mode_all":
                        if (file.status == "已上传")
                        {
                            MessageBox.Show("请先下载文件，再打开");
                        }
                        else
                        {
                            if (!file.OpenFile())
                            {
                                MessageBox.Show("该文件不存在，请重新下载！");
                                form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                            }
                        }
                        break;
                    case "mode_downloading":
                        MessageBox.Show("请先下载文件，再打开");
                        break;
                    case "mode_downloaded":
                    case "mode_printing":
                    case "mode_printed":
                        if (!file.OpenFile())
                        {
                            MessageBox.Show("该文件不存在，请重新下载！");
                            form.mydata.Rows[RowIndex].Cells["operation"].Value = "重新下载";
                        }
                        break;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法打开文件");
            }

        }
    }

    class operation_GetUserinfo_class : operation_class
    {
        public operation_GetUserinfo_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            try
            {
                userInfo user = new userInfo();
                user = file.UserMessage;
                MessageBox.Show("用户：" + user.name + "  学号：" + user.student_number + "  手机号：" + user.phone);
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法显示用户信息");
            }

        }
    }

    class operation_GetRequirements_class : operation_class
    {
        public operation_GetRequirements_class(login_download form, ToJsonMy file, int RowIndex)
        {
            this.form = form;
            this.file = file;
            this.RowIndex = RowIndex;
        }
        public override void do_operation()
        {
            try
            {
                if (file.requirements != null)
                {
                    MessageBox.Show(file.requirements, "备注信息");
                }
                else
                {
                    MessageBox.Show("该文件无备注信息");
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "无法显示备注信息");
            }

        }
    }
}