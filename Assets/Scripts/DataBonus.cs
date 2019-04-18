using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class DataBonus
{
    [SerializeField, Multiline(1)] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private bool _passive;

    [SerializeField, Space(10)] private Button _button;
    [SerializeField] private Text _textButton;
    [SerializeField] private float _bonus = 1f;
    [SerializeField] private float _bonusMultiplicator = 1f;

    public event Action<DataBonus> BttnEvent;

    public float MultipleBonus()
    {
        return Bonus * BonusMultiplicator;
    }
    public bool IsPassive
    {
        get { return _passive; }
        set { _passive = value; }
    }
    public float BonusMultiplicator
    {
        get { return _bonusMultiplicator; }
        set { _bonusMultiplicator = value; }
    }
    public Button RefButton
    {
        get { return _button; }
        set { _button = value; }
    }

    public float Bonus
    {
        get { return _bonus; }
        set { _bonus = value; }
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public void GetTextBttn()
    {
        _textButton.text = $"{_name}\n{Price * MultipleBonus()}$";
    }
    public void Do()
    {
        BttnEvent?.Invoke(this);
    }
}
