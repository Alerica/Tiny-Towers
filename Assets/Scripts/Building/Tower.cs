using UnityEngine;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using System.Timers;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bowRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetInRange = 8f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float aps = 1f; // Attack per second

    private Transform target;
    private float timeUntilFire;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();
        if(!CheckTargetIsInRange())
        {
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 1f / aps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void FindTarget() 
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            transform.position, 
            targetInRange, 
            (Vector2) transform.position, 0f, 
            enemyMask);

        if(hits.Length > 0) {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bowRotationPoint.rotation = Quaternion.RotateTowards(bowRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange() 
    {
        return Vector2.Distance(target.position, transform.position) < targetInRange;
    }

    private void Shoot() 
    {
        GameObject arrowObj = Instantiate(arrowPrefab, firingPoint.position, Quaternion.identity);
        Arrow arrowScript = arrowObj.GetComponent<Arrow>();
        arrowScript.SetTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetInRange);
    }
}
