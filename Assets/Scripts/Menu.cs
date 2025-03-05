using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI goldUI;
    Animator animator;
    private bool isMenuOpen = true;
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
    }
}
