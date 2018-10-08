using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WFAttendListener
{
    /// <summary>
    /// 监听类
    /// </summary>
    public class ProcessListener
    {
        #region 变量
        /// <summary>
        /// 监听线程
        /// </summary>
        private Thread listenerThread;
        /// <summary>
        /// 监听间隔时间
        /// </summary>
        private int listenerSleepTime = 10;
        /// <summary>
        /// 程序名
        /// </summary>
        private string name;
        /// <summary>
        /// 程序路径
        /// </summary>
        private string path;
        #endregion

        #region 监听操作
        /// <summary>
        /// 启动监听
        /// </summary>
        /// <param name="sleepTime"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public void StartListener(int _sleepTime,string _name,string _path)
        {
            listenerSleepTime = _sleepTime;
            name = _name;
            path = _path;
            StopListener();
            listenerThread = new Thread(listener);
            listenerThread.Start();
            LogHelper.WriteLog(string.Format("正在监听{0}...", name));

        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void StopListener()
        {
            if (listenerThread != null)
            {
                listenerThread.Abort();
                LogHelper.WriteLog(string.Format("停止监听{0}...", name));

            }
        }

        /// <summary>
        /// 监听打开执行程序
        /// </summary>
        private void listener()
        {
            ProcessEntity processEntity = new ProcessEntity(name, path);

            ///循环监听并执行打开程序动作
            while (true)
            {
                if (!processEntity.IsAlive())
                    processEntity.StartProcess();
                Thread.Sleep(listenerSleepTime * 1000);

            }

        }
        #endregion

    }
}
