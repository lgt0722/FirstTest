using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAttendListener
{
    /// <summary>
    /// 记录日志信息
    /// </summary>
    class LogHelper
    {
        static object obj = new object();

        public static void WriteLog(string msg)
        {

            lock (obj)
            {
                //日志存放路径
                string dirFolder = Application.StartupPath + @"\\ListenerLog\\" + DateTime.Now.ToString("yyyyMM");
                string filePath = dirFolder + @"\\" + DateTime.Now.ToString("dd") + ".log";
                if (!Directory.Exists(dirFolder))
                    Directory.CreateDirectory(dirFolder);
                //创建文件
                FileStream fs;
                if (File.Exists(filePath))
                    fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                else
                    fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                //写入信息
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   ---   " + msg);
                sw.Close();
                fs.Close();
            }
        }
    }
}
