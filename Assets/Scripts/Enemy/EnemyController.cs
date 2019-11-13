using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public float radius = 10f;
    public float chasing_radius = 15f;
    public bool detected = false;

    public Transform target;
    public NavMeshAgent agent;

    private int current_patrol_point = 0;

    [Header("Patrol setup")]
    public Transform[] patrol_points;

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameManager.instance.player_object.transform;
        agent = GetComponent<NavMeshAgent>();

        if (detected == false)
            MoveToNextPatrolPoint();
    }


    public virtual void Update()
    {
        //if (detected == false)
            //MoveToNextPatrolPoint();

        float distance = Vector3.Distance(target.position, transform.position);
        bool patrol = false;

        if (distance <= radius)
        {
            agent.SetDestination(target.position);
            FacePlayer();
            detected = true;
        }
        else if (distance < chasing_radius && detected == true)
        {
            agent.SetDestination(target.position);
            FacePlayer();
        }
        else if (distance > chasing_radius)
        {
            Invoke("Idle", 4);
        }

        patrol = !detected && patrol_points.Length > 0;
        if (patrol)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                MoveToNextPatrolPoint();
        }
    }

    public virtual void MoveToNextPatrolPoint()
    {
        if(patrol_points.Length > 0)
        {
            agent.SetDestination(patrol_points[current_patrol_point].position);
            current_patrol_point++;
            current_patrol_point %= patrol_points.Length;
        }
    }

    public void Idle()
    {
        detected = false;
    }

    public virtual void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look_rot, Time.deltaTime * 5f);
    }

    #region Gizmo
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chasing_radius);
    }
    #endregion
}
