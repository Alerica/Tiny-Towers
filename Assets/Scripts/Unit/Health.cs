using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 100;
    private bool isDestroyed = false;

    public void TakeDamage(int damage) 
    {
        hitPoints -= damage;
        if(hitPoints <= 0 && !isDestroyed) 
        {
            isDestroyed = true;
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }


}
