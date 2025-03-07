using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] private AudioSource toggleAudio;
    Animator animator;
    private bool isMenuOpen = false;
    public bool mouseOver = false;
    void OnGUI()
    {
        goldUI.text = GameManager.main.GetGold().ToString();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSelected()
    {

    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        animator.SetBool("isMenuOpen", isMenuOpen);

        if (toggleAudio != null)
        {
            toggleAudio.Play();
        }
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
    }
}
