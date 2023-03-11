using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;

    Animator animator;
    AudioSource audioSource;

    private Player player;
    private float vertLimit = -5.8f;
    private float horLimit = -8.5f;

    Collider2D enemyCollider;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) { Debug.LogError("No Player Found = NULL"); }

        animator = GetComponent<Animator>();
        if (animator == null) { Debug.LogError("Animator is NULL"); }


        enemyCollider = GetComponent<Collider2D>();
        if (enemyCollider == null) { Debug.LogError("No Collider Set = Null"); }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { Debug.LogError("No Audio Source Component"); }
    }

    private void Update()
    {
        movement();
    }

    float RandomNum()
    {
        return Random.Range(horLimit, -horLimit);
    }

    void movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y <= vertLimit)
        {
            transform.position = new Vector3(RandomNum(), -vertLimit, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (player != null)
            {
                player.addScore(10);
            }
            enemyCollider.enabled = false;
            triggerDestroyAnim();
            Destroy(other.gameObject); // Destruye laser cuando colisiona
            Destroy(this.gameObject, 1.0f); // Destruye Enemy cuando colisiona con laser
        }
        if (other.tag == "Player")
        {
            // Damage the player
            if (player != null)
            {
                player.Damage();
            }
            enemyCollider.enabled = false;
            triggerDestroyAnim();
            Destroy(this.gameObject, 1.0f); // Destruye Enemy cuando colisiona
        }
    }

    void triggerDestroyAnim()
    {
        animator.SetTrigger("OnEnemyDeath");
        audioSource.Play();
    }
}
