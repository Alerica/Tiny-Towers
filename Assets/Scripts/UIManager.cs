using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    private bool isHoveringUI;

    void Awake()
    {
        main = this;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }
}
