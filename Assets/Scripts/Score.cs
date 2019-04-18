
public class Score
{
    private float _score = 0f;
    private float _activeBonusLVL = 1f;
    private float _passiveBonusLVL = 0f;
    /// <summary>
    /// GameScore
    /// </summary>
    public float PropScore
    {
        get { return _score; }
        set { _score = value; }
    }
    /// <summary>
    /// Active bonus value
    /// </summary>
    public float ActiveBonusLvl
    {
        get { return _activeBonusLVL; }
        set { _activeBonusLVL = value; }
    }
    /// <summary>
    /// Passive bonus value
    /// </summary>
    public float PassiveBonusLvl
    {
        get { return _passiveBonusLVL; }
        set { _passiveBonusLVL = value; }
    }
    public override string ToString()
    {
        return $"Score = {_score}\nActiveBonus = {_activeBonusLVL}\nPassiveBonus = {_passiveBonusLVL}.";
    }
}
