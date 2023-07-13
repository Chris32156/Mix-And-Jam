using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Params
    public float speed;
    public int startingHealth;
    public float rotationOffset = 0;
    public TextMeshProUGUI LivesText;
    public float shootCooldown;
    public GameObject DisabledShoot;
    public TextMeshProUGUI DisabledShootText;
    public TextMeshProUGUI GoldText;
    public int score;
    public int gold = 0;

    float lastShot = -20;
    int currentHealth;

    // Declare Vars
    Game game;
    Animator anim;
    SceneManagement scene;
    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        scene = FindObjectOfType<SceneManagement>();
        audio = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();

        currentHealth = startingHealth;
        LivesText.SetText("X " + currentHealth.ToString());
        GoldText.SetText(gold.ToString());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - lastShot > shootCooldown)
        {
            DisabledShoot.SetActive(false);
        }
        else
        {
            DisabledShoot.SetActive(true);
            DisabledShootText.SetText((shootCooldown - (Time.time - lastShot)).ToString("F1"));
        }
        /* Movement
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        // Check if not paused and can move
        if (mousePos.x != objectPos.x && mousePos.y != objectPos.y && !game.isPaused)
        {
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            // transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        } */
    }

    public void Attack()
    {
        if (Time.time - lastShot > shootCooldown)
        {
            lastShot = Time.time;

            var Enemies = FindObjectsOfType<Enemy>();
            float minDist = Mathf.Infinity;
            Enemy tMin = null;

            foreach (Enemy Enemy in Enemies)
            {
                Transform t = Enemy.transform;
                float dist = Vector3.Distance(t.position, transform.position);
                if (dist < minDist)
                {
                    tMin = Enemy;
                    minDist = dist;
                }
            }

            if (tMin != null)
            {
                score++;
                gold += tMin.goldDropped;
                GoldText.SetText(gold.ToString());
                tMin.Destroy();
                if (audio)
                {
                    audio.HitSound();
                }
            }
        }
    }

    public void GotHit()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, 99);
        LivesText.SetText("X " + currentHealth.ToString());

        // Game Over
        if (currentHealth <= 0)
        {
            StartCoroutine(Explode());
            game.isPaused = true;

            if (audio)
            {
                audio.HitSound();
            }
            PlayerPrefs.SetInt("Score", score);
            scene.LoadScene("Game Over");
        }
        else
        {
            if (audio)
            {
                audio.PlayerHitSound();
            }
        }
    }

    IEnumerator Explode()
    {
        anim.SetTrigger("Died");
        yield return new WaitForSeconds(1.5f);
        if (audio)
        {
            audio.GameOverSound();
        }
        Destroy(gameObject);
    }
}
