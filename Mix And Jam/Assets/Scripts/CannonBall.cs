using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;

    private Vector2 target;
    bool a = false;
    float slopeX;
    float slopeY;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        target = new Vector2(player.transform.position.x, player.transform.position.y);

        slopeY = player.transform.position.y - transform.position.y;
        slopeX = player.transform.position.x - transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            transform.position += new Vector3(slopeX * 0.009f, slopeY * 0.009f, 0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                a = true;
            }
        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyProjectile();
            player.GotHit();
        }
    }
}
