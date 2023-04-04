using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public const string idCamera = "Camera";
    private Camera cameraForMove;
    [SerializeField] private float duration = 1;
    [SerializeField] private float startSpeed = 3;
    private float speed = 1;
    private Vector2 startPosition;
    private Vector2 direction;
    private Vector2 lastNormal;
    private void Start()
    {
        cameraForMove = Camera.main;
        speed = startSpeed;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (cameraForMove == null)
            return;
        DOTween.Kill(idCamera);
        startPosition = cameraForMove.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cameraForMove == null)
            return;
        RaycastHit hit;
        Ray ray = new Ray(cameraForMove.transform.position, cameraForMove.transform.forward * 1000);
        Debug.DrawRay(cameraForMove.transform.position, cameraForMove.transform.forward * 1000, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out CameraZone zone))
            {
                lastNormal = cameraForMove.transform.position;
                speed = startSpeed;
                Debug.Log("плохо");
            }
            speed = 0.5f*startSpeed;
        }
        else
        {
            return;
        }
        direction = startPosition - (Vector2)cameraForMove.ScreenToWorldPoint(eventData.position);
        cameraForMove.transform.Translate(-direction * speed);
        startPosition = cameraForMove.ScreenToWorldPoint(eventData.position);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (cameraForMove == null)
            return;
        RaycastHit hit;
        Ray ray = new Ray(cameraForMove.transform.position, cameraForMove.transform.forward * 1000);
        Debug.Log("рисуем");
        Debug.DrawRay(cameraForMove.transform.position, cameraForMove.transform.forward * 1000, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out CameraStopZone zone))
            {
                cameraForMove.transform.DOMove(new Vector3(lastNormal.x, lastNormal.y, cameraForMove.transform.position.z), duration).SetId(idCamera);
                speed = startSpeed;
            }
        }
        else
        {
            DOTween.Kill(idCamera);
            cameraForMove.transform.DOMove(new Vector3(lastNormal.x, lastNormal.y, cameraForMove.transform.position.z), duration).SetId(idCamera);
            Debug.Log("норм");
        }
    }
}
