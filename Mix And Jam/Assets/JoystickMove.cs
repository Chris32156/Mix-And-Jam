using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{

    public Joystick movementJoystick;
    public Transform player;
    public float rotationOffset = 0;
    public float playerSpeed;
    private Rigidbody2D rb;
    Player Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Player>();
        playerSpeed = playerSpeed * (1 + (PlayerPrefs.GetInt("Speed Level", 1) - 1) * 0.05f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Player.isDead)
        {
            if (movementJoystick.joystickVec.y != 0)
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
                float angle = Mathf.Atan2(movementJoystick.joystickVec.x * -1, movementJoystick.joystickVec.y) * Mathf.Rad2Deg;
                player.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}