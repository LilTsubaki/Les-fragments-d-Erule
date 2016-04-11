
using System;

public class EruleRandom
{
    private static Random random = new Random(14298);

    public static int RangeValue(int min, int max)
    {
        return random.Next(min, max+1);
    }
    public static float RangeValue(float min, float max)
    {
        return (float)(min + random.NextDouble() *(max+1 - min));
    }

}
