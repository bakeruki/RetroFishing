using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public int money;
    public Text moneyText;

    public void addMoney(int value)
    {
        money += value;
        moneyText.text = "Money: " + money.ToString();
    }
}
