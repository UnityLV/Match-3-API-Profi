using UnityEngine;

public class PseudorandomPoints : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    
    public Transform GetPseudorandomPoint()
    {
        return points[Random.Range(0, points.Length)];   
    }
}
