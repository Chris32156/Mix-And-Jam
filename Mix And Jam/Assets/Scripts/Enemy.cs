using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject cannonBall;
    public Transform player;
    public Transform shootFrom;
    [SerializeField] Sprite[] Sprites;
    public float YSpawnPos;
    public float XSpawnPos;

    bool isDead = false;

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
        isDead = true;
        StartCoroutine(Explode());
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
        bool isLeftOrRight;
        int xModifier = 1;
        int yModifier = 1;

        //Choose Which Border It Spawns On
        int Border = Random.Range(1, 5); // 1 is Left 2 is Right 3 Is Top 4 is Bottom

        if (Border < 3)
        {
            isLeftOrRight = true;

            //Sets It To -
            if (Border == 1)
            {
                xModifier = -1;
            }
        }
        //If It Spawns On Top Or Bottom
        else
        {
            isLeftOrRight = false;

            //Sets It To Negative if Bottom
            if (Border == 4)
            {
                yModifier = -1;
            }
        }

        //Declare local Vars
        float x;
        float y;

        //If Left Or Right
        if (isLeftOrRight)
        {
            x = XSpawnPos * xModifier;
            y = Random.Range(YSpawnPos * -1, YSpawnPos);
        }
        //If Is Top Or Bottom
        else
        {
            y = YSpawnPos * yModifier;
            x = Random.Range(XSpawnPos * -1, XSpawnPos);
        }

        //Fix Bug With Spawning Too High If Postive
        if (y == YSpawnPos)
        {
            y -= 0.5f;
        }

        transform.position = new Vector2(x, y);
    }
}
