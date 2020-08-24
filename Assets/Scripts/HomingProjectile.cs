using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Rigidbody2D body;

    private GameObject player;

    private float speed = 1;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Mario");
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction /= direction.magnitude;
        
        body.velocity = direction * speed;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetSelfDestruct(int seconds)
    {
        Invoke("SelfDestruct", seconds);
    }
}
