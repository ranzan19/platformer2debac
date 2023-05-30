using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextEspecialCoins;


    public static void UpdateTextCoins(string s)
    {
        Instance.uiTextCoins.text = s;
    }


    public static void UpdateTextEspecialCoins(string s)
    {
        Instance.uiTextEspecialCoins.text = s;
    }
}
