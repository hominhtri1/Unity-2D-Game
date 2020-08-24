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

        // InvokeRepeating("Attack", 1, 30);
        StartCoroutine("Attack");
    }

    void Update()
    {
        if (health == 0)
        {
            Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Level 1")
				SceneManager.LoadScene("Level 2");
			else
				SceneManager.LoadScene("Level 1");
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

    IEnumerator Attack()
    {
        while (true)
        {
            for (int i = 0; i < 3; ++i)
            {
                FireHomingProjectile();

                yield return new WaitForSeconds(2);
            }

            yield return new WaitForSeconds(10);
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

    void FireHomingProjectile()
    {
        GameObject clone =
			Instantiate(projectile, transform.position + Vector3.up * 1.8f, Quaternion.identity);

        HomingProjectile homingProjectile = clone.GetComponent<HomingProjectile>();

        homingProjectile.SetSpeed(1.5f);
        homingProjectile.SetSelfDestruct(8);
    }
}
