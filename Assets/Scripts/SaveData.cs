namespace MosMos
{
    public class SaveData
    {
        /// <summary>
        /// GameScore
        /// </summary>
        public float score = 0f;
        /// <summary>
        /// Active bonus value
        /// </summary>
        public float activeBonusLvl = 1f;
        /// <summary>
        /// Passive bonus value
        /// </summary>
        public float passiveBonusLvl = 0f;

        public override string ToString()
        {
            return $"Score = {score}\nActiveBonus = {activeBonusLvl}\nPassiveBonus = {passiveBonusLvl}.";
        }
    }
}