using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraPositioner : MonoBehaviour
{
    [Inject] private Board _board;

    private void Start()
    {
        var center = _board.CenterPoint;
        transform.position = new Vector3(center.x, center.y, transform.position.z);        
    }
}
