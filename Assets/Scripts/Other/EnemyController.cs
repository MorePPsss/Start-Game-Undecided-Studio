using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private EnemyStates enemyStates;
    private NavMeshAgent enemyAgent;
    private GameObject attackTarget;
    private GameObject baitTarget;
    private GameObject playerTarget;

    [Header("Material Settings")]
    public Material normalMaterial;
    public Material alertMaterial;
    private Renderer enemyRenderer;

    [Header("Basic Settings")]
    public bool isGuard;
    public float findRadius;
    private float moveSpeed;//Record the Enemy's original speed
    public float lookAtTime;
    private float remainLookAtTime;

    [Header("Patrol Range")]
    public float patrolRange;
    private Vector3 patrolPoint;
    private Vector3 guardPos;

    [Header("Action Flag")]
    private bool isChase;
    private bool isWalk;
    private bool isFollow;

    void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyAgent = GetComponent<NavMeshAgent>();
        moveSpeed = enemyAgent.speed;
        guardPos = transform.position;
        remainLookAtTime = lookAtTime;
    }
    void Start()
    {
        if (isGuard) 
        { 
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PATROL;
            GetNewPatrolPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        if (FindPlayer())
        {
            enemyStates = EnemyStates.CHASE;
        }
            switch (enemyStates)
        {
            case EnemyStates.GUARD:

                break;
            case EnemyStates.PATROL:
                Partrol();
                break;
            case EnemyStates.CHASE:
                //TODO: Chasing Player in the sightRadius
                Chasing();
                //TODO: Chasing Animation
                //TODO: from Chasing State back to the former State. 
                //TODO: Attack and Animation
                break;
            case EnemyStates.DEAD:

                break;
        }
    }

    bool FindPlayer()
    {
        /*Key:Physics.OverlapSphere()
         returns an array containing all colliders that are in contact with or located inside a sphere
         */
        var colliders = Physics.OverlapSphere(transform.position, findRadius);

        // 遍历检测到的对象
        foreach (var target in colliders)
        {
            // 优先检测诱饵
            if (target.CompareTag(Tag.BAITITEM))
            {
                baitTarget = target.gameObject;
            }
            // 检测玩家
            else if (target.CompareTag(Tag.PLAYER))
            {
                playerTarget = target.gameObject;
            }
        }

        // 优先选择诱饵作为目标
        if (baitTarget != null)
        {
            attackTarget = baitTarget;
            return true;
        }
        // 如果没有诱饵，选择玩家
        else if (playerTarget != null)
        {
            attackTarget = playerTarget;
            return true;
        }

        return false;
    }

    void Chasing()
    {
        isChase = true;
        enemyAgent.speed = moveSpeed;
        if (!FindPlayer())
        {
            isFollow = false;
            if (remainLookAtTime > 0)
            {
                enemyAgent.destination = transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if(isGuard)
            {
                enemyStates = EnemyStates.GUARD;
                enemyRenderer.material = normalMaterial;
                enemyAgent.destination = guardPos; // Back to the guard position
            }
            else
            {
                enemyStates = EnemyStates.PATROL; // Switch to the patrol state
            }
        }
        else
        {
            isFollow = true;
            if (enemyRenderer != null && normalMaterial != null)
            {
                enemyRenderer.material = alertMaterial;
            }
            // 设置目标为当前检测到的目标（诱饵或玩家）
            if (attackTarget != null)
            {
                enemyAgent.destination = attackTarget.transform.position;
            }
        }
        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.transform.position) <= 1.5f)
        {
            if (attackTarget.CompareTag(Tag.PLAYER))
            {
                PlayerCaught(); // 捕获玩家
            }
            else if (attackTarget.CompareTag(Tag.BAITITEM))
            {
                Destroy(attackTarget); // 销毁诱饵
                attackTarget = null; // 清除当前目标
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRadius);
    }

    void GetNewPatrolPoint()
    {
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPatrolPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
        NavMeshHit hit;
        patrolPoint = NavMesh.SamplePosition(randomPatrolPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    void Partrol()
    {
        isChase = false;
        enemyAgent.speed = moveSpeed * 0.5f;
        enemyRenderer.material = normalMaterial;

        if (Vector3.Distance(patrolPoint, transform.position) <= 1)
        {
            isWalk = false;//Stop to find another new patrolPoint
            if( remainLookAtTime > 0 )
            {
                remainLookAtTime -= Time.deltaTime;
            }else
            {
                GetNewPatrolPoint();
            }
        }
        else
        {
            isWalk = true;
            enemyAgent.destination = patrolPoint;
        }
    }

    void PlayerCaught()
    {
        Debug.Log("玩家被敌人抓住了！游戏失败！");

        // 停止敌人移动
        enemyAgent.isStopped = true;


        // 触发玩家失败的逻辑
        //GameManager.Instance.GameOver(); // 假设有一个 GameManager 来处理游戏结束
    }
}
