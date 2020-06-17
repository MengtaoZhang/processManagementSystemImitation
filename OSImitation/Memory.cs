using System;
namespace OSImitation
{
    public class Memory
    {
        int memorySize = 1000;
        public static int[] memoryImitation = new int[1000];
        public static int pointerOfMemory; //记录内存占用情况  现在还没啥用2333
        public static int resultOfMemory = 1000;  //检测内存是否足够


        public Memory(int memorySize = 1000)
        {
           this.memorySize = memorySize;
        }

        public int MemorySize
        {
            get;
            set;
        }

        //此处利用数组来模拟内存大小，0表示未分配，1表示已分配，先进行初始化

        public  void MemoryInitialization()
        {
        

            for (int i = 0; i < this.memorySize; i++)
            {
                memoryImitation[i] = 0;
            }

            

        }//Method MemoryInitialization



        /*在栈中存储数据时，是从高内存地址向低内存地址填充的。
         * 所以倒着存储       
        */
        public void MemoryApply(int sizeOfApply)
        {

            

            //如果剩余内存足够，则申请下来
            if (resultOfMemory > 0)
            {
                for (int i = resultOfMemory; i > resultOfMemory - sizeOfApply ; i--)
                {
                    memoryImitation[i] = 1;
                    resultOfMemory--;
                }
            }

            //如果剩余内存<申请内存，则申请失败，该进程被阻塞。
            //5.9还未设计完全，因为取消了指针的繁琐设计还未想到完善对策
            else if (resultOfMemory < 0)
            {
                Console.WriteLine("OutOfMemoryException");
                /*
                这里还要写该进程从运行队列转到阻塞队列中的过程
            */
            }
        }//MemoryInitialization

        public void memoryRelease(int sizeOfApply)
        {
            for(int i = resultOfMemory; i < resultOfMemory + sizeOfApply; i++)  //释放内存以将模拟内存的存储内容置为1模拟
            {
                memoryImitation[i] = 0;
                resultOfMemory++;

            }
        }

    }//class
}//namespace
