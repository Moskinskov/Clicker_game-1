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
    /// <summary>
    /// Active\Passive - bonus?
    /// </summary>
    public bool IsPassive
    {
        get { return _passive; }
        set { _passive = value; }
    }
    /// <summary>
    /// Bonus multiply
    /// </summary>
    public float BonusMultiplicator
    {
        get { return _bonusMultiplicator; }
        set { _bonusMultiplicator = value; }
    }
    /// <summary>
    /// Reference of the button in shop
    /// </summary>
    public Button RefButton
    {
        get { return _button; }
        set { _button = value; }
    }
    /// <summary>
    /// Value of bonus
    /// </summary>
    public float Bonus
    {
        get { return _bonus; }
        set { _bonus = value; }
    }
    /// <summary>
    /// Price of bonus
    /// </summary>
    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }
    /// <summary>
    /// Refreshing the text in the shop
    /// </summary>
    public void GetTextBttn()
    {
        _textButton.text = $"{_name}\n{Price * MultipleBonus()}$";
    }
    /// <summary>
    /// What button should be do onTap
    /// </summary>
    public void Do()
    {
        BttnEvent?.Invoke(this);
    }
}
