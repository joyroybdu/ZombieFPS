using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Make sure to include this!

public class ZombieChasingState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public float chaseSpeed = 3f;
    public float stopChasingDistance = 10f;
    public float attackDistance = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         if(SoundManager.Instance.zombieChannel.isPlaying==false){
            SoundManager.Instance.zombieChannel.clip= SoundManager.Instance.zombieChaseSound;
          
        }
        agent.SetDestination(player.position);
        float distanceToPlayer = Vector3.Distance(animator.transform.position, player.position);

        if (distanceToPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }
        else if (distanceToPlayer <= attackDistance)
        {
            animator.SetBool("isAttacking", true); // ✅ FIXED: Set to TRUE when close to attack
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}