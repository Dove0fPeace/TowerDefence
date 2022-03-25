using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Health}
        public UpdateSource Source = UpdateSource.Gold;

        private TMP_Text m_MoneyText;
        private void Awake()
        {
            m_MoneyText = GetComponent<TMP_Text>();

            switch(Source)
            {
                case UpdateSource.Gold:
                    TD_Player.OnGoldUpdate += UpdateText;
                    break;

                case UpdateSource.Health:
                    TD_Player.OnLifeUpdate += UpdateText;
                    break;
            }
        }

        private void UpdateText(int value)
        {
            m_MoneyText.text = value.ToString();
        }
    }
}