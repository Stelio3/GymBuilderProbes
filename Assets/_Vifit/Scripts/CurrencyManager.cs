using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    int coins;
    public TMPro.TextMeshProUGUI coins_txt;

    private void Start() {
        PlayerStats.F1tc0ins = 1000;
        coins = PlayerStats.F1tc0ins;
        UpdateUI();
    }

    public int GetCoins() {

         return coins;   
    }

    public void AddCoins(int n) {

        coins += n;
        PlayerStats.F1tc0ins = coins;
        UpdateUI();
    }

    public bool RemoveCoins(int n) {

        if (coins-n >= 0) {

            coins -= n;
            UpdateUI();
            PlayerStats.F1tc0ins = coins;
            return true;

        } else {
            return false;
        }
    }

    void UpdateUI() {
         coins_txt.text = string.Format("Your Fitcoins:   {0:n0}", coins);
    }
}
