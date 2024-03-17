using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;

	public Collider2D weapon;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	public void Attack()
	{
		// Styr bossen vapen när det kommer till attack
		Vector3 pos = weapon.transform.position;
		// Sätter en offset när attacken ska ske
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		// Ifall spelaren befinner sig inom bossen range så attackerar den
		weapon = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (weapon != null)
		{
			weapon.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
	}

	public void EnragedAttack()
	{
		// Samma som ovanstående
		Vector3 pos = weapon.transform.position;
		// Samma som ovanstående
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;
		// Samma som ovanstående
		weapon = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (weapon != null)
		{
			weapon.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}

	// Metod för att rita ut i scenen hur attacken ser ut
	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
