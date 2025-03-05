using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb2d;

    [Header("Attributes")]
    [SerializeField]
    private float moveSpeed = 3f;

    private Transform target;
    private int pathIndex = 0;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameManager.main.path[pathIndex];
    }

    void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if(pathIndex == GameManager.main.path.Length) 
            {
                Destroy(gameObject);   
                return; 
            } 
            target = GameManager.main.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb2d.linearVelocity = direction * moveSpeed;
    }
}
