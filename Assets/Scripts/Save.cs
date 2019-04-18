using System;

namespace MosMos
{
    [Serializable]
    public class Save
    {
        public float score = 0f;
        public float activeBonusLvl = 1f;
        public float passiveBonusLvl = 0f;

        public override string ToString()
        {
            return $"Score = {score}\nActiveBonus = {activeBonusLvl}\nPassiveBonus = {passiveBonusLvl}.";
        }
    }
}