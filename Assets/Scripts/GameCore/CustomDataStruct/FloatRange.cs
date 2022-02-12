using System;

using Random = System.Random;

namespace GameCore.CustomDataStruct
{
    [Serializable]
    public struct FloatRange
    {
        public float Min;
        public float Max;

        FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandom()
        {
            var randomizer = new Random();
            return randomizer.Next((int)Min, (int)Max);
        }
    }
}
