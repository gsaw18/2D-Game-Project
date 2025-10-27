using UnityEngine;
using UnityEngine.SceneManagement;

//Health
//Health system for the player
//When the player takes (default 3) hits, damage is added to them
//When # hits taken, restart the game from the beginning
public class Health : MonoBehaviour
{
    [Header("Tuning")]
    public int maxHits = 3; //max number of hits before restart
    public float invulnTime = 0.75f;   //brief invulnerability after a hit
    public bool flashWhileInvuln = true; //Indicator for when a "hit" happens

    int hits = 0;
    bool invuln = false;
    SpriteRenderer sr;

    void Awake() { sr = GetComponent<SpriteRenderer>(); }

    //Apply damage and handle invulnerability
    public void TakeDamage(int amount = 1)
    {
        if (invuln) return;

        hits += amount;
        if (hits >= maxHits)
        {
            // Restart scene
            var s = SceneManager.GetActiveScene();
            SceneManager.LoadScene(s.buildIndex);
            return;
        }

        StartCoroutine(Invulnerability()); // brief invulnerability window
    }

    //Invulnerability implementation
    System.Collections.IEnumerator Invulnerability()
    {
        invuln = true;

        //Toggle an option to have the player flash so as to indicate damamge has been taken
        if (flashWhileInvuln && sr)
        {
            float t = 0f;
            while (t < invulnTime)
            {
                sr.enabled = !sr.enabled; // blink
                yield return new WaitForSeconds(0.1f);
                t += 0.1f;
            }
            sr.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invulnTime);
        }

        invuln = false;
    }
}
