using Mono.Cecil;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb2d;

    [Header("Attributes")]
    [SerializeField] private float arrowSpeed = 6f;
    [SerializeField] private int arrowDamage = 25;
    [SerializeField] private Transform target;
    

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb2d.linearVelocity = direction * arrowSpeed;
        
        RotateArrowTowardsTarget(direction);
    }

    public void SetTarget(Transform _target) {
        target = _target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health> ().TakeDamage(arrowDamage);
        Destroy(gameObject);
    }

    private void RotateArrowTowardsTarget(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    } 
}
