using UnityEngine;
using Unity.Cinemachine;

//RoomManager
//Controller for movement between rooms
public class RoomManager : MonoBehaviour
{
    [Header("Scene Refs")]
    public Room[] rooms; //divide map up into _ rooms 
    public GameObject player; //player
    public CinemachineCamera vcam; //camera
    public CinemachineConfiner2D confiner; //confiner to restrict movement of the camera when the player moves from room to room
     
    int current; //current room

    void Awake()
    {
        MoveToRoom(0, warp: true);
    }

    //Moves the player to the specified room and updates camera confinement
    public void MoveToRoom(int index, bool warp=false)
    {
        //Room index
        index = Mathf.Clamp(index, 0, rooms.Length - 1);
        current = index;

        confiner.BoundingShape2D = rooms[index].bounds; //Update condiner bounds

        if (player && rooms[index].playerSpawn) //
        {
            Vector3 newPos = rooms[index].playerSpawn.position;
            if (warp) //When "warpping", snap both player and camera to new room position
            {
                //Instantly snap vcam when teleporting to avoid slide from previous room
                var brain = Camera.main.GetComponent<CinemachineBrain>();
                if (brain) brain.DefaultBlend.Time = 0f;
                player.transform.position = newPos;
                vcam.ForceCameraPosition(new Vector3(newPos.x, newPos.y, vcam.transform.position.z), Quaternion.identity);
            }
            else
            {
                player.transform.position = newPos;
            }
        }

        if (vcam && vcam.Follow == null) vcam.Follow = player.transform; //Ensure vcam follows player
    }

    public int GetCurrentIndex() => current;
}
