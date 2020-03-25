using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;

namespace P1
{
    class MyProcess
    {
        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;

       /* public static void PrintProcessList()
        {
                Process[] processlist = Process.GetProcesses();
                //double x = 0;
                foreach (Process process in processlist)
                {

                    Console.WriteLine("{0}     {1}", process.Id, process.ProcessName);
                }
        }*/

        public static void PrintProcessList()
        {

            Process[] processlist = Process.GetProcesses();
            double x = 0;

            foreach (Process p in processlist)
            {

                if (lastTime == null)
                {
                    lastTime = DateTime.Now;
                    lastTotalProcessorTime = p.TotalProcessorTime;
                }
                else
                {
                    if (p.Id != 0)
                    {
                        curTime = DateTime.Now;
                        curTotalProcessorTime = p.TotalProcessorTime;

                        Thread.Sleep(10);

                        lastTime = DateTime.Now;
                        lastTotalProcessorTime = p.TotalProcessorTime;

                        double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                        x += CPUUsage;
                        Console.WriteLine("{0}           CPU: {1:0.0}%     ", p.Id, CPUUsage * 100);
                    }
                    else
                    {
                        x = x * 100;
                        Console.WriteLine("{0}           CPU: {1:0.0}%       ", p.Id, (100 - x));
                    }
                }
            }


        }

        public static void ChangeProcessPriority()
        {
            try
            {
                Console.WriteLine("-Enter the ID to change Process Priority : ");
                int x = int.Parse(Console.ReadLine());
                Process pr = Process.GetProcessById(x);

                Console.WriteLine("-Process Priority Now Is:  ");
                Console.WriteLine(pr.PriorityClass);

                Console.WriteLine("-Choose Process Priority you Want Enter 1 or 2 or 3 : \n 1- High \n 2- Idle \n 3- Normal \n  ");
                int num = int.Parse(Console.ReadLine());

                using (Process p = pr)
                {
                    switch (num)
                    {
                        case 1:
                            p.PriorityClass = ProcessPriorityClass.High;
                            Console.WriteLine("Process Priority Changed To " + p.PriorityClass);
                            break;
                        case 2:
                            p.PriorityClass = ProcessPriorityClass.Idle;
                            Console.WriteLine("Process Priority Changed To " + p.PriorityClass);
                            break;
                        case 3:
                            p.PriorityClass = ProcessPriorityClass.Normal;
                            Console.WriteLine("Process Priority Changed To " + p.PriorityClass);
                            break;
                        default:
                            Console.WriteLine("Process Priority NotChanged To " + p.PriorityClass);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Id Not Correct!!!!");
            }
        }

        public static void killProcess()
        {
            int pid;
            Console.WriteLine("-Enter the ID to change Process Priority : ");
            bool isbool = int.TryParse(Console.ReadLine(), out pid);
            if (isbool)
            {
                try
                {
                    Process processToKill = Process.GetProcessById(pid);
                    processToKill.Kill();
                    Console.WriteLine("Pocess Killed ");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error !!!");
                }
            }
            else
                Console.WriteLine("You Must Enter Number !!!!");

        }

        public static void Main()
        {
            Console.WriteLine("*OS Project 1 :");
            Console.WriteLine();

            Console.WriteLine("Enter S to Close Program");
            char ChClose = 'N';
            Console.WriteLine();
            while(Char.ToUpper(ChClose) != 'S')
            {
                Console.WriteLine("Enter 1 or 2 or 3 : \n 1- Print Process List \n 2- Change Process Priority \n 3- kill Process \n");
                Console.WriteLine();
                try
                {

                    int UserEntry;
                    bool isbool = int.TryParse(Console.ReadLine(), out UserEntry);
                    if (isbool)
                    {
                        switch (UserEntry)
                        {
                            case 1:
                                PrintProcessList();
                                break;
                            case 2:
                                ChangeProcessPriority();
                                break;
                            case 3:
                                killProcess();
                                break;
                            default:
                                Console.WriteLine("Not Correct Number !!!!");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("You Must Enter 1 or 2 or 3 ");

                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine();

                    Console.WriteLine("Enter ( S ) to Close Program Or ( C or Any Char ) If Want To Contain :");
                    ChClose = Convert.ToChar(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine("Try Again Error in Char You Entered : ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            //Console.ReadLine();
        }
    }
}





/* Process[] processlist = Process.GetProcesses();
 if (processlist.Length == 0)
 {
     Console.WriteLine(" does not exist");
 }
 else
 {
     for( i=0; i< processlist.Length; i++)

     //foreach (Process process in processlist)
     {

         Process p = processlist[i];
         if (lastTime == null)
         {
             lastTime = DateTime.Now;
             lastTotalProcessorTime = p.TotalProcessorTime;
         }
         else
         {
             curTime = DateTime.Now;
             curTotalProcessorTime = p.TotalProcessorTime;
             double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
             //Console.WriteLine("CPU: {1:0.0}%", CPUUsage * 100);
             lastTime = curTime;
             lastTotalProcessorTime = curTotalProcessorTime;
             Console.WriteLine("Process: {0}          ID: {1}         CPU: {1:0.0}%", processlist[i].ProcessName, processlist[i].Id, (CPUUsage * 100));
         }
     }

    // Process[] pp = Process.GetProcesses();
 // Process p = Process.GetProcessById(123);
 }*/



//public static void PrintProcessList()
//{
//    Process[] processes = Process.GetProcesses();

//    var counters = new List<PerformanceCounter>();

//    foreach (Process process in processes)
//    {
//        var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
//        counter.NextValue();
//        counters.Add(counter);
//        // Console.WriteLine(process.TotalProcessorTime);
//    }

//    int i = 0;

//    Thread.Sleep(1000);

//    foreach (var counter in counters)
//    {
//        Console.WriteLine(processes[i].ProcessName + "       | ID :" + processes[i].Id + "         | CPU% " + counter.NextValue());
//        ++i;
//    }
//}



/*
public static void PrintProcessList(int num)
{

    Console.WriteLine("Press any key to stop...\n");
    while (!Console.KeyAvailable)
    {
        Process[] processlist = Process.GetProcesses();
        double x = 0;
        foreach (Process process in processlist)
        {

            if (lastTime == null)
            {
                lastTime = DateTime.Now;
                lastTotalProcessorTime = process.TotalProcessorTime;
            }
            else
            {
                Process p = Process.GetProcessById(process.Id);
                if (p.Id != 0)
                {
                    curTime = DateTime.Now;
                    curTotalProcessorTime = p.TotalProcessorTime;
                    Console.WriteLine(curTotalProcessorTime);
                    Console.WriteLine(lastTotalProcessorTime);

                    double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                    x += CPUUsage;
                    Console.WriteLine("{0}   CPU: {1:0.0}%     {2}", process.Id, CPUUsage * 100, process.ProcessName);

                    lastTime = DateTime.Now;
                    //Thread.Sleep(1000);
                    lastTotalProcessorTime = curTotalProcessorTime;
                }
                else
                {
                    x = (x / Convert.ToDouble(Environment.ProcessorCount)) * 100;
                    Console.WriteLine("{0}         CPU: {1:0.0}%       {2}", process.Id, 100 - x, process.ProcessName);
                }
            }
        }
        Console.WriteLine("................................................................................................\n");
    }
}*/







/*
static void GetCpuUsage()
{
    //Console.WriteLine("Press any key to stop...\n");
    //while (!Console.KeyAvailable)
    //{
    //    Process process = Process.GetProcessById(num);
    //    double x = 0;

    //        if (lastTime == null)
    //        {
    //            lastTime = DateTime.Now;
    //            lastTotalProcessorTime = process.TotalProcessorTime;
    //        }
    //        else
    //        {
    //            Process p = Process.GetProcessById(process.Id);
    //            if (p.Id != 0)
    //            {
    //                curTime = DateTime.Now;
    //                curTotalProcessorTime = p.TotalProcessorTime;
    //                double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
    //                x += CPUUsage;
    //                Console.WriteLine("{0}   CPU: {1:0.0}%     {2}", process.Id, CPUUsage * 100, process.ProcessName);

    //                lastTime = DateTime.Now;
    //                Thread.Sleep(1000);
    //                lastTotalProcessorTime = curTotalProcessorTime;
    //            }
    //            else
    //            {
    //                x = (x / Convert.ToDouble(Environment.ProcessorCount)) * 100;
    //                Console.WriteLine("{0}         CPU: {1:0.0}%       {2}", process.Id, 100 - x, process.ProcessName);
    //            }
    //        }
    //    }
    //    //Console.WriteLine("................................................................................................\n");

}
*/