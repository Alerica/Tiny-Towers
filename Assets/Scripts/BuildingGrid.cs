using UnityEngine;
using UnityEngine.Rendering;

public class BuildingGrid : MonoBehaviour
{
    [Header("References")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;
    private GameObject building;
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
        Debug.Log("Build Tower at " + transform.position);
        if (building != null) return;

        Building buildingToBuild = BuildManager.main.GetSelectedBuilding();
        if(buildingToBuild.cost > GameManager.main.GetGold())
        {
            Debug.Log("Can`t Afford");
            return;
        }

        GameManager.main.DecreaseGold(buildingToBuild.cost);
        
        building = Instantiate(buildingToBuild.prefab, transform.position, Quaternion.identity);
    }
}
