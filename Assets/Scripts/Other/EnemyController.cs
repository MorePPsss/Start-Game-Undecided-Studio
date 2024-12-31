using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD, DIGESTION }
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IEndGameObserver
{
    private EnemyStates enemyStates;
    private NavMeshAgent enemyAgent;
    private GameObject attackTarget;
    private GameObject baitTarget;
    private GameObject playerTarget;

    [Header("Material Settings")]
    public Material normalMaterial;
    public Material alertMaterial;
    public Material digestionMaterial;
    private Renderer enemyRenderer;

    [Header("Basic Settings")]
    public bool isGuard;
    public float findRadius;
    private float moveSpeed;//Record the Enemy's original speed
    public float lookAtTime;
    private float remainLookAtTime;
    public float digestionTime;

    [Header("Patrol Range")]
    public float patrolRange;
    private Vector3 patrolPoint;
    private Vector3 guardPos;

    [Header("Action Flag")]
    private bool isChase;
    private bool isWalk;
    private bool isFollow;
    private bool isDigestion;
    private bool isDead;

    private bool playerDead;

    private Quaternion guardRotation;

    void Awake()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyAgent = GetComponent<NavMeshAgent>();
        moveSpeed = enemyAgent.speed;
        guardPos = transform.position;
        remainLookAtTime = lookAtTime;
        guardRotation = transform.rotation;
        isDead = false;
        playerDead = false;
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
        //FIXME��Scene changing need to be modified!
        GameManager.Instance.AddObserver(this);
    }

    //���п�ָ��ı���ԭ��û���ҵ�GameManager�����������س���������Ҳ���ص�ʱ��Ϳ������ˡ�
    //�����Ƚ����˽��ã����play�������õ��ˣ��Ͳ��ᱨ���ˡ�
    //void OnEnable()
    //{
    //    GameManager.Instance.AddObserver(this);
    //}

    void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDead)
        {
            SwitchStates();
        }
    }

    void SwitchStates()
    {
        if (isDead)
        {
            enemyStates = EnemyStates.DEAD;
        }
        if (enemyStates != EnemyStates.DIGESTION)
        {
            if (FindPlayer())
            {
                enemyStates = EnemyStates.CHASE;
            }
        }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                BackToGuard();
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
                Dead();
                break;

            case EnemyStates.DIGESTION:
                DigestionBait();
                break;
        }
    }

    bool FindPlayer()
    {
        /*Key:Physics.OverlapSphere()
         returns an array containing all colliders that are in contact with or located inside a sphere
         */
        var colliders = Physics.OverlapSphere(transform.position, findRadius);
        baitTarget = null;
        playerTarget = null;
        // ������⵽�Ķ���
        foreach (var target in colliders)
        {
            // ���ȼ���ն�
            if (target.CompareTag(Tag.BAITITEM))
            {
                baitTarget = target.gameObject;
            }
            // ������
            else if (target.CompareTag(Tag.PLAYER))
            {
                playerTarget = target.gameObject;
            }
        }

        // ����ѡ���ն���ΪĿ��
        if (baitTarget != null)
        {
            attackTarget = baitTarget;
            return true;
        }
        // ���û���ն���ѡ�����
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
            // ����Ŀ��Ϊ��ǰ��⵽��Ŀ�꣨�ն�����ң�
            if (attackTarget != null)
            {
                enemyAgent.destination = attackTarget.transform.position;
            }
        }
        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.transform.position) <= 1.5f)
        {
            if (attackTarget.CompareTag(Tag.BAITITEM))
            {
                
                Destroy(attackTarget); // �����ն�
                enemyStates = EnemyStates.DIGESTION;
                attackTarget = null; // �����ǰĿ��
            }
            else if (attackTarget.CompareTag(Tag.PLAYER))
            {
                PlayerCaught(); // �������
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
        Debug.Log("��ұ�����ץס�ˣ���Ϸʧ�ܣ�");

        // ֹͣ�����ƶ�
        enemyAgent.isStopped = true;


        // �������ʧ�ܵ��߼�
        GameManager.Instance.GameOver(DeadType.Enemy);
    }

    void DigestionBait()
    {
        isDigestion = true;
        if (digestionMaterial != null)
        {
            enemyRenderer.material = digestionMaterial;
        }
        if (digestionTime > 0)
        {
            digestionTime -= Time.deltaTime;
        }
        else
        {
            isDigestion = false;
            if (isGuard)
            {
                enemyStates = EnemyStates.GUARD;
            }
            else
            {
                enemyStates = EnemyStates.PATROL;
            }
            enemyAgent.destination = guardPos;
            enemyRenderer.material = normalMaterial;
        }
    }

    void BackToGuard()
    {
        isChase = false;
        if (transform.position != guardPos)
        {
            isWalk = true;
            enemyAgent.isStopped = false;
            enemyAgent.destination = guardPos;
            if (Vector3.SqrMagnitude(guardPos - transform.position) <= enemyAgent.stoppingDistance)
            {
                isWalk = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
            }
        }
    }

    void Dead()
    {
        Debug.Log("Enemy has been slained");
    }

    public void EndNotify()
    {
        //TODO: When player is dead, all of enemies will do the same thing
        isChase = false;
        isWalk = false;
        attackTarget = null;
        playerDead = true;
        enemyRenderer.material = alertMaterial;
        Debug.Log("���������");
    }
}
