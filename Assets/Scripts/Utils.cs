using System.Collections.Generic;

public static class Utils
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random range = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            --n;
            int k = range.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
