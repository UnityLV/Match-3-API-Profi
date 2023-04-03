using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Code.Bank
{
    public class Stars : BankDefault
    {
        [SerializeField] private TMP_Text starsText;
        private const string starsAmount = "starsAmaunt";

        private void Start()
        {
            if (PlayerPrefs.HasKey(starsAmount))
            {
                //Debug.Log(PlayerPrefs.GetInt(starsAmount));
                Add(PlayerPrefs.GetInt(starsAmount));
                starsText.text = Value.ToString();
            }
        }
        private void OnDestroy()
        {
            PlayerPrefs.SetInt(starsAmount, Value);
        }
        public override void Add(int value)
        {
            base.Add(value);
            starsText.text = Value.ToString();
            PlayerPrefs.SetInt(starsAmount, Value);
        }
        public override bool TryRemove(int value)
        {
            if (base.TryRemove(value))
            {
                starsText.text = Value.ToString();
                PlayerPrefs.SetInt(starsAmount, Value);
                return true;
            }
            return false;
        }
    }
}