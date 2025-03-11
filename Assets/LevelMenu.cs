using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject uiPanel; 

    private bool isPointerOver = false;

    void Update()
    {
        if (isPointerOver == false)
        {
            uiPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
    }
}
