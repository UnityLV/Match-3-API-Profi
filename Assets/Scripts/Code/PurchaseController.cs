using Assets.Scripts.Code.Bank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseController : MonoBehaviour
{
    [SerializeField] private Coins coins;
    public void BuySixThousand()
    {
        coins.Add(6000);
    }
    public void BuyTwelveThousand()
    {
        coins.Add(12000);
    }
    public void BuyTwentyFiveThousand()
    {
        coins.Add(25000);
    }
}
