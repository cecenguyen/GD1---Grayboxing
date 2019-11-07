using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public float radius = 10f;
    public float default_radius = 15f;

    public bool detected = false;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameManager.instance.player_object.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= radius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FacePlayer();
            }
        }
    }

    public virtual void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look_rot, Time.deltaTime * 5f);
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
