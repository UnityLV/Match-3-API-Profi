using TMPro;
using UnityEngine;

public class DynamicMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Transform target;
    [SerializeField] private Camera camera;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 offcetTarget;
    [SerializeField] private Vector2 offcetEdge;
    private void Update()
    {
        Vector2 targetSpot = camera.WorldToScreenPoint(target.position);
        Vector2 position = new Vector2(targetSpot.x - Screen.width / 2, targetSpot.y - Screen.height / 2) + offcetTarget;
        if (position.x > Screen.width / 2 - offcetEdge.x)
            position = new Vector2(Screen.width / 2 - offcetEdge.x, position.y);
        if (position.x < -Screen.width / 2 + offcetEdge.x)
            position = new Vector2(-Screen.width / 2 + offcetEdge.x, position.y);
        if (position.y > Screen.height / 2 - offcetEdge.y)
            position = new Vector2(position.x, Screen.height / 2 - offcetEdge.y);
        if (position.y < -Screen.height / 2 + offcetEdge.y)
            position = new Vector2(position.x, - Screen.height / 2 + offcetEdge.y);

        rectTransform.anchoredPosition = position;
        Debug.Log(rectTransform.anchoredPosition);
    }
}
