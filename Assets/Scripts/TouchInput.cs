using UnityEngine;
using UnityEngine.Events;

public class TouchInput : MonoBehaviour
{
    private Camera _camera;

    public event UnityAction<Vector3> PointerDown;
    public event UnityAction<Vector3> PointerUp;
    public event UnityAction<Vector3> PointerDrag;    

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                PointerDown?.Invoke(GetWorldPosition(touch.position));                
            }

            if (touch.phase == TouchPhase.Moved)
            {
                PointerDrag?.Invoke(GetWorldPosition(touch.position));
            }

            if (touch.phase == TouchPhase.Ended)
            {
                PointerUp?.Invoke(GetWorldPosition(touch.position));
            }
        }
    }

    private Vector2 GetWorldPosition(Vector2 screenPosition)
    {
        return _camera.ScreenToWorldPoint(screenPosition);
    }
}
