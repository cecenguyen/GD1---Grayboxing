using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam; //to check if we have a camera
    [SerializeField]
    private float cam_rot_limit = 85f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 j_force = Vector3.zero;
    private float cam_rot_x = 0f;
    private float cur_cam_rot_x = 0f;

    private Rigidbody rb;

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 v)
    {
        velocity = v;
    }

    public void Rotate(Vector3 r)
    {
        rotation = r;
    }

    public void RotateCamera(float rotation)
    {
        cam_rot_x = rotation;
    }

    public void Jump(Vector3 jump_force)
    {
        j_force = jump_force;
    }

    // Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (j_force != Vector3.zero)
        {
            rb.AddForce(j_force * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void PerformRotation()
    {
        //Math stuff google please
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //Set the rotation
            cur_cam_rot_x -= cam_rot_x;
            //Clamp the rotation
            cur_cam_rot_x = Mathf.Clamp(cur_cam_rot_x, -cam_rot_limit, cam_rot_limit);

            //Actual rotating
            cam.transform.localEulerAngles = new Vector3(cur_cam_rot_x, 0f, 0f);
        }
    }
}
