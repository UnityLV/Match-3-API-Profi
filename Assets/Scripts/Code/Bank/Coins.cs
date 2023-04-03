using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Code.Bank
{
    public class Coins : BankDefault
    {
        [SerializeField] private TMP_Text coinsText;
        private const string coins = "stars";

        private void Start()
        {
            if (PlayerPrefs.HasKey(coins))
            {
                Add(PlayerPrefs.GetInt(coins));
                coinsText.text = Value.ToString();
            }

        }
        private void OnDestroy()
        {
            PlayerPrefs.SetInt(coins, Value);
        }
        public override void Add(int value)
        {
            base.Add(value);
            coinsText.text = Value.ToString();
        }
        public override bool TryRemove(int value)
        {
            if (base.TryRemove(value))
            {
                coinsText.text = Value.ToString();
                return true;
            }
            return false;
        }
    }
}