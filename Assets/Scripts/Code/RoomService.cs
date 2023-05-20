using UnityEngine;

public class RoomService : MonoBehaviour
{
    public RoomItems[] roomItems;
    public RoomItems[] roomSecondItems;
    void Start()
    {
        RoomForFilling.instance.SetRooms(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
