using UnityEngine;
using UnityEngine.SceneManagement;

//KeyPickup
//Implements a collectable "key" item
//When the player touches the key, key gets added (KeyInventory)
[RequireComponent(typeof(Collider2D))]
public class KeyPickup : MonoBehaviour
{
    public int amount = 1; //default key 

    public bool countsTowardRestart = false; //mark keys that count toward restart for last room

    private static int finalRoomCollected = 0; //static tally for the session (auto-resets on scene reload)
    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    //To trigger key collection when player gets a key
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var inv = other.GetComponent<KeyInventory>();
        if (inv == null) return;
        inv.AddKey(amount);

        if (countsTowardRestart) //check if this key is one of the final-room keys
        {
            finalRoomCollected++;
            // When all 3 are collected, restart the scene
            if (finalRoomCollected >= 3)
            {
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
                return; // avoid Destroy after reload call
            }
        }

        Destroy(gameObject);
    }
}
