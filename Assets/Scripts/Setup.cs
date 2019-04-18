using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MosMos
{
    public class Setup : MonoBehaviour
    {
        [SerializeField] private List<DataBonus> _allBonuses;
        [SerializeField] private Text _scoreText;
        [SerializeField] private GameObject _shoppanel;

        private Score _score = new Score();
        private Save _save = new Save();

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Save"))
            {
                _save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
                _score.PropScore = _save.score;
                _score.ActiveBonusLvl = _save.activeBonusLvl;
                _score.PassiveBonusLvl = _save.passiveBonusLvl;
                print(_save.ToString());
                foreach (var bonus in _allBonuses)
                {
                    if (!bonus.IsPassive && _score.ActiveBonusLvl > 1)
                        bonus.Price *= (int)Mathf.Pow(2, _score.ActiveBonusLvl - 1);
                    if (bonus.IsPassive && _score.PassiveBonusLvl > 0)
                        bonus.Price *= (int)Mathf.Pow(2, _score.PassiveBonusLvl);
                }
            }
            else
            {
                print("Нет сохранения.");
            }

            foreach (var bonus in _allBonuses)
            {
                bonus.BttnEvent += EventAction;
                bonus.GetTextBttn();
            }
        }
        private void Update()
        {
            PassiveСalculate();
        }

        #region OnClick Methods

        public void OnTap()
        {
            _score.PropScore += _score.ActiveBonusLvl;
            if (_scoreText != null)
                _scoreText.text = _score.ScoreText + "$";
        }

        public void ShopBttn()
        {
            _shoppanel.SetActive(!_shoppanel.activeSelf);
        }

        public void PushTheBttn(Button button)
        {
            foreach (var bonus in _allBonuses)
            {
                if (bonus.RefButton == button)
                {
                    bonus.Do();
                }
            }
        }

        #endregion

        #region Private Methods

        private void PassiveСalculate()
        {
            _score.PropScore += _score.PassiveBonusLvl * Time.deltaTime;
            var tempScore = (int)_score.PropScore;
            if (_scoreText != null)
                _scoreText.text = tempScore + "$";
        }


        private void EventAction(DataBonus bonus)
        {
            if (bonus.Price > _score.PropScore)
            {
                Debug.Log("Недостаточно средств для покупки.");
                return;
            }

            _score.PropScore -= bonus.Price;

            foreach (var dataBonus in _allBonuses)
            {
                if (bonus.IsPassive == dataBonus.IsPassive)
                {
                    dataBonus.Price += dataBonus.Price;
                    dataBonus.GetTextBttn();
                }
            }

            if (bonus.IsPassive)
                _score.PassiveBonusLvl += bonus.MultipleBonus();
            if (!bonus.IsPassive)
                _score.ActiveBonusLvl += bonus.MultipleBonus();
        }

        #endregion

        private void OnApplicationQuit()
        {
            _save.score = _score.PropScore;
            _save.activeBonusLvl = _score.ActiveBonusLvl;
            _save.passiveBonusLvl = _score.PassiveBonusLvl;
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(_save));
        }

    }
}