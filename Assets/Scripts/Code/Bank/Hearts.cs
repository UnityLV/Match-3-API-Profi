using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Code.Bank
{
    public class Hearts : BankDefault
    {
        [SerializeField] private TMP_Text heartsText;
        [SerializeField] private int MaxHearts = 5;
        [SerializeField] private Coins coins;
        [SerializeField] private GameObject heartsBuyButton;
        private TimeSpan time;
        private TimeSpan allHeartsTime;
        private const string hearts = "Hearts";
        private const string heartsTime = "HeartsTime";
        private const string exitTime = "ExitTime";
        private const string AllHeartsTime = "AllHeartsTime";
        static bool isOnlyOne;
        private void Start()
        {
            if (isOnlyOne)
            {
                return;
            }
            isOnlyOne = true;
            if (PlayerPrefs.HasKey(hearts))
            {
                Add(PlayerPrefs.GetInt(hearts));
            }
            else
            {
                Add(MaxHearts);
            }
            if (PlayerPrefs.HasKey(heartsTime))
            {
                string[] dataTimeArray = PlayerPrefs.GetString(exitTime).Split(':');
                TimeSpan difference = DateTime.Now - new DateTime(int.Parse(dataTimeArray[0]), int.Parse(dataTimeArray[1]), int.Parse(dataTimeArray[2]), int.Parse(dataTimeArray[3]), int.Parse(dataTimeArray[4]), 0);
                string[] allTimeArray = PlayerPrefs.GetString(AllHeartsTime).Split(':');
                int seconsPointIndex = allTimeArray[2].IndexOf('.');
                if(seconsPointIndex != -1)
                    allTimeArray[2] = allTimeArray[2].Substring(0, seconsPointIndex);
                allHeartsTime = new TimeSpan(int.Parse(allTimeArray[0]), int.Parse(allTimeArray[1]), int.Parse(allTimeArray[2]));
                if (difference>= allHeartsTime)
                {
                    time = TimeSpan.Zero;
                    allHeartsTime = TimeSpan.Zero;
                    Add(MaxHearts);
                }
                else
                {
                    allHeartsTime = allHeartsTime - difference;
                    string[] timeArray = PlayerPrefs.GetString(heartsTime).Split(':');
                    time = new TimeSpan(int.Parse(timeArray[0])- difference.Hours, int.Parse(timeArray[1])- difference.Minutes, int.Parse(timeArray[2]) - difference.Seconds);
                    StartCoroutine(ShowRemainTime());
                }
            }
        }
        public void BuyHearts(int heartsCost)
        {
            if (coins.TryRemove(heartsCost))
            {
                Add(1);
            }
        }
        public override void Add(int value)
        {
            base.Add(value);
            heartsText.text = Value.ToString();
            PlayerPrefs.SetInt(hearts, Value);
            if (Value >= MaxHearts)
            {
                heartsBuyButton.SetActive(false);
            }
        }
        public override bool TryRemove(int value)
        {
            if (base.TryRemove(value))
            {
                if (time == TimeSpan.Zero)
                {
                    time = new TimeSpan(0, 30, 0);
                    StartCoroutine(ShowRemainTime());
                }
                allHeartsTime += new TimeSpan(0, 30, 0);
                PlayerPrefs.SetInt(hearts, Value);
                return true;
            }
            return false;
        }
        IEnumerator ShowRemainTime()
        {
            while (true)
            {
                if (Value >= MaxHearts)
                {
                    time = TimeSpan.Zero;
                    allHeartsTime = TimeSpan.Zero;
                    heartsBuyButton.SetActive(false);
                    break;
                }
                if (time.Hours == 0)
                {
                    heartsText.text =$"{time.Minutes}:{time.Seconds}";
                }
                else
                {
                    heartsText.text = time.ToString();
                }
                yield return new WaitForSeconds(1);
                time -= new TimeSpan(0, 0, 1);
                allHeartsTime -= new TimeSpan(0, 0, 1);  
                if (time <= TimeSpan.Zero)
                {
                    Add(1);
                    if (Value>= MaxHearts)
                    {
                        break;
                    }
                    else
                    {
                        time = new TimeSpan(0, 30, 0);
                    }
                }
            }
        }
        private void OnDestroy()
        {
            if (time > TimeSpan.Zero)
            {
                PlayerPrefs.SetString(heartsTime, time.ToString());
                PlayerPrefs.SetString(AllHeartsTime, allHeartsTime.ToString());
                PlayerPrefs.SetString(exitTime, $"{DateTime.Now.Year}:{DateTime.Now.Month}:{DateTime.Now.Day}:{DateTime.Now.Hour}:{DateTime.Now.Minute}");
            }

        }
    }
}