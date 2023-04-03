using Assets.Scripts.Code.Bank;
using Assets.Scripts.Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelController : MonoBehaviour, IInit<DialogEnd>
{
    [SerializeField] private VisualNovel visualNovel;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private TasksController tasksController;
    [SerializeField] private Stars stars;

    private IInit<DialogEnd> _dialogEnd;
    private CanvasGroup _visualNovelGroup;
    private event DialogEnd _dialogEndEvent;
    private void Awake()
    {
        _visualNovelGroup = visualNovel.GetComponent<CanvasGroup>();
        _dialogEnd = visualNovel;
        visualNovel.Initialize((_dialogID) => canvasController.MenuDeactivate());
        visualNovel.Initialize((_dialogID) => EndDialog(_dialogID));
    }

    public void StartDialog(int dialogId = -1, bool isTutor = false)
    {
        canvasController.VisualNovelActivate(_visualNovelGroup, true);
        visualNovel.StartNovel(dialogId, isTutor);
    }
    public void EndDialog(int dialogID)
    {
        _dialogEndEvent.Invoke(dialogID);
        if (tasksController.CurrentTask == 0 && stars.Value == 0)
        {
            stars.Add(1);
        }
    }
    private void OnDestroy()
    {
        visualNovel.Deinitialize((_dialogID) => canvasController.MenuDeactivate());
        visualNovel.Deinitialize((_dialogID) => EndDialog(_dialogID));
    }

    public void Initialize(DialogEnd @delegate)
    {
        _dialogEndEvent += @delegate;
    }

    public void Deinitialize(DialogEnd @delegate)
    {
        _dialogEndEvent += @delegate;
    }
}
