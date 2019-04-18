using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MosMos
{
    /// <summary>
    /// Main game class
    /// </summary>
    public class Setup : MonoBehaviour
    {
        [SerializeField, Tooltip("List of shop items")] private List<DataBonus> _allBonuses;
        [SerializeField, Tooltip("ScoreText - link")] private Text _scoreText;
        [SerializeField, Tooltip("ShopPanel - link")] private GameObject _shoppanel;
        [SerializeField] private AudioData _audioData = new AudioData();

        private Score _score = new Score();
        private SaveData _save = new SaveData();
        private XMLData _xmlData;




        private void Awake()
        {
            _xmlData = new XMLData();
            _audioData.AudioDataInit();

            if (File.Exists(_xmlData.SaveLoadPath))
            {
                var tempData = _xmlData.Load();
                _score.PropScore = tempData.score;
                _score.ActiveBonusLvl = tempData.activeBonusLvl;
                _score.PassiveBonusLvl = tempData.passiveBonusLvl;
                print(_score.ToString());

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
        /// <summary>
        /// 'OnTap' - method
        /// </summary>
        public void OnTap()
        {
            _score.PropScore += _score.ActiveBonusLvl;
            if (_scoreText != null)
                _scoreText.text = _score + "$";
            _audioData.Audios[0].Play();
        }
        /// <summary>
        /// On\Off ShopPanel
        /// </summary>
        public void ShopBttn()
        {
            _shoppanel.SetActive(!_shoppanel.activeSelf);
            _audioData.Audios[2].Play();
        }
        /// <summary>
        /// 'Push The Button' - method
        /// </summary>
        /// <param name="button"></param>
        public void PushTheBttn(Button button)
        {
            foreach (var bonus in _allBonuses)
            {
                if (bonus.RefButton == button)
                {
                    bonus.Do();
                }
            }
            _audioData.Audios[1].Play();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Calculate the passive value per\sec
        /// </summary>
        private void PassiveСalculate()
        {
            _score.PropScore += _score.PassiveBonusLvl * Time.deltaTime;
            var tempScore = (int)_score.PropScore;
            if (_scoreText != null)
                _scoreText.text = tempScore + "$";
        }
        /// <summary>
        /// Action event for each button
        /// </summary>
        /// <param name="bonus"></param>
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
            _xmlData.Save(_save);
        }

    }
}