using System;
namespace OSImitation
{
    public class ProcessControlBlock
    {
        int idOfProcess;
        EnumerationStateOfProcess stateOfProcess;
        int priority;
        int runtime = 200;  //初始化为200ms
        string beginOfIO = DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒");  //初始化为进程创建的时间
        int lastOfIO = 200;  //初始化为200ms
        IO printer;

        //设计进程的元素
        public ProcessControlBlock(int idOfProcess, EnumerationStateOfProcess stateOfProcess, int priority, int runtime, string beginOfIO, int lastOfIO, IO printer)
        {
            this.idOfProcess = idOfProcess;  //进程的唯一表示符
            this.stateOfProcess = stateOfProcess; //进程的状态
            this.priority = priority;  //进程的优先级
            this.runtime = runtime;  //进程的运行时间
            this.beginOfIO = beginOfIO;  // 进程开始使用I/O的时间
            this.lastOfIO = lastOfIO;  //进程使用I/O的时间
            this.printer = printer;
        }

        public int IdOfProcess
        {
            get;
            set;
        }

        public EnumerationStateOfProcess StateOfProcess
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public int Runtime
        {
            get;
            set;
        }

        public int BeginOfIO
        {
            get;
            set;
        }

        public int LastOfIO
        {
            get;
            set;
        }

        public IO Printer
        {
            get;
            set;
        }



    }//class
}//namespace
