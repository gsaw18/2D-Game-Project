using UnityEngine;

//KeyInventory
//Tracks the player's key count
public class KeyInventory : MonoBehaviour
{
    public int Keys { get; private set; } //Current numbe of keys carried
    public void AddKey(int amount = 1)
    {
        Keys += amount; //Add keys when key is collected
    }

    //Checker to check if required number of keys is met in order to move on to the next door
    public bool TryUseKeys(int amount)
    {
        
        if (Keys >= amount)
        {
            Keys -= amount; //Spend keys upon opening door
            return true;
        }
        return false; //If not enoguh keys have been collected
    }
}
