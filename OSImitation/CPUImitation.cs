using System;
using System.Timers;

namespace OSImitation
{
    public class CPUImitation
    {
        public CPUImitation()
        {
            //int timeSlice = 60000;  //60,000 毫秒  1min
            Timer myTimer = new System.Timers.Timer(60000);
//            Console.WriteLine("myTimer is on!");
//            myTimer.Stop();
//            myTimer.Dispose();
            /*
             * 在这个类设置时钟             //在program中创建cpu对象  然后创建一个主线时间
            */
        }
    }
}
