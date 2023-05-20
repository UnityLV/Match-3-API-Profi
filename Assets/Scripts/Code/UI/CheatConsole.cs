using Assets.Scripts.Code.Bank;
using UnityEngine;

namespace Assets.Scripts.Code.UI
{
    public class CheatConsole : MonoBehaviour
    {
        [SerializeField] private Stars stars;
        [SerializeField] private Hearts live;
        [SerializeField] private Coins coins;
        [SerializeField] private CanvasController canvasController;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Decrease decrease;

        private void Start()
        {
            decrease.Initialize(() => canvasController.MenuDeactivate());
        }
        public void CheatActivate()
        {
            canvasController.MenuActivate(canvasGroup);
        }
        public void DeleteAllSaves()
        {
            PlayerPrefs.DeleteAll();
        }
        public void GetStars()
        {
            stars.Add(1);
        }
        public void GetCoins()
        {
            coins.Add(1000);
        }
        public void RemoveAll()
        {
            PlayerPrefs.DeleteAll();

        }
    }
}