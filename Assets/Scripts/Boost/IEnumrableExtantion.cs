using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumrableExtantion
{
    public static T RandomElement<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.RandomElementUsing<T>(new Random());
    }
    public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
    {
        int index = rand.Next(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target)
    {
        Random r = new Random();

        return target.OrderBy(x => (r.Next()));
    }
}
