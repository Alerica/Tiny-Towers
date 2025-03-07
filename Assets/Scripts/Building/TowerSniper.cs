using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TowerSniper : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject TNTPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private AudioSource upgradeAudio;

    [Header("Attribute")]
    [SerializeField] private float targetInRange = 8f;
    [SerializeField] private float aps = 0.5f; // Attack per second
    [SerializeField] private int baseCost = 200;

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

    private bool CheckTargetIsInRange() 
    {
        return Vector2.Distance(target.position, transform.position) < targetInRange;
    }

    private void Shoot() 
    {
        GameObject TNTObj = Instantiate(TNTPrefab, firingPoint.position, Quaternion.identity);
        TNT TNTScript = TNTObj.GetComponent<TNT>();
        TNTScript.SetTarget(target);

        if (shootAudio != null)
        {
            shootAudio.Play();
        }
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
        
        if (upgradeAudio != null)
        {
            upgradeAudio.Play();
        }
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
