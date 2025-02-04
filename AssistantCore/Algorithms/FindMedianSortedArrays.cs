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
        var duplicates = 0;
        for (int i = 0; i < union.Length - 1; i++)
        {
            var current = union[i];
            var next = union[i + 1];

            if (current == next)
            {
                for (int shl = i; shl < union.Length - 1 - duplicates; shl++)
                {
                    union[shl] = union[shl + 1];
                    union[shl + 1] = -1;
                }

                duplicates++;
            } 
        }

        Console.WriteLine("Duplicates union:");
        DisplayArray(union);

        return -1;
    }

    static void DisplayArray(int[] arr)
    {
        foreach (int value in arr)
            Console.Write(value + ", ");

        Console.WriteLine();
    }
}
