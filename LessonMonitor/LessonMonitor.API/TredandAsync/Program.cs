using System;
using System.Collections.Generic;
using System.Threading;
using static ThredandAsync.Program;

namespace ThredandAsync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string result = "";
            //string result1 = "";
            //string result2 = "";

            //var thread1 = new Thread(() => result = DoAction(352));
            //thread1.Name = "thread1";
            //var thread2 = new Thread(new ThreadStart(() => result1 = DoAction(345345)));
            //thread2.Name = "thread2";
            //thread2.IsBackground= true;

            //ThreadPool.QueueUserWorkItem<int>((x) => { result2 = DoAction(1234123); }, 10, true);

            //thread1.Start();
            //thread2.Start();
            for (int i = 0; i < 100; i++)
            {
                var worker = new Worker();

                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());
                ThreadPool.QueueUserWorkItem((x) => worker.AddNumber());

                Console.WriteLine("Press any key to exit");
                Console.ReadLine();

                Console.WriteLine(worker);
            }


        

            //Console.WriteLine(result);
            //Console.WriteLine(result1);
            //Console.WriteLine(result2);
        }
        
       
        public static string PrintThreadInfo(int myThreadId)
        {
            var proccesorId = Thread.GetCurrentProcessorId();
            var name = Thread.CurrentThread.Name;
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var proirity = Thread.CurrentThread.Priority;
            var threadState = Thread.CurrentThread.ThreadState;
            var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
            var isBackground = Thread.CurrentThread.IsBackground;

            //Thread.SpinWait(iteration);
            Console.WriteLine("myThreadId: " + myThreadId);
            Console.WriteLine("proccesorId :" + proccesorId);
            Console.WriteLine("name :" + name);
            Console.WriteLine("threadId :" + threadId);
            Console.WriteLine("proirity :" + proirity);
            Console.WriteLine("threadState :" + threadState);
            Console.WriteLine("isThreadPoolThread :" + isThreadPoolThread);
            Console.WriteLine("isBackground :" + isBackground);
            Console.WriteLine();

            return Guid.NewGuid().ToString();

        }

        public class Worker
        {
            private List<int> _numbers = new List<int>();
            private int _counter = 0;
            private Random _random = new Random();

            private Mutex _mutex = new Mutex();
            private AutoResetEvent _autoResetEvent = new AutoResetEvent(true); 

            public void AddNumber()
            {

                lock (_numbers)
                {
                    _numbers.Add(_random.Next());
                    _counter++;
                }

                var proccesorId = Thread.GetCurrentProcessorId();
                var name = Thread.CurrentThread.Name;
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var proirity = Thread.CurrentThread.Priority;
                var threadState = Thread.CurrentThread.ThreadState;
                var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
                var isBackground = Thread.CurrentThread.IsBackground;


                Console.WriteLine("proccesorId :" + proccesorId);
                Console.WriteLine("name :" + name);
                Console.WriteLine("threadId :" + threadId);
                Console.WriteLine("proirity :" + proirity);
                Console.WriteLine("threadState :" + threadState);
                Console.WriteLine("isThreadPoolThread :" + isThreadPoolThread);
                Console.WriteLine("isBackground :" + isBackground);
                Console.WriteLine();

            }
            public void AddNumberBarier()
            {

                lock (_numbers)
                {
                    _numbers.Add(_random.Next());
                    _counter++;
                }

                var proccesorId = Thread.GetCurrentProcessorId();
                var name = Thread.CurrentThread.Name;
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var proirity = Thread.CurrentThread.Priority;
                var threadState = Thread.CurrentThread.ThreadState;
                var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
                var isBackground = Thread.CurrentThread.IsBackground;


                Console.WriteLine("proccesorId :" + proccesorId);
                Console.WriteLine("name :" + name);
                Console.WriteLine("threadId :" + threadId);
                Console.WriteLine("proirity :" + proirity);
                Console.WriteLine("threadState :" + threadState);
                Console.WriteLine("isThreadPoolThread :" + isThreadPoolThread);
                Console.WriteLine("isBackground :" + isBackground);
                Console.WriteLine();

            }
            public void AddNumberMonitor()
            {
                _autoResetEvent.WaitOne();
                _numbers.Add(_random.Next());
                _counter++;
                _autoResetEvent.Set();
            }
                public void AddNumberAutoResetEvent()
            {
                Monitor.Enter(_numbers);
                try
                {
                    _numbers.Add(_random.Next());
                    _counter++;
                }
                finally
                {
                    Monitor.Enter(_numbers);
                }

                var proccesorId = Thread.GetCurrentProcessorId();
                var name = Thread.CurrentThread.Name;
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var proirity = Thread.CurrentThread.Priority;
                var threadState = Thread.CurrentThread.ThreadState;
                var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
                var isBackground = Thread.CurrentThread.IsBackground;


                Console.WriteLine("proccesorId :" + proccesorId);
                Console.WriteLine("name :" + name);
                Console.WriteLine("threadId :" + threadId);
                Console.WriteLine("proirity :" + proirity);
                Console.WriteLine("threadState :" + threadState);
                Console.WriteLine("isThreadPoolThread :" + isThreadPoolThread);
                Console.WriteLine("isBackground :" + isBackground);
                Console.WriteLine();
            }

            public void AddNumberMutex()
            {
                _mutex.WaitOne();
                _numbers.Add(_random.Next());
                _counter++;
                _mutex.ReleaseMutex();

                var proccesorId = Thread.GetCurrentProcessorId();
                var name = Thread.CurrentThread.Name;
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var proirity = Thread.CurrentThread.Priority;
                var threadState = Thread.CurrentThread.ThreadState;
                var isThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread;
                var isBackground = Thread.CurrentThread.IsBackground;


                Console.WriteLine("proccesorId :" + proccesorId);
                Console.WriteLine("name :" + name);
                Console.WriteLine("threadId :" + threadId);
                Console.WriteLine("proirity :" + proirity);
                Console.WriteLine("threadState :" + threadState);
                Console.WriteLine("isThreadPoolThread :" + isThreadPoolThread);
                Console.WriteLine("isBackground :" + isBackground);
                Console.WriteLine();
            }
           

            public override string ToString()
            {
                return $"counter: {_counter}, numbers: [{string.Join(",",_numbers)}]";
            }
        }
    }
}
