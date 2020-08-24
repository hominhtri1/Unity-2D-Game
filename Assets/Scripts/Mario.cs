using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Mario : MonoBehaviour
{
	public GameObject pauseMenu;
	
	public TextMeshProUGUI healthText = null;

	public GameObject projectile;

	public AudioClip jumpAudio;
	
	private Rigidbody2D body;

	private Animator animator;

	private AudioSource audioSource;

	private GameObject pauseMenuInstance;

	private bool paused;

	private int health;

	private bool facingRight;
	
    void Start()
    {
		body = GetComponent<Rigidbody2D>();

		animator = GetComponent<Animator>();

		audioSource = GetComponent<AudioSource>();

		paused = false;

		health = 3;

		facingRight = true;
    }

	void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			paused = true;

			Time.timeScale = 0;

			pauseMenuInstance = Instantiate(pauseMenu, new Vector3(0, 0, -1), Quaternion.identity);
		}

		if (paused)
			return;

		if (healthText != null)
			healthText.text = health.ToString();
		
		if (health <= 0)
			RestartLevel();
		
		if (transform.position.y < -10)
			RestartLevel();

		if (Input.GetKeyDown("s"))
		{
			Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Level 1")
				SceneManager.LoadScene("Level 2");
			else
				SceneManager.LoadScene("Level 1");
		}
		
		if ((body.velocity.x > 0 && !facingRight) || (body.velocity.x < 0 && facingRight))
			Flip();

		if (body.velocity.x == 0)
			animator.SetBool("Walking", false);
		else
			animator.SetBool("Walking", true);

		if (Input.GetKeyDown("z"))
		{
			if (facingRight)
				FireProjectile(0);
			else
				FireProjectile(180);
		}
	}

    void FixedUpdate()
    {
		if (paused)
			return;

		bool isGrounded = IsGrounded();

        float forceX = 0;

		if (Input.GetKey("left"))
			forceX = -18;
		
		if (Input.GetKey("right"))
			forceX = 18;

		if (Math.Abs(body.velocity.x) > 5)
			forceX = 0;
		
		float forceY = 0;

		if (Input.GetKey("up") && isGrounded)
		{
			forceY = 500;

			audioSource.PlayOneShot(jumpAudio);
		}
		
		body.AddForce(new Vector2(forceX, forceY));
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

	void RestartLevel()
	{
		Scene scene = SceneManager.GetActiveScene();

		SceneManager.LoadScene(scene.name);
	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void FireProjectile(double angleDegree)
	{
		double angleRadian = angleDegree / 180 * Math.PI;

		Vector3 direction =
			new Vector3((float) Math.Cos(angleRadian), (float) Math.Sin(angleRadian), 0);
		
		FireProjectile(direction);
	}

	void FireProjectile(Vector3 direction)
    {
        GameObject clone =
			Instantiate(projectile, transform.position + direction, Quaternion.identity);

        Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();

        cloneBody.velocity = direction * 5;
    }

	bool IsGrounded()
	{
		LayerMask mask = ~(1 << 8);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, mask);

		if (hit.collider == null)
			return false;
		else
			return true;
	}

	public void Unpause()
	{
		paused = false;
			
		Time.timeScale = 1;

		Destroy(pauseMenuInstance);
	}
}
