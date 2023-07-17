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
    public float speedUpCooldown;
    public float healCooldown;
    public float KillEverythingCooldown;
    public bool speedUpUnlocked;
    public bool healUnlocked;
    public bool killEverythingUnlocked;
    public GameObject DisabledShoot;
    public TextMeshProUGUI DisabledShootText;
    public GameObject DisabledSpeedUp;
    public TextMeshProUGUI DisabledSpeedUpText;
    public GameObject DisabledHeal;
    public GameObject KillLock;
    public GameObject HealLock;
    public GameObject SpeedupLock;
    public TextMeshProUGUI DisabledHealText;
    public GameObject DisabledKill;
    public TextMeshProUGUI DisabledKillText;
    public TextMeshProUGUI GoldText;
    public JoystickMove joystick;
    public int score;
    public int Gold = 0;
    public bool isDead = false;

    float lastShot = -200;
    float lastSpeedUp = -200;
    float lastHeal = 0;
    float lastKillEverything = -200;
    float baseSpeed;
    int currentHealth;

    // Declare Vars
    Game game;
    Animator anim;
    SceneManagement scene;
    AudioManager audio;
    Rigidbody2D rb;
    Spawner spawn;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        scene = FindObjectOfType<SceneManagement>();
        audio = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        spawn = FindObjectOfType<Spawner>();
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = joystick.playerSpeed;
        currentHealth = startingHealth;
        LivesText.SetText("X " + currentHealth.ToString());
        GoldText.SetText(Gold.ToString());

        if(!speedUpUnlocked)
        {
            SpeedupLock.SetActive(true);
            DisabledSpeedUp.SetActive(false);
        }
        if(!healUnlocked)
        {
            HealLock.SetActive(true);
            DisabledHeal.SetActive(false);
        }
        if(!killEverythingUnlocked)
        {
            KillLock.SetActive(true);
            DisabledKill.SetActive(false);
        }
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

        if (Time.time - lastSpeedUp > speedUpCooldown)
        {
            DisabledSpeedUp.SetActive(false);
        }
        else
        {
            DisabledSpeedUp.SetActive(true);
            DisabledSpeedUpText.SetText((speedUpCooldown - (Time.time - lastSpeedUp)).ToString("F1"));
        }

        if (lastHeal + healCooldown <= spawn.CurrentWave - 1)
        {
            DisabledHeal.SetActive(false);
        }
        else
        {
            DisabledHeal.SetActive(true);
            DisabledHealText.SetText((lastHeal + healCooldown - spawn.CurrentWave + 1).ToString() + " Waves");
        }

        if (Time.time - lastKillEverything > KillEverythingCooldown)
        {
            DisabledKill.SetActive(false);
        }
        else
        {
            DisabledKill.SetActive(true);
            DisabledKillText.SetText((KillEverythingCooldown - (Time.time - lastKillEverything)).ToString("F1"));
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
        if (Time.time - lastShot > shootCooldown && !isDead)
        {

            var Enemies = FindObjectsOfType<Enemy>();
            float minDist = Mathf.Infinity;
            Enemy tMin = null;

            foreach (Enemy Enemy in Enemies)
            {
                if (!Enemy.isDead)
                {
                    Transform t = Enemy.transform;
                    float dist = Vector3.Distance(t.position, transform.position);
                    if (dist < minDist)
                    {
                        tMin = Enemy;
                        minDist = dist;
                    }
                }
            }

            if (tMin != null && minDist < 10)
            {
                lastShot = Time.time;
                score++;
                tMin.Destroy();
                if (audio)
                {
                    audio.HitSound();
                }
                AddGold(tMin.GoldDropped);
            }
        }
    }

    public void SpeedUp()
    {
        if (Time.time - lastSpeedUp > speedUpCooldown && speedUpUnlocked && !isDead)
        {
            lastSpeedUp = Time.time;
            joystick.playerSpeed = joystick.playerSpeed * 2;
            Invoke("SpeedupEnd", 10);
        }
    }

    public void Heal()
    {
        if(lastHeal + healCooldown <= spawn.CurrentWave - 1 && healUnlocked && currentHealth < startingHealth && !isDead)
        {
            lastHeal = spawn.CurrentWave - 1;
            currentHealth++;
            LivesText.SetText("X " + currentHealth.ToString());
        }
    }

    public void KillEverything()
    {
        if (Time.time - lastKillEverything > KillEverythingCooldown && killEverythingUnlocked && !isDead)
        {
            var Enemies = FindObjectsOfType<Enemy>();
            Enemy tMin = null;

            foreach (Enemy Enemy in Enemies)
            {
                if (!Enemy.isDead)
                {
                    Transform t = Enemy.transform;
                    float dist = Vector3.Distance(t.position, transform.position);
                    if (dist < 10)
                    {
                        lastKillEverything = Time.time;
                        score++;
                        Enemy.Destroy();
                        if (audio)
                        {
                            audio.HitSound();
                        }
                        AddGold(Enemy.GoldDropped);
                        Invoke("checkIfNextRound", 2);
                    }
                }
            }
        }
    }

    void checkIfNextRound()
    {
        spawn.CheckIfAllDied();
    }

    void SpeedupEnd()
    {
        joystick.playerSpeed = baseSpeed;
    }

    public void GotHit(int damage)
    {
        currentHealth-= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 99);
        LivesText.SetText("X " + currentHealth.ToString());

        // Game Over
        if (currentHealth <= 0)
        {
            rb.velocity = Vector3.zero;
            StartCoroutine(Explode());
            game.isPaused = true;
            isDead = true;
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

    public void AddGold(int gold)
    {
        Gold += gold;
        GoldText.SetText(Gold.ToString());
    }
}
