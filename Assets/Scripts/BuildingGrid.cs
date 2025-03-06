using UnityEngine;
using UnityEngine.Rendering;

public class BuildingGrid : MonoBehaviour
{
    [Header("References")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;
    private GameObject buildingObject;
    public Tower tower;
    private Color startColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        startColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = startColor;
    }

    void OnMouseDown()
    {
        if(UIManager.main.IsHoveringUI()) return;
        
        Debug.Log("Build Tower at " + transform.position);
        if (buildingObject != null) 
        {
            tower.OpenUpgradeUI();
            return;
        }
        Building buildingToBuild = BuildManager.main.GetSelectedBuilding();
        if(buildingToBuild.cost > GameManager.main.GetGold())
        {
            Debug.Log("Can`t Afford");
            return;
        }

        GameManager.main.DecreaseGold(buildingToBuild.cost);
        
        buildingObject = Instantiate(buildingToBuild.prefab, transform.position, Quaternion.identity);
        tower = buildingObject.GetComponent<Tower>();
    }
}
