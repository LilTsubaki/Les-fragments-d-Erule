
using System;

public class EruleRandom
{
    private static Random random = new Random(14298);

    public static int RangeValue(int min, int max)
    {
        if(min == max)
        {
            return max;
        }
        return random.Next(min, max+1);
    }
    public static float RangeValue(float min, float max)
    {
        return (float)(min + random.NextDouble() *(max - min));
    }

}
