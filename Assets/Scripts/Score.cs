
public class Score
{
    private float _score = 0;
    private float _activeBonusLVL = 1;
    private float _passiveBonusLVL = 0;

    public float PropScore
    {
        get { return _score; }
        set { _score = value; }
    }

    public string ScoreText
    {
        get { return _score.ToString(); }
    }

    public float ActiveBonusLvl
    {
        get { return _activeBonusLVL; }
        set { _activeBonusLVL = value; }
    }

    public float PassiveBonusLvl
    {
        get { return _passiveBonusLVL; }
        set { _passiveBonusLVL = value; }
    }
}
