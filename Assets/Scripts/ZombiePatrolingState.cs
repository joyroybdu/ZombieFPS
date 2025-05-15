using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolTime = 10f;
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;
    public float detectionRange = 5f;
    public float speed = 2f;
    List<Transform> waypointsList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
        timer = 0;

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(SoundManager.Instance.zombieChannel.isPlaying==false){
            SoundManager.Instance.zombieChannel.clip= SoundManager.Instance.zombieWalkingSound;
            SoundManager.Instance.zombieChannel.PlayDelayed(1f);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        timer += Time.deltaTime;
        if (timer >= patrolTime)
        {
            animator.SetBool("isPatroling", false);
        }

        float distanceToPlayer = Vector3.Distance(animator.transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
