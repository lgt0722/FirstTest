using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WFAttendListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string listenerInterval = System.Configuration.ConfigurationManager.AppSettings["ListenerInterval"];
            string listenerAppName = System.Configuration.ConfigurationManager.AppSettings["ListenerAppName"];
            if (listenerInterval != "0" && System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + @"\" + listenerAppName + ".exe"))
            {
                ProcessListener processListener = new ProcessListener();
                processListener.StartListener(Convert.ToInt32(listenerInterval), listenerAppName, System.Windows.Forms.Application.StartupPath + @"\" + listenerAppName + ".exe");
            }
        }
    }
}
