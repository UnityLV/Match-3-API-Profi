using Assets.Scripts.Code.Bank;
using Assets.Scripts.Code.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class TasksController : MonoBehaviour
{
    public int CurrentTask => _currentTask;

    private const string TaskNumber = "TaskNumber";
    private const string SecondTask = "SecondTaskOnPlayer";
    private const string SecondTaskActivate = "SecondTaskActivate";
    [Header("Task")]
    [SerializeField] private Image task;
    [SerializeField] private Image taskImage;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private TMP_Text taskCostText;

    [Header("SecondTask")]
    [SerializeField] private Image secondTask;
    [SerializeField] private Image secondTaskImage;
    [SerializeField] private TMP_Text secondTaskText;
    [SerializeField] private TMP_Text secondTaskCostText;
    [Header("Other")]
    [SerializeField] private CompleteTask completeTask;
    [SerializeField] private CompleteTask sacondCompleteTask;
    [SerializeField] private BarTask barTask;
    [SerializeField] private int percentAward = 6;
    [SerializeField] private Stars stars;
    [SerializeField] private TaskScriptableObject[] taskScriptableObjects;
    [SerializeField] private TaskScriptableObject[] secondScriptableObjects;
    [SerializeField] private NovelController novelController;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private RoomForFilling roomForFilling;

    private int _currentTask;
    private bool _isTwoActive;
    private bool _isOnlySecondActive;
    private bool _isSaveSecond;
    private TaskScriptableObject _currentTaskScriptableObject;
    private IInit<TaskComplete> _taskComplite;
    private IInit<BarRefreshed> _barRefreshed;
    private bool isTaskCompleteStartButNotEnd;
    private bool isTaskCompleteButBarNotRefreshedYet;
    private bool isSecondTaskExecute;
    private void Start()
    {
        _taskComplite = completeTask;
        _barRefreshed = barTask;
        _taskComplite.Initialize(OnTaskComplete);
        sacondCompleteTask.Initialize(OnSecondTaskComplete);
        _barRefreshed.Initialize(OnBarRefreshed);
        if (PlayerPrefs.HasKey(TaskNumber))
            _currentTask = PlayerPrefs.GetInt(TaskNumber);
        if (PlayerPrefs.HasKey(SecondTask))
        {
            if(PlayerPrefs.GetInt(SecondTask) == 1)
            {
                secondTask.gameObject.SetActive(true);
                RefreshSecondTask();
                _isTwoActive = true;
            }
        }
        if (PlayerPrefs.HasKey(SecondTaskActivate))
        {
            if (PlayerPrefs.GetInt(SecondTaskActivate) == 1)
            {
                secondTask.gameObject.SetActive(true);
                task.gameObject.SetActive(false);
                RefreshSecondTask();
                _isTwoActive = false;
            }
        }
    }
    public void TryToCompleteCurentTask()
    {
        TryToComplete(GetCurrentTask());
    }
    public void TryToCompleteSecondTask()
    {
        isSecondTaskExecute = true;
        CompleteTask star = completeTask;
        if (_isTwoActive)
            star = sacondCompleteTask;
        TryToComplete(secondScriptableObjects[0], star);
    }
    public void TryToComplete(TaskScriptableObject task, CompleteTask star = null) 
    {
        if (isTaskCompleteStartButNotEnd || isTaskCompleteButBarNotRefreshedYet)
        {
            isSecondTaskExecute = false;
            return;
        }
        if (stars.TryRemove(task.Cost))
        {
            _currentTaskScriptableObject = task;
            if(star == null)
                completeTask.gameObject.SetActive(true);
            else
                star.gameObject.SetActive(true);
            isTaskCompleteStartButNotEnd = true;
        }
        else
        {
            isSecondTaskExecute = false;
        }
    }
    public TaskScriptableObject GetCurrentTask()
    {
       return taskScriptableObjects[_currentTask];
    }
    public void OnTaskComplete()
    {
        isTaskCompleteStartButNotEnd = false;
        if (!_isOnlySecondActive)
            _currentTask++;
        PlayerPrefs.SetInt(TaskNumber, _currentTask);
        barTask.Add(percentAward);
        isTaskCompleteButBarNotRefreshedYet = true;
        barTask.RefreshBar();
    }
    public void OnSecondTaskComplete()
    {
        isTaskCompleteStartButNotEnd = false;
        barTask.Add(percentAward);
        isTaskCompleteButBarNotRefreshedYet = true;
        barTask.RefreshBar();
    }
    public void RefreshTask()
    {
        if (CurrentTask >= taskScriptableObjects.Length)
        {
            return;
        }
        taskImage.sprite = taskScriptableObjects[CurrentTask].TaskImage;
        taskText.text = taskScriptableObjects[CurrentTask].Task;
        taskCostText.text = taskScriptableObjects[CurrentTask].Cost.ToString();
    }
    public void RefreshSecondTask()
    {
        if (CurrentTask >= taskScriptableObjects.Length)
        {
            isSecondTaskExecute = false;
            return;
        }
        secondTaskImage.sprite = secondScriptableObjects[0].TaskImage;
        secondTaskText.text = secondScriptableObjects[0].Task;
        secondTaskCostText.text = secondScriptableObjects[0].Cost.ToString();
    }
    public void OnBarRefreshed()
    {
        if (isTaskCompleteButBarNotRefreshedYet)
        {
            isTaskCompleteButBarNotRefreshedYet = false;
            if (_currentTask != 1)
            {
                _isSaveSecond = false;
                canvasController.MenuDeactivate();
                if (isSecondTaskExecute)
                {
                    secondTask.gameObject.SetActive(false);
                    isSecondTaskExecute = false;
                    if (!_isTwoActive)
                    {
                        canvasController.FlyAwayAllOffButOne(null);
                        task.gameObject.SetActive(true);
                        roomForFilling.ItemsSwitchNext(() => StartCoroutine(StartNovel(0)));
                    }
                    else
                    {
                        roomForFilling.ItemsSwitchNext(null);
                    }
                    _isOnlySecondActive = false;
                    _isTwoActive = false;
                    return;
                }
                RefreshTask();
                if (!_isTwoActive)
                {
                    canvasController.FlyAwayAllOffButOne(null);
                    roomForFilling.ItemsSwitch(() => StartCoroutine(StartNovel(0)));
                }
                else
                {
                    task.gameObject.SetActive(false);
                    roomForFilling.ItemsSwitch(null);
                    _isOnlySecondActive = true;
                }
                _isTwoActive = false;
                if (_currentTask == 5)
                {
                    secondTask.gameObject.SetActive(true);
                    RefreshSecondTask();
                    _isTwoActive = true;
                    _isSaveSecond = true;
                }
            }
            else
            {
                StartCoroutine(StartNovel(0));
            }
        }
    }
    IEnumerator StartNovel(float delay)
    {
            yield return new WaitForSeconds(delay);
            novelController.StartDialog(taskScriptableObjects[_currentTask-1].DialogNumber);
    }
    private void OnApplicationQuit()
    {
        _taskComplite?.Deinitialize(OnTaskComplete);
        _barRefreshed?.Deinitialize(OnBarRefreshed);
        PlayerPrefs.SetInt(SecondTask, _isSaveSecond? 1: 0);
        PlayerPrefs.SetInt(SecondTaskActivate, _isOnlySecondActive ? 1 : 0);

        if (isTaskCompleteStartButNotEnd)
        {
            CanselTask();
            return;
        }
        if (isTaskCompleteButBarNotRefreshedYet)
        {
            CanselTask();
            return;
        }
    }
    private void CanselTask()
    {
        _currentTask--;
        PlayerPrefs.SetInt(TaskNumber, _currentTask);
        PlayerPrefs.SetInt(SecondTask, 0);
        barTask.TryRemove(percentAward);
        stars.Add(_currentTaskScriptableObject.Cost);
    }
}
