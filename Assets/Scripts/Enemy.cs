using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public MiniCharactor player;

    // 目的地
    public Vector3 targetPoint;
    public Rigidbody rb;

    // 巡回座標
    public Vector3[] patrolPoint;
    public int currentIndex;
    
    void Start()
    {
        TryGetComponent<NavMeshAgent>(out agent);

        TryGetComponent<Rigidbody>(out rb);

        player = GameObject.FindAnyObjectByType<MiniCharactor>();
    }

    void Update()
    {
        rb.linearVelocity = Vector3.zero;

        Vector3 posA = player.transform.position;
        Vector3 posB = transform.position;
        float distance = Vector3.Distance(posA, posB);

        if(distance < 3)
        {
            targetPoint = posA;
        }
        else
        {
            targetPoint = patrolPoint[currentIndex % patrolPoint.Length];
        }

        float patrollDisance = Vector3.Distance(patrolPoint[currentIndex], transform.position);
        if(patrollDisance < 0.5f)
        {
            currentIndex++;
        }

        // エージェントに目的地を設定
        agent.SetDestination(targetPoint);
    }
}
