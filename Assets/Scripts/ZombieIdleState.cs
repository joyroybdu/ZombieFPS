using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;
    Transform player;
    public float detectionRange = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            animator.SetBool("isPatroling", true);
        }

        float distanceToPlayer = Vector3.Distance(animator.transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("isChasing", true);
        }
    }
}
