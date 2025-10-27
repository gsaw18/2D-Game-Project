using UnityEngine;

//DamageOnTouch
//Handles damage to player
public class DamageOnTouch : MonoBehaviour
{
    public int damage = 1;
    public bool useTriggers = true; // set true if enemy collider is a Trigger

    //I had a problem with triggers getting to react, so I implemented two functions for two trigger conditions
    //1. Trigger-based contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTriggers) return; 
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Health>()?.TakeDamage(damage);
    }

    //2. Collision-based contact
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTriggers) return;
        if (!collision.collider.CompareTag("Player")) return;
        collision.collider.GetComponent<Health>()?.TakeDamage(damage);
    }
}
