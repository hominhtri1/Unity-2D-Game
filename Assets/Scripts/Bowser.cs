using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Bowser : MonoBehaviour
{
    public GameObject player;
    
    public GameObject projectile;

    private int health;

    void Start()
    {
        health = 3;

        InvokeRepeating("FireProjectileAtPlayer", 1, 2);
    }

    void Update()
    {
        if (health == 0)
		{
			Scene scene = SceneManager.GetActiveScene();
			
            if (scene.buildIndex == 1)
				SceneManager.LoadScene(2);
			else
				SceneManager.LoadScene(1);
		}
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject collidingObject = collision.gameObject;

		if (collidingObject.tag == "Projectile")
        {
            health -= 1;

            Destroy(collidingObject);
        }
	}

    void FireProjectileAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction /= direction.magnitude;

        FireProjectile(direction);
    }

    void FireProjectile(Vector3 direction)
    {
        GameObject clone =
			Instantiate(projectile, transform.position + direction * 1.8f, Quaternion.identity);

        Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();

        cloneBody.velocity = direction * 3;
    }
}
