using UnityEngine;
using UnityEngine.AI;

public class Botspinning : MonoBehaviour
{
    public GameObject Bot;
    private NavMeshAgent navMeshAgent;
    public Transform Player;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(Player.position);
    }

    private void Update()
    {
            Bot.transform.Rotate(45f, 0f, 0f);
            navMeshAgent.SetDestination(Player.position);
    }
}