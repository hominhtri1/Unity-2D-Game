using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Bowser : MonoBehaviour
{
    public GameObject player;
    
    public GameObject normalProjectile;
    public GameObject homingProjectile;

    public AudioClip shootAudio;

    public TextMeshProUGUI healthText = null;

    public GameObject victoryEasyModeMenu;
    public GameObject victoryHardModeMenu;

    private Animator animator;

    private AudioSource audioSource;

    private int health;

    private bool dead;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        health = 3;

        dead = false;

        StartCoroutine("Attack");
    }

    void Update()
    {
        if (healthText != null)
			healthText.text = health.ToString();

        if (health <= 0 && !dead)
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

		if (collidingObject.tag == "Player Projectile")
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
        animator.SetTrigger("Shoot");

        audioSource.PlayOneShot(shootAudio);

        GameObject clone =
			Instantiate(normalProjectile, transform.position + direction * 1.8f, Quaternion.identity);

        Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();

        cloneBody.velocity = direction * 3;
    }

    void FireHomingProjectile()
    {
        animator.SetTrigger("Shoot");

        audioSource.PlayOneShot(shootAudio);

        GameObject clone =
			Instantiate(homingProjectile, transform.position + Vector3.up * 1.8f, Quaternion.identity);

        HomingProjectile homingProjectileComp = clone.GetComponent<HomingProjectile>();

        homingProjectileComp.SetSpeed(1.5f);
        homingProjectileComp.SetSelfDestruct(8);
    }
}
