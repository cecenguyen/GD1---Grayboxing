using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : EnemyController
{
    [SerializeField]
    public float shoot_radius = 10f;

    public float chasing_radius = 15f;

    Transform target_range;
    NavMeshAgent agent_range;

    // Start is called before the first frame update
    void Start()
    {
        target_range = GameManager.instance.player_object.transform;
        agent_range = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public override void Update()   //keep this since it derived from EnemyController
    {
        float distance = Vector3.Distance(target_range.position, transform.position);

        if (distance <= radius)
        {
            agent_range.SetDestination(target_range.position);
            FacePlayer();
            detected = true;
        }
        else if (distance > radius && distance < shoot_radius && detected == true)
        {
            agent_range.SetDestination(transform.position);
            FacePlayer();
            detected = true;
        }
        else if (distance > shoot_radius && distance < chasing_radius && detected == true)
        {
            agent_range.SetDestination(target_range.position);
            FacePlayer();
            Invoke("Idle", 3);
        }
    }

    public void Idle()
    {
        detected = false;
    }

    public override void FacePlayer()
    {
        Vector3 direction = (target_range.position - transform.position).normalized;
        Quaternion look_rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look_rot, Time.deltaTime * 5f);
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shoot_radius);
    }
}
