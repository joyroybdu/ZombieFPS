using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackingState : StateMachineBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;
    public float stopAttackDistance = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(SoundManager.Instance.zombieChannel.isPlaying==false){
            SoundManager.Instance.zombieChannel.clip= SoundManager.Instance.zombieAttackSound;
            SoundManager.Instance.zombieChannel.PlayDelayed(1f);
        }
        LookAtPlayer();

        float distanceToPlayer = Vector3.Distance(animator.transform.position, player.position);
        if (distanceToPlayer > stopAttackDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - agent.transform.position).normalized;
        direction.y = 0; // ✅ Keep zombie upright (no tilting)
        if (direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}