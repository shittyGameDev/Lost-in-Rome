using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int health = 500;

	public GameObject Manager;
	public GameObject deathEffect;
	public CompletedBoss completeBoss;
	public GameObject endGame;

	public bool isInvulnerable = false;

	// Metod f�r att bossen ska ta skada
	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		// Ber�kningen f�r att minska HP
		health -= damage;

		// N�r bossen ska g� in i sin enrage
		if (health <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		// N�r bossens hp g�r till 0
		if (health <= 0)
		{ 
			Die();
		}
	}

	// Vad som sker n�r bossen d�r
	void Die()
	{
		// Kallar p� en metod f�r att visa att mini gamet �r klart
		Manager.GetComponent<CompletedBoss>().scoreUpdate();
		// Spawnar f�rem�len runt i scenen
		completeBoss.GetComponent<CompletedBoss>().ShowObjects();
		// S�tter p� endgame sk�rmen
		endGame.GetComponent<EndGame>().showEndGame();
		// Spelar deatheffecten
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		// F�rst�r objektet
		Destroy(gameObject);
		
	}

}
