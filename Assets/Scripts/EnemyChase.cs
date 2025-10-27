using UnityEngine;

//EnemyChase
//Enemy movement: chase the player upon entering enemy radius
//Goal: Have enemies chase when entering rooms
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    //Enemy specs
    public float moveSpeed = 2f;
    public float chaseRadius = 5f;

    Transform player;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var p = FindAnyObjectByType<KeyInventory>(); //use KeyInventory to find player root
        if (p) player = p.transform;
    }
    
    // Update is called once per frame
    //Called every physics step
    //Handles enemy movement toward the player
    void FixedUpdate()
    {
        if (!player) { rb.linearVelocity = Vector2.zero; return; } //stop motion if no player is within radius

        Vector2 dir = player.position - transform.position; //positoin from enemy to player
        if (dir.sqrMagnitude <= chaseRadius * chaseRadius) //chase player if within radius
            rb.linearVelocity = dir.normalized * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero; //idle when far
    }
}
