using UnityEngine;

public class RandomUtils
{
    public const int MinPercent = 1;
    public const int MaxPercent = 100;

    public static bool IsSuccess(in int desiredPercent)
    {
        if (desiredPercent < 0 || desiredPercent > MaxPercent)
            throw new System.ArgumentOutOfRangeException(nameof(desiredPercent));

        int randomPercent = Random.Range(MinPercent, MaxPercent + 1);

        return randomPercent <= desiredPercent;
    }

    public static Color GetRandomColor()
    {
        float minColorValue = 0;
        float maxColorValue = 1;

        float redValue = Random.Range(minColorValue, maxColorValue);
        float greenValue = Random.Range(minColorValue, maxColorValue);
        float blueValue = Random.Range(minColorValue, maxColorValue);

        return new Color(redValue, greenValue, blueValue);
    }
}
