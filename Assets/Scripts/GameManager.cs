using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI liveText;

    public static GameManager main;
    public Transform startPoint;
    public Transform[] path;

    [SerializeField] private int gold;
    [SerializeField] private int live;
    void Awake()
    {
        main = this;
    }

    void Start() 
    {
        live = 10;
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

    public int GetGold() 
    {
        return gold;
    }

    public bool DecreaseLive()
    {
        live--;
        return true;
    }

    public void UpdateLiveUI()
    {
        liveText.text = live.ToString();
    }
}
