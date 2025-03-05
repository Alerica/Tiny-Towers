using UnityEngine;
using UnityEngine.Rendering;

public class Grid : MonoBehaviour
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

        GameObject buildingToBuild = BuildManager.main.GetSelectedBuilding();
        building = Instantiate(buildingToBuild, transform.position, Quaternion.identity);
    }
}
