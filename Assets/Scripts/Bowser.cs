using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Bowser : MonoBehaviour
{
    public GameObject player;
    
    public GameObject normalProjectile;
    public GameObject homingProjectile;

    public GameObject victoryEasyModeMenu;
    public GameObject victoryHardModeMenu;

    private int health;

    private bool dead;

    void Start()
    {
        health = 3;

        dead = false;

        StartCoroutine("Attack");
    }

    void Update()
    {
        if (health == 0 && !dead)
        {
            dead = true;

            Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Easy Mode")
			{
                Mario playerComp = player.GetComponent<Mario>();

                playerComp.Pause();

                Instantiate(victoryEasyModeMenu, new Vector3(0, 0, -1), Quaternion.identity);
            }
			else
			{
                Mario playerComp = player.GetComponent<Mario>();

                playerComp.Pause();

                Instantiate(victoryHardModeMenu, new Vector3(0, 0, -1), Quaternion.identity);
            }
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
            for (int i = 0; i < 5; ++i)
            {
                FireProjectileAtPlayer();

                yield return new WaitForSeconds(1);
            }

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
			Instantiate(normalProjectile, transform.position + direction * 1.8f, Quaternion.identity);

        Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();

        cloneBody.velocity = direction * 3;
    }

    void FireHomingProjectile()
    {
        GameObject clone =
			Instantiate(homingProjectile, transform.position + Vector3.up * 1.8f, Quaternion.identity);

        HomingProjectile homingProjectileComp = clone.GetComponent<HomingProjectile>();

        homingProjectileComp.SetSpeed(1.5f);
        homingProjectileComp.SetSelfDestruct(8);
    }
}
