using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver = false;

    void Update()
    {
        if(mouseOver) Debug.Log("Mouse over: " + name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        UIManager.main.SetHoveringState(false);
        gameObject.SetActive(false);
    }
}
