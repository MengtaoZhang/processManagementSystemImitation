using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

namespace OSImitation
{
    public class Control
    {
        /*
         * 控制类负责进程状态之间的转换
         * 其中包括CPU运行的方法（生成一个CPU的对象）
        */


        /*
         * 程序入口在这里，到时候可以在program中调用这些静态函数（提前将函数类型修改成这样）           
        */


        public Control()
        {
            CPUImitation myCPU;
        }


        IO[] myIOs = new IO[5];//        新建一个IO设备的对象数组
        int pointerOfMyIOs = 5;

        Memory myMemory = new Memory();   //新建一个内存对象
        
        
        Queue <Process> idleQueue = new Queue <Process>();  //空闲队列

        Queue <Process> firstClassReadyQueue = new Queue<Process>();  //优先级最高的就绪队列
        Queue<Process> secondClassReadyQueue = new Queue<Process>();  //优先级次高的就绪队列
        Queue<Process> thirdClassReadyQueue = new Queue<Process>();  //优先级最低的就绪队列，在该队列中的进程再次进入运行态后设置为非抢占

        Queue<Process> runQueue = new Queue<Process>();

        Queue<Process> blockByIO = new Queue<Process>();  //因为I/O而阻塞的队列
        Queue<Process> blockByMemory = new Queue<Process>();  //因为内存不足而阻塞的队列



        /*
        创建进程，申请内存，申请I/O设备，
            */
        public void createProcess(int IDOfProcess ,int sizeOfProcess, int priority, bool IOApply)
        {
            /*
             * 创建进程
             * 申请（从空闲队列）-->申请资源（模拟）-->填写PCB-->挂就绪队列
             * 
             * warning：基于OS性能和主存局限性的原因，应考虑系统中的进程数量。
             * 当进程还处在新建态时，程序保留在辅存中，通常是在磁盘中          
             * 
             * 
             * 进程创建的条件：
             * 1.新的批处理作业
             * 2.交互登陆
             * 3.OS因为提供一项服务而创建
             * 4.由现有的进程派生
             * 
             * 新建进程时，1.先将PCB放入主存，处于新建态时，程序与数据都 不获得空间
            */

            //先创建进程，若就绪队列允许，则进入就绪队列
            Process myProcess = new Process();   //创建新的进程对象，初始化进程控制块
            myProcess.pcb.IdOfProcess = IDOfProcess;   //给新进程分配一个唯一的进程标识号
            myProcess.pcb.Priority = priority;//初始优先级为3
            idleQueue.Enqueue(myProcess); //加入到空闲队列中
            myProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Create;  //状态修改为新建
            myProcess.pcb.Printer = myIOs[pointerOfMyIOs];
            myProcess.pcb.Printer.Printer = IOApply;
            pointerOfMyIOs--;  //申请IO设备

            Console.WriteLine("YOUR PROCESS IS ON THE WAY");

            myMemory.MemoryInitialization();

            if (Memory.resultOfMemory > 0)  //检测存储空间是否充足
            {

                myMemory.MemoryApply(sizeOfProcess);  //给进程分配空间， 假设申请200个地址
                idleQueue.Dequeue(); //从空闲队列中取出
                firstClassReadyQueue.Enqueue(myProcess);//将进程加载到就绪队列中

                Console.WriteLine("YOUR PEOCESS IS CREATED ");

            }

            else
            {
                Console.WriteLine("Memory is over! Can't create! wait PLZ");
            }



        }//createProcess

        public void runProcess()
        {
            Process tempMyProcess = new Process();

            //分派器将进程进入处理器运行，本系统将其设定的消耗时长假定为使用时长模拟
            if(firstClassReadyQueue.Count > 0)
            {
                tempMyProcess = firstClassReadyQueue.Dequeue();
                runQueue.Enqueue(tempMyProcess);
                tempMyProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Running;
                tempMyProcess.pcb.Priority--;

                Console.WriteLine("YOUR PROCESS IS RUNNING NOW");
            }
            else if(secondClassReadyQueue.Count > 0)
            {
                tempMyProcess = secondClassReadyQueue.Dequeue();
                runQueue.Enqueue(tempMyProcess);
                tempMyProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Running;
                tempMyProcess.pcb.Priority--;

                Console.WriteLine("YOUR PROCESS IS RUNNING NOW");

            }
            else if(thirdClassReadyQueue.Count > 0)
            {
                tempMyProcess = thirdClassReadyQueue.Dequeue();
                runQueue.Enqueue(tempMyProcess);
                tempMyProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Running;

                Console.WriteLine("YOUR PROCESS IS RUNNING NOW");

            }

        } //runProcess

        public void timeOut()
        {
            Process tempOfMyProcess = new Process();
            tempOfMyProcess = runQueue.Dequeue();
            
            if(tempOfMyProcess.pcb.Priority == 3)  //优先度最高的进程超时后返回到次最高级的就绪队列中
            {
                tempOfMyProcess.pcb.Priority--;
                secondClassReadyQueue.Enqueue(tempOfMyProcess);

                Console.WriteLine("YOUR PROCESS IS FACED TIMEOUT");

            }

            if (tempOfMyProcess.pcb.Priority == 2)   //优先级次高的进程超时后返回到最低优先级的就绪队列中
            {
                tempOfMyProcess.pcb.Priority--;
                thirdClassReadyQueue.Enqueue(tempOfMyProcess);

                Console.WriteLine("YOUR PROCESS IS FACED TIMEOUT");
            }

        }

        public void blockedByIO()
        {
            Process tempProcess = new Process();
            tempProcess = runQueue.Dequeue();
            blockByIO.Enqueue(tempProcess);
            tempProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Block;
            myMemory.memoryRelease(200);//默认每个进程新建时申请200个内存位置
            if(tempProcess.pcb.Printer.Printer == false)    //当占用一个IO设备而且被阻塞后，
            {
                tempProcess.pcb.Printer.Printer = true;
                myIOs[pointerOfMyIOs] = null;
                pointerOfMyIOs++;
            }

            Console.WriteLine("YOUR PROCESS IS BLOCKED BY IO DEVICES");
        }

        public void blockedByMemory()
        {
            //TODO
        }//bolockedByMemory

        public void exit()
        {
            Process tempOfProcess = new Process();
            tempOfProcess = runQueue.Dequeue();
            tempOfProcess.pcb.StateOfProcess = EnumerationStateOfProcess.Exit; //从运行队列结束并且释放

            if (tempOfProcess.pcb.Printer.Printer == false)
            {
                tempOfProcess.pcb.Printer.Printer = true;
            }//释放IO

            Memory.resultOfMemory += 20;
            
            Console.Write(this);
            Console.WriteLine("恭喜该进程功成身退！");
        }//exit
    }
}
