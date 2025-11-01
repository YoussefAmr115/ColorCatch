using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Behavior Settings")]
    public float chaseDistance = 10f; 
    public float penaltyTime = 5f;    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        
        if (distance <= chaseDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            GameManager.Instance.ApplyEnemyPenalty(penaltyTime);
        }
    }
}
