using System;
using System.Threading;

namespace AssistantCore.Threading;

public class ManualResetMode
{
    private static EventWaitHandle ewh;
    private static long threadCount = 0;

    [MTAThread]
    public static int Run()
    {
        ewh = new EventWaitHandle(false, EventResetMode.ManualReset);

        for (int i = 0; i <= 4; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
            t.Start(i);
        }

        // Wait until all the threads have started and blocked.
        while (Interlocked.Read(ref threadCount) < 5)
        {
            Thread.Sleep(500);
        }

        // Because the EventWaitHandle was created with
        // ManualReset mode, signaling it releases all the
        // waiting threads.
        Console.WriteLine("Press ENTER to release the waiting threads.");
        Console.ReadLine();

        ewh.Set();

        return 1;
    }

    public static void ThreadProc(object data)
    {
        int index = (int)data;

        Console.WriteLine("Thread {0} blocks.", data);
        Interlocked.Increment(ref threadCount);

        ewh.WaitOne();

        Console.WriteLine("Thread {0} exits.", data);
        Interlocked.Decrement(ref threadCount);
    }
}
