using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemyBehavior : StateMachineBehaviour
{
    public Transform player;
    public Rigidbody rb;
    
    public float speed = 3f;
    public float attackRange = 2f;
    public float patrolRange = 13f;

    public EnemyBase enemy;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        enemy = animator.GetComponent<EnemyBase>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        
        Vector3 target = new Vector3(player.position.x, rb.position.y, player.position.z);
        Vector3 newPosition = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            //Change to attack state
            animator.SetTrigger("Attack");
        }
        
        if (Vector2.Distance(player.position, rb.position) >= patrolRange)
        {
            //Change to patrol state
            animator.SetTrigger("Patrol");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Patrol");
    }
}
