using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Printer
{
    abstract class operation_all_class
    {
        public login_download form;
        public List<string> stringlist = new List<string>();
        public void do_operation()
        {
            foreach (DataGridViewRow dr in form.mydata.Rows)
            {
                if (Convert.ToBoolean(dr.Cells["select_idex"].Value) == true)
                {
                    stringlist.Add(dr.Cells["id"].Value.ToString());
                }
            }
            foreach (var item in stringlist)
            {
                string id = item;
                int index = -1;
                ToJsonMy file = database.find_myjson(id);
                foreach (DataGridViewRow dr in form.mydata.Rows)
                {
                    if (dr.Cells["id"].Value.ToString() == id)
                    {
                        index = dr.Index;
                        break;
                    }
                }
                if (index > -1)
                {
                    Do_operation(file, index);
                    //operation_EnsurePayed_class operation = new operation_EnsurePayed_class(form, file, index);
                    //operation.do_operation();
                }
            }
        }

        abstract public void Do_operation(ToJsonMy file, int index);
    }

    class operation_all_EnsurePayed_class : operation_all_class
    {
        public operation_all_EnsurePayed_class(login_download form)
        {
            this.form = form;
        }
        public override void Do_operation(ToJsonMy file, int index)
        {
            operation_EnsurePayed_class operation = new operation_EnsurePayed_class(form, file, index);
            operation.do_operation();
            
        }
    }

    class operation_all_DirectPrint_class : operation_all_class
    {
        public operation_all_DirectPrint_class(login_download form)
        {
            this.form = form;
        }
        public override void Do_operation(ToJsonMy file, int index)
        {
            operation_PrintDirect_class operation = new operation_PrintDirect_class(form, file, index);
            operation.do_operation();

        }
    }

    class operation_all_TellPrinted_class : operation_all_class
    {
        public operation_all_TellPrinted_class(login_download form)
        {
            this.form = form;
        }
        public override void Do_operation(ToJsonMy file, int index)
        {
            operation_TellPrinted_class tellprinted = new operation_TellPrinted_class(form, file, index);
            tellprinted.do_operation();

        }
    }

    class operation_all_cancel_class : operation_all_class
    {
        public operation_all_cancel_class(login_download form)
        {
            this.form = form;
        }
        public override void Do_operation(ToJsonMy file, int index)
        {
            operation_cancel_class operation = new operation_cancel_class(form, file, index);
            operation.do_operation();

        }
    }
}
