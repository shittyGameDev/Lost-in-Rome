using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 50;
    public float attackRange = 1f;
    public LayerMask bossLayer;
    public Animator playerAnimator; 

    private bool isAttacking = false;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {   
        isAttacking = true;
        playerAnimator.SetTrigger("Attack"); 
        
        // Väntar innan attacken görs
        yield return new WaitForSeconds(0.5f); 

        // Attacken. Tittar ifall det är bossen som träffas av collidern
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, bossLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            BossHealth bossHealth = enemy.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
            }
        }

        // Väntar innan man kan attackera igen
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
