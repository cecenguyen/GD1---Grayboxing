using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouse_sensitivity = 3f;
    [SerializeField]
    private float jump_force = 5000f;

    private static int frame = 0;

    private PlayerMotor motor;
    //Reset frame when touch ground
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
            frame = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        float x_move = Input.GetAxisRaw("Horizontal");
        float z_move = Input.GetAxisRaw("Vertical");

        Vector3 move_hor = transform.right * x_move;
        Vector3 move_ver = transform.forward * z_move;

        //Calculating velocity base on horizontal movement and verticle movement
        Vector3 vel = (move_hor + move_ver).normalized * speed;

        //Actual movement
        motor.Move(vel);

        //Rotation y (only right or left not up or down)
        float y_rotate = Input.GetAxisRaw("Mouse X");

        //Declare vector rotation to keep track of player rotation
        Vector3 rotation = new Vector3(0f, y_rotate, 0f) * mouse_sensitivity;

        //Actual rotation
        motor.Rotate(rotation);

        //Rotation x is base on camera rotation
        float x_rotate = Input.GetAxisRaw("Mouse Y");

        //Declare vector rotation to keep track of player rotation
        float camera_rot_x = x_rotate * mouse_sensitivity;

        //Actual rotation
        motor.RotateCamera(camera_rot_x);

        Vector3 jump = Vector3.zero;
        //Actual jump
        if (Input.GetButton("Jump") && frame < 10)
        {
            jump = Vector3.up * jump_force;
            frame++;
        }
        motor.Jump(jump);
    }
}
