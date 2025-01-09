using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float freezeDuration = 2.0f;
    bool isFrozen = false;
    NavMeshAgent agent;

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnHitByBullet()
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeMovement());
        }
    }
    private System.Collections.IEnumerator FreezeMovement()
    {
        isFrozen = true;

        agent.isStopped = true;

        yield return new WaitForSeconds(freezeDuration);

        agent.isStopped = false;
        isFrozen = false;
    }
void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
    }
}
