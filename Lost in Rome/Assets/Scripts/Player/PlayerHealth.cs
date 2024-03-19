using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public int health = 100;

	public GameObject deathEffect;

	// Metod för att spelaren ska ta skada
	public void TakeDamage(int damage)
	{
		// Reducerar hp med mängden skada
		health -= damage;

		// Startar animationen för att ta skada
		StartCoroutine(DamageAnimation());

		// Vad som händer när spelaren får slut på hp
		if (health <= 0)
		{
			Die();
		}
	}

	// Laddar om scenen när spelaren dör
	void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		// Animation som gör att spelaren blinkar när den tar skada genom att göra färgen genomskinlig
		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

}
