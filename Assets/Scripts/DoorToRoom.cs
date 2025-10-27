using UnityEngine;

//DoorToRoom
//Trigger placed in doorways to move player to target room
[RequireComponent(typeof(Collider2D))]
public class DoorToRoom : MonoBehaviour
{
    public int targetRoomIndex = 0;
    public int keysRequired = 1; //default
    public bool consumeKeys = true; //"consume" colected keys

    [Header("Optional Override Spawn")]
    public Transform customSpawnInTarget; //leave null to use room's default spawn
    RoomManager rm;

    void Reset() { GetComponent<Collider2D>().isTrigger = true; } //setup trigger at doorway

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rm = FindAnyObjectByType<RoomManager>();
    }

    //Handles door to room transition
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<KeyInventory>();
        if (inv == null) return;

        if (keysRequired == 0 || inv.Keys >= keysRequired)
        {
            if (consumeKeys && keysRequired > 0) inv.TryUseKeys(keysRequired); //unlock door with req. keys

            // Optionally set the target room's PlayerSpawn to customSpawn
            if (customSpawnInTarget && rm.rooms != null &&
                targetRoomIndex >= 0 && targetRoomIndex < rm.rooms.Length)
            {
                rm.rooms[targetRoomIndex].playerSpawn = customSpawnInTarget;
            }

            rm.MoveToRoom(targetRoomIndex, warp: false);
        }
        else //Cannot continue without collecting keys
        {
            // check for keys needed to enter door
            Debug.Log($"Need {keysRequired} keys.");
        }
    }
}
