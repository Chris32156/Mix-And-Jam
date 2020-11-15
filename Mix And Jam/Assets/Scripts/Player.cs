using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Params
    public float speed;
    public int startingHealth;
    public float rotationOffset = 0;

    int currentHealth;

    // Declare Vars
    Game game;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        anim = GetComponent<Animator>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        // Check if not paused and can move
        if (mousePos.x != objectPos.x && mousePos.y != objectPos.y && !game.isPaused)
        {
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        var Enemies = FindObjectsOfType<Enemy>();
        if (Enemies.Length > 0)
        {
            int index = Random.Range(0, Enemies.Length);
            Enemies[index].Destroy();
        }
    }

    public void GotHit()
    {
        currentHealth--;

        // Game Over
        if (currentHealth <= 0)
        {
            StartCoroutine(Explode());
            game.isPaused = true;
        }
    }

    IEnumerator Explode()
    {
        anim.SetTrigger("Died");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
