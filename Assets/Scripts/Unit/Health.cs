using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private int enemyGold = 20;
    private bool isDestroyed = false;

    public void TakeDamage(int damage) 
    {
        hitPoints -= damage;
        if(hitPoints <= 0 && !isDestroyed) 
        {
            isDestroyed = true;
            GameManager.main.IncreaseGold(enemyGold);
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }


}
