using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    
    public PlayerWeapon weapon;

    public GameObject bullet_prefab;
    public Transform fire_point;
    private WeaponGraphic graphic;

    private float next_time_to_fire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(camera == null)
        {
            Debug.LogError("No camera reference in PlayerShoot");
            this.enabled = false;
        }
        graphic = GetComponent<WeaponGraphic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= next_time_to_fire)
        {
            next_time_to_fire = Time.time + weapon.fire_rate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet_object = (GameObject)Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
        graphic.muzzle_flash.Play();
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
