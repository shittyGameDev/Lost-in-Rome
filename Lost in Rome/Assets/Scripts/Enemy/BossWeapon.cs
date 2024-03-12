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
		Vector3 pos = weapon.transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		weapon = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (weapon != null)
		{
			weapon.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
	}

	public void EnragedAttack()
	{
		Vector3 pos = weapon.transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		weapon = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (weapon != null)
		{
			weapon.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
