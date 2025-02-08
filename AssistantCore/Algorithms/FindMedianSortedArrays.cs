using System;

namespace AssistantCore.Algorithms;

public class FindMedianSortedArrays
{
    public static int Run(int[] left, int[] right)
    {
        int[] union = new int[left.Length + right.Length];
        int k = 0, n = 0;

        //Sorting left array
        for (; n < left.Length - 1; n++)
        {
            int current = left[n];

            for (int m = n + 1; m < left.Length; m++)
            {
                if (left[m] < current)
                {
                    left[n] = left[m];
                    left[m] = current;
                    current = left[n];
                }
            }

            union[k++] = left[n];
        }

        union[k++] = left[n];

        //Sorting right array
        n = 0;
        while(n < right.Length - 1)
        {
            int current = right[n];

            for (int m = n + 1; m < right.Length; m++)
            {
                if (right[m] < current)
                {
                    right[n] = right[m];
                    right[m] = current;
                    current = right[n];
                }
            }

            n++;
        }

        //Merging arrays
        int u = 0;
        for (int r = 0; r < right.Length; r++)
        {
            for (; u < left.Length + r; u++)
            {
                if (right[r] < union[u])
                {
                    //Shift union items to right
                    for (int su = left.Length - 1 + r; su >= u; su--)
                    {
                        union[su + 1] = union[su];
                    }
                    union[u] = right[r];

                    break;
                }
            }
        }

        Console.WriteLine("Left array:");
        DisplayArray(left);
        Console.WriteLine("Right array:");
        DisplayArray(right);
        Console.WriteLine("Union array:");
        DisplayArray(union);

        //Remove duplicates
        int duplicates = 0, count = 0;
        for (int i = 0; i < union.Length - duplicates; i++)
        {
            if (union[i] == union[i + 1])
            {
                count = 0;
                var ui = i;

                while (union[i] == union[++ui])
                    count++;

                for (int shl = 1; shl < union.Length - count - i; shl++)
                {
                    union[i + shl] = union[i + shl + count];
                }

                duplicates += count;
            }
        }

        for (int d = union.Length - 1; d >= union.Length - duplicates; d--)
            union[d] = 0;

        Console.WriteLine("Duplicates union:");
        DisplayArray(union);

        //Median
        Array.Resize(ref union, union.Length - duplicates);
        Console.WriteLine("Resized array:");
        DisplayArray(union);

        float med = 0F;
        if (union.Length % 2 == 0)
        {
            med = (float)(union[union.Length / 2 - 1] + union[union.Length / 2]) / 2;    
        }
        else
        {
            med = union[(union.Length - 1) / 2];
        }

        Console.WriteLine($"The Median of two arrays will be: {med}");

        return -1;
    }

    static void DisplayArray(int[] arr)
    {
        foreach (int value in arr)
            Console.Write(value + ", ");

        Console.WriteLine();
    }
}
