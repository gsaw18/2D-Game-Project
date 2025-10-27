using UnityEngine;

//Room
//Holds references for a single "room"
//Although the map is essentially one giant room, this script helps divide the map into three "Rooms"
//Camera pans to each room upon entering
public class Room : MonoBehaviour
{
    public PolygonCollider2D bounds;
    public Transform playerSpawn;

    void OnValidate()
    {
        if (!bounds) bounds = GetComponentInChildren<PolygonCollider2D>();
    }
}
