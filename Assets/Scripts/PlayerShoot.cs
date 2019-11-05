using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask mask;

    public PlayerWeapon weapon;
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
        //Store info of what we hit when the projectile hit
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
        }
    }   

    void EnemyShot(string name, int damage)
    {
        Debug.Log(name + " has been shot");

        Enemy enemy = GameManager.GetEnemy(name);
        if(enemy.isAlive == true)
            enemy.TakeDamage(damage);
    }
}
