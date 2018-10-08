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
    /// 监听的程序信息
    /// </summary>
    public class ProcessEntity
    {
        #region 属性
        /// <summary>
        /// 程序名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 执行程序的路径
        /// </summary>
        public string path { get; set; }
        #endregion

        #region 构造函数
        public ProcessEntity(string _name,string _path)
        {
            name = _name;
            path = _path;
        }
        #endregion

        #region 操作程序
        /// <summary>
        /// 启动程序
        /// </summary>
        public void StartProcess()
        {
            StopProcess();
            Thread.Sleep(500);
            if (!System.IO.File.Exists(path))
                return;
            Process.Start(path);
            LogHelper.WriteLog(string.Format("{0}启动成功...",name));
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        public void StopProcess()
        {
            if (GetProcess() != null)
            {
                GetProcess().Kill();
                LogHelper.WriteLog(string.Format("准备重启{0}...",name));
            }
        }

        /// <summary>
        /// 程序是否在运行
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            Process process = GetProcess();
            if (process != null)
            {
                if (process.HasExited)//进程已终止
                {
                    LogHelper.WriteLog(string.Format("监测到{0}已停止运行...",name));
                    return false;
                }
                else//程序是否响应
                {
                    if (!process.Responding)
                    {
                        LogHelper.WriteLog(string.Format("监测到{0}无响应...",name));
                    }
                    return process.Responding;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取要执行的程序
        /// </summary>
        /// <returns></returns>
        private Process GetProcess()
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length >= 1)
            {
                path = processes[0].MainModule.FileName;
                return processes[0];
            }
            LogHelper.WriteLog(string.Format("监测到{0}未在运行...",name));
            return null;
        }
        #endregion

    }
}
