using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    private int life= 100;
    
    private bool attacked = false;
    private float attackDistance = 3f;

    public Animator anim;
    public GameObject mesh;
    void Start()
    {
        
    }

    public void startChasing()
    {
        agent.speed++;
        StartCoroutine(followTarget());
    }
    private IEnumerator followTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(.1f);
        while (agent.enabled)
        {
            agent.SetDestination(player.position);
            yield return wait;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, player.position);

        if (life < 0 || distanceToPlayer > 200)
        {
            if (this.gameObject.activeSelf && life < 0)
            {
                PlayerInfo.addscore();
            }
            this.gameObject.SetActive(false);
            agent.enabled = false;
            EnemyManager.enemyDied();
            life = 100;
        }

        if (distanceToPlayer < attackDistance && !attacked)
        {
            attacked = true;
            StartCoroutine(Attack());
        }

        float velocity = agent.velocity.magnitude;
        anim.SetBool("run", velocity > 0.4 && distanceToPlayer > attackDistance);
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        agent.enabled = false;
        player.GetComponent<PlayerInfo>().takeDamage(25);
        yield return new WaitForSeconds(3f);
        attacked = false;
        agent.enabled = true;
        anim.ResetTrigger("attack");
    }

    public void removeHealth()
    {
        this.life -= 25;
    }
}