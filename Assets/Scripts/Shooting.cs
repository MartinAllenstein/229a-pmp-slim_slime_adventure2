using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float shootCooldown = 0.5f;
    private float lastShootTime = -999f;
    private bool isShooting = false;
    
    Animator animator;

    public AudioClip jumpSound;
    public AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime > shootCooldown)
        {
            //Delay
            lastShootTime = Time.time;
            
            //shoot raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 3f);
            
            //get click point
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            
            //if hit
            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                //Debug.Log("Hit " + hit.collider.name);

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                
                //shoot bullet
                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                
                //add velocity
                shootBullet.linearVelocity = projectileVelocity;

                audioSource.PlayOneShot(jumpSound);
                animator.SetBool("IsAttack" , true);
                
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;
        
        //find velocity of x,y
        float velocityX = direction.x / time;
        float velocityY = direction.y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        
        //get projectile vector
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
        
        return projectileVelocity;
    }
    
    void StopAttackAnim()
    {
        animator.SetBool("IsAttack", false);
    }

}
