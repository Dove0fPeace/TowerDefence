using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    public enum UpdateSource { Gold, Health}
    public UpdateSource Source = UpdateSource.Gold;

    private TMP_Text _moneyText;
    private void Start()
    {
        _moneyText = GetComponent<TMP_Text>();

        switch(Source)
        {
            case UpdateSource.Gold:
                TD_Player.GoldUpdateSubscribe(UpdateText);
                break;

            case UpdateSource.Health:
                TD_Player.HealthUpdateSubscribe(UpdateText);
                break;
        }
    }

    private void UpdateText(int value)
    {
        _moneyText.text = value.ToString();
    }
}