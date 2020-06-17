using System;
namespace OSImitation
{
    public class Process
    {
        public string program;
        public int data;
        public ProcessControlBlock pcb;

        public Process(string program = "Hello World!", int data = 0, ProcessControlBlock pcb = null)
        {
            this.program = program; //进程包含的代码
            this.data = data;//进程包含的数据
            this.pcb = pcb; //进程控制块
        }


        public string Program
        {
            get;
            set;
        }

        public int Data
        {
            get;
            set;
        }

        public ProcessControlBlock Pcb
        {
            get;
            set;
        }

        //还没想好该类里面加什么方法。。。

}
}

