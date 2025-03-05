using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public Transform startPoint;
    public Transform[] path;

    [SerializeField] private int gold;

    void Awake()
    {
        main = this;
    }

    void Start() 
    {
        gold = 100;
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
    }

    public bool DecreaseGold(int amount)
    {
        if(amount <= gold) 
        {
            gold -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough gold");
        }
        return false;
    }
}
