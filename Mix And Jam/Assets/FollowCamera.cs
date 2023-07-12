using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float CameraShake;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    bool isDead = false;
    
    void main()
    {

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.z = -10;
            transform.position = smoothedPosition;
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(Shake(0.25f, CameraShake));
        isDead = true;
        Invoke("PlayerRespawned", 0.25f);
    }

    public void PlayerRespawned()
    {
        isDead = false;
        target = GameObject.Find("Player(Clone)").gameObject.transform;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = orignalPosition.x - Random.Range(-1f, 1f) * magnitude;
            float y = orignalPosition.y - Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }
}
