using System;


namespace GameCore.CustomDataStruct
{
    [Serializable]
    public struct ThreeSpecification
    {
        public int Strength;
        public int Agility;
        public int Intelligence;
        

        ThreeSpecification(int strength, int agiliy, int intelligence)
        {
            Strength = strength;
            Agility = agiliy;
            Intelligence = intelligence;
        }


    }

}

