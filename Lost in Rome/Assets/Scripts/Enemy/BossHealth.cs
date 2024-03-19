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

	// Metod för att bossen ska ta skada
	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		// Beräkningen för att minska HP
		health -= damage;

		// När bossen ska gå in i sin enrage
		if (health <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		// När bossens hp går till 0
		if (health <= 0)
		{ 
			Die();
		}
	}

	// Vad som sker när bossen dör
	void Die()
	{
		// Kallar på en metod för att visa att mini gamet är klart
		Manager.GetComponent<CompletedBoss>().scoreUpdate();
		// Spawnar föremålen runt i scenen
		completeBoss.GetComponent<CompletedBoss>().ShowObjects();
		// Sätter på endgame skärmen
		endGame.GetComponent<EndGame>().showEndGame();
		// Spelar deatheffecten
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		// Förstör objektet
		Destroy(gameObject);
		
	}

}
