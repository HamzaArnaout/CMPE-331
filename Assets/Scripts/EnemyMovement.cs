using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float sightRange;
    public bool playerInSightRange;

    //Attacking
    public NavMeshAgent agent;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackRange;
    public bool playerInAttackRange;
    public float damage = 10f;

    public Animator animator;

    public LayerMask whatIsPlayer;

    private bool attacking;

    public Animation animation;

    public Health zombieHealth;

    public GameObject colliders;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (zombieHealth.health > 0) // Make their speed 0 if theyre dead
            agent.speed = speed;
        else
            agent.speed = 0;

        if ((playerInSightRange || zombieHealth.health < 50) && !playerInAttackRange && !attacking) // If you shoot them or ur in their range theyll start following you
        {
            animator.SetTrigger("Follow"); // Plays the walking animation
            agent.SetDestination(player.transform.position); // Follow Player
        } else if (playerInAttackRange)
        {
            agent.SetDestination(transform.position); //Make sure enemy doesn't move
            animator.SetBool("Attacking", true);
            attacking = true;
        }
    }

    public void AttackPlayer()
    {
        if (!alreadyAttacked && playerInAttackRange)
        {
            Health playerhealth = player.GetComponent<Health>();

            
            playerhealth.TakeDamage(damage);
            Debug.Log(playerhealth);

            animation.Play();

            alreadyAttacked = true;
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;

        animator.SetBool("Attacking", false);
        attacking = false;
        agent.SetDestination(player.transform.position);
    }
    public void SetLowSpeed()
    {
        speed = 0.2f;
    }

    public void SetHighSpeed()
    {
        speed = 1.5f;
    }

    public void DisableColliders()
    {
        colliders.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
