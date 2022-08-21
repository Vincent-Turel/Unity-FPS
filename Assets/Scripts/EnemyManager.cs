using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    
    private static int enemyCount;

    private EnemyScript enemyScript;
    private NavMeshAgent agent;
    
    void Start()
    {
        enemyCount = 0;
        StartCoroutine(spawnEnemies());
    }

    private void Update()
    {
        if (enemyCount < 50)
        {
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies()
    {
        LayerMask layerMask = LayerMask.GetMask("Ground");
        while (enemyCount < 50)
        {
            Vector3 position = target.position;
            float Xpos = Random.Range(position.x - 100, position.x + 100);
            float Zpos = Random.Range(position.z - 100, position.z + 100);
            const float Ypos = 500f;
            RaycastHit hit;
            
            if (Physics.Raycast(new Vector3(Xpos, Ypos, Zpos), Vector3.down, out hit,
                500, layerMask))
            {
                if (NavMesh.SamplePosition(hit.point, out var hitOnNavMesh, 2f, NavMesh.AllAreas))
                {
                    GameObject enemy = EnemyPool.sharedInstance.getPooledObject();
                    enemy.transform.position = hitOnNavMesh.position;
                    enemyScript = enemy.GetComponent<EnemyScript>();
                    agent = enemy.GetComponent<NavMeshAgent>();
                    enemyScript.player = target;
                    enemyCount++;
                    enemy.SetActive(true);
                    agent.enabled = true;
                    enemyScript.startChasing();
                }
            }
        } 
        yield return new WaitForSeconds(0.01f);
    }

    public static void enemyDied()
    {
        enemyCount--;
    }
}
