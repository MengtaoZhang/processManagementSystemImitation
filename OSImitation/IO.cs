using System;
namespace OSImitation
{
    public class IO
    {
        bool printer = true;//初始化占用的IO是true  意思是为公用的
        int counterOfTime; //初始化为200ms

        public IO(bool printer, int counterOfTime)
        {
            this.printer = printer;
            this.counterOfTime = counterOfTime;
        }

        public bool Printer
        {
            get;
            set;
        }

        public int CounterOfTime
        {
            get;
            set;
        }

        public void occupationOfIO()
        {
            this.printer = false;
            //这里应写出使用时间块儿。。。暂时还没学完
        }


        //IO[] myIOs = new IO[10];
    }
}
