using System;
using System.Threading;

namespace AssistantCore.Threading;

public class AutoResetMode
{
    private static EventWaitHandle ewh;
    private static long threadCount = 0;
    private static readonly EventWaitHandle clearCount = new EventWaitHandle(
       false,
       EventResetMode.AutoReset);

    public static int Run()
    {
        ewh = new EventWaitHandle(false, EventResetMode.AutoReset);

        for (int i = 0; i <= 4; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadProc));
            t.Start(i);
        }

        // Wait until all the threads have started and blocked.
        // Interlocked class to guarantee thread safety.
        while (Interlocked.Read(ref threadCount) < 5)
        {
            Thread.Sleep(500);
        }

        // SignalAndWait signals the EventWaitHandle, which releases exactly one thread be-fore resetting, because it was created with AutoReset mode. SignalAndWait then blocks on clearCount, to allow the signaled thread to decrement the count before looping again.
        while (Interlocked.Read(ref threadCount) > 0)
        {
            Console.WriteLine("Press ENTER to release a waiting thread.");
            Console.ReadLine();

            WaitHandle.SignalAndWait(ewh, clearCount);
        }

        Console.WriteLine();

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

        clearCount.Set();
    }
}
