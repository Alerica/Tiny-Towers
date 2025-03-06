using UnityEngine;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using System.Timers;
using UnityEngine.UI;
using Unity.Mathematics;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bowRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float targetInRange = 8f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float aps = 1f; // Attack per second
    [SerializeField] private int baseCost = 100;

    private float apsBase;
    private float targetInRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;
    
    void Start()
    {
        apsBase = aps;
        targetInRangeBase = targetInRange;
        upgradeButton.onClick.AddListener(UpgradeBuilding);
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

    public void OpenUpgradeUI () 
    {
        upgradeUI.SetActive(true);
    }

     public void CloseUpgradeUI () 
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void UpgradeBuilding()
    {
        if(CalculateCost() > GameManager.main.GetGold()) return;

        GameManager.main.DecreaseGold(CalculateCost());

        level ++;
        aps = CalculateAPS();
        targetInRange = CalculateRange();
        CloseUpgradeUI();

        Debug.Log("New APS: " + aps);
        Debug.Log("New Range: " + targetInRange);
        Debug.Log("New Cost" + CalculateCost());
        
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateAPS()
    {
        return apsBase * Mathf.Pow(level, 0.5f);
    }

    private float CalculateRange()
    {
        return targetInRangeBase * Mathf.Pow(level, 0.3f);
    }
 
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetInRange);
    }
}
