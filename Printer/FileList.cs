using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Printer
{
    /// <summary>
    /// 文件列表类
    /// </summary>
    public class FileList
    {
        private ObservableCollection<File> file_list;

        /// <summary>
        /// 无参构造函数，初始化列表
        /// </summary>
        public FileList()
        {
            file_list = new ObservableCollection<File>();
        }

        /// <summary>
        /// 增加文件
        /// </summary>
        /// <param name="file"></param>
        public void addFile(File file)
        {
            file_list.Add(file);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public void removeFile(File file)
        {
            file_list.Remove(file);
        }

        /// <summary>
        /// 清空文件列表
        /// </summary>
        public void clearAll()
        {
            file_list.Clear();
        }

        /// <summary>
        /// 返回文件列表，用于Binding
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<File> getList()
        {
            return this.file_list;
        }
    }
}
