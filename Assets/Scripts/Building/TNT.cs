using UnityEngine;
using Mono.Cecil;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
public class TNT : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Attributes")]
    [SerializeField] private float TNTSpeed = 8f;
    [SerializeField] private int TNTDamage = 100;
    [SerializeField] private Transform target;
    

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb2d.linearVelocity = direction * TNTSpeed;
        
        RotateTNTTowardsTarget(direction);
    }

    public void SetTarget(Transform _target) {
        target = _target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health> ().TakeDamage(TNTDamage);
        
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void RotateTNTTowardsTarget(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    } 
}
