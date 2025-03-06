using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    // [SerializeField] private GameObject[] buildingPrefabs;
    [SerializeField] private Building[] buildings;

    private int selectedBuilding = 0;

    void Awake()
    {
        main = this;   
    }

    public Building GetSelectedBuilding()
    {
        return buildings[selectedBuilding];
    }

    public void SetSelectedBuilding(int _selectedBuilding)
    {
        selectedBuilding = _selectedBuilding;
    }
}
