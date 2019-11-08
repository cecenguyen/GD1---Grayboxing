using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    
    [Header("Weapon")]
    public PlayerWeapon weapon;

    public GameObject bullet_prefab;
    public Transform fire_point;
    // Start is called before the first frame update
    void Start()
    {
        if(camera == null)
        {
            Debug.LogError("No camera reference in PlayerShoot");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shoot");
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet_object = (GameObject)Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
        Bullet bullet = bullet_object.GetComponent<Bullet>();
        bullet.SetDirection(fire_point.forward);
        bullet.SetDamage(weapon.damage);
        /*//Store info of what we hit when the projectile hit
        RaycastHit hit;

        //Raycast syntax: origin, direction, hitinfo, maxdistance, what we should hit
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,
            weapon.range, mask))
        {
            Debug.Log("Shooting at " + hit.collider.name);
            if (hit.collider.tag == "Enemy")
            {
                EnemyShot(hit.collider.name, weapon.damage);
            }
        }*/
    }   
}
