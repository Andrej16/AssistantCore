using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantCore.Algorithms;

public class ParallelMaintainCollectionOrder
{
    public int MaintainWithOrder()
    {
        var list = Enumerable.Range(1, 100);
        var resultList = new ConcurrentBag<(long, int)>();

        Parallel.ForEach(list, (item, state, index) =>
        {
            var prime = LongRunningOperation(1000, item);

            resultList.Add((index, prime));
        });

        Console.WriteLine("Maintain the order of the collection:");
        foreach (var item in resultList.OrderBy(r => r.Item1))
        {
            Console.WriteLine(item.Item2);
        }

        return 1;
    }

    /// <summary>
    /// this approach does not preserver source order, output will be unordered
    /// </summary>
    /// <returns></returns>
    public int MaintainWithAsOrdered()
    {
        var list = Enumerable.Range(1, 100);
        var resultList = new ConcurrentBag<int>();

        Parallel.ForEach(list.AsParallel().AsOrdered(), item =>
        {
            var prime = LongRunningOperation(1000, item);

            resultList.Add(item);
        });

        Console.WriteLine("Maintain the order of the collection:");
        foreach (var item in resultList)
        {
            Console.WriteLine(item);
        }

        return 1;
    }

    private int LongRunningOperation(int n, int result)
    {
        int count = 0;
        long a = 2;
        while (count < n)
        {
            long b = 2;
            int prime = 1;
            while (b * b <= a)
            {
                if (a % b == 0)
                {
                    prime = 0;
                    break;
                }
                b++;
            }
            if (prime > 0)
            {
                count++;
            }
            a++;
        }

        return result;
    }
}

