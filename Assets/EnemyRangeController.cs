using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : EnemyController
{
    [SerializeField]
    public float shoot_radius = 10f;

    EnemyRangeAttack attack;

    public LayerMask player;

    private int current_range_patrol_point = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attack = GetComponent<EnemyRangeAttack>();

        if (detected == false)
            MoveToNextPatrolPoint();
    }

    // Update is called once per frame
    public override void Update()   //keep this since it derived from EnemyController
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool patrol = false;
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        if (distance <= radius)
        {
            agent.SetDestination(target.position);
            FacePlayer();
            detected = true;
        }
        else if (distance > radius && distance < shoot_radius)
        {
            agent.SetDestination(transform.position);
            FacePlayer();

            
            if (Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, player))
            {
                Debug.Log("Enemy Shooting");
                attack.RangeAttack();

            }

            detected = true;
        }
        else if (distance > shoot_radius && distance < chasing_radius && detected == true)
        {
            agent.SetDestination(target.position);
            FacePlayer();
            Invoke("Idle", 4);
        }

        patrol = !detected && patrol_points.Length > 0;
        if (patrol)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                MoveToNextPatrolPoint();
        }
    }

    public override void MoveToNextPatrolPoint()
    {
        if (patrol_points.Length > 0)
        {
            agent.SetDestination(patrol_points[current_range_patrol_point].position);
            current_range_patrol_point++;
            current_range_patrol_point %= patrol_points.Length;
        }
    }

    public override void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look_rot, Time.deltaTime * 5f);
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shoot_radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chasing_radius);
    }
}
