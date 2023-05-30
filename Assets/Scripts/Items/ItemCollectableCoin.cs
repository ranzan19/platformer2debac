using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public CoinType coinType = CoinType.NORMAL;


    protected override void OnCollect()
    {
        base.OnCollect();
        if(coinType == CoinType.NORMAL)
        {
            ItemManager.Instance.AddCoins();
        }
        else if(coinType == CoinType.ESPECIAL)
        {
        ItemManager.Instance.AddEspecialCoins();
        }
    }


    public enum CoinType
    {
        NORMAL,
        ESPECIAL
    }
}
