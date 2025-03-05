using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform startPoint;
    public Transform[] path;

    void Awake()
    {
        main = this;
    }
}
