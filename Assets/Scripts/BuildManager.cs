using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] buildingPrefabs;

    private int selectedBuilding = 0;

    void Awake()
    {
        main = this;   
    }

    public GameObject GetSelectedBuilding()
    {
        return buildingPrefabs[selectedBuilding];
    }
}
