using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Code.UI
{
    public delegate void TaskComplete();
    public class CompleteTask : MonoBehaviour, IInit<TaskComplete>
    {
        [SerializeField] private RectTransform flyCanvas;
        private event TaskComplete _taskComplete;

        public void Initialize(TaskComplete @delegate)
        {
            _taskComplete += @delegate;  
        }
        public void Deinitialize(TaskComplete @delegate)
        {
            _taskComplete -= @delegate;
        }
        private void EndFly()
        {
            _taskComplete?.Invoke();
            flyCanvas.gameObject.SetActive(false);
        }
    }
}