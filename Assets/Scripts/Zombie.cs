using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public int HP = 100;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navAgent;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
                SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieRoarSound, 1f);
            }
            else
            {
                animator.SetTrigger("DIE2");
                SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieDeathSound, 1f);
            }

            navAgent.enabled = false; // ✅ Stops movement after death
           StartCoroutine(DestroyAfterDelay(3f));
        }
        else
        {
            animator.SetTrigger("DAMAGE");
            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieHitSound, 1f);
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // Detection range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10f); // Chase range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 15f); // stop chase range
    }
}