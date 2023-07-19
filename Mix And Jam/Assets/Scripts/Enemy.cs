using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public int GoldDropped = 5;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject cannonBall;
    public Transform player;
    public Transform shootFrom;
    public Color damaged;
    public Color basic;
    public SpriteRenderer shipSprite;
    [SerializeField] Sprite[] Sprites;
    public float YSpawnPos;
    public float XSpawnPos;

    public bool isDead = false;
    public int health = 1;

    Game game;
    Spawner spawner;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer ship;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        game = FindObjectOfType<Game>();
        spawner = FindObjectOfType<Spawner>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ship = GetComponent<SpriteRenderer>();

        timeBetweenShots = startTimeBetweenShots;

        RandomizePosition();

        int x = Random.Range(0, Sprites.Length);
        ship.sprite = Sprites[x];
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.isPaused && !isDead)
        {
            // Go towards
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            // Stay
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            // Run
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            // Shoot
            if (timeBetweenShots <= 0)
            {
                Instantiate(cannonBall, shootFrom.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }

            //Rotate
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    public void Destroy()
    {
        health--;
        if (health <= 0)
        {
            isDead = true;
            StartCoroutine(Explode());
        }
        else
        {
            shipSprite.color = damaged;
            Invoke("revertColor", 0.3f);
        }
    }

    void revertColor()
    {
        shipSprite.color = basic;
    }

    IEnumerator Explode()
    {
        anim.SetTrigger("Died");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        spawner.CheckIfAllDied();
    }

    private void RandomizePosition()
    {
        //Declare Vars
        int xModifier;
        int yModifier;
        float x = 43;
        float y = 43;

        while (x >= 43 || x <= -43 || y >= 43 || y <= -43)
        {
            x = Random.Range(5f, 10f);
            y = Random.Range(5f, 10f);
            xModifier = Random.Range(0, 2);
            yModifier = Random.Range(0, 2);

            if (xModifier == 1)
            {
                x = x * -1;
            }
            if (yModifier == 1)
            {
                y = y * -1;
            }

            x += player.transform.localPosition.x;
            y += player.transform.localPosition.y;
        }
        transform.position = new Vector2(x, y);
    }
}
