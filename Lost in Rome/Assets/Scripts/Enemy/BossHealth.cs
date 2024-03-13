using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int health = 500;

	public GameObject Manager;
	public GameObject deathEffect;
	public CompletedBoss completeBoss;

	public bool isInvulnerable = false;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{ 
			Die();
		}
	}

	void Die()
	{
		Manager.GetComponent<CompletedBoss>().scoreUpdate();
		completeBoss.GetComponent<CompletedBoss>().ShowObjects();
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		
	}

}
