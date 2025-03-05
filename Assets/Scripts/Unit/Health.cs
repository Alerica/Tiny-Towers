using UnityEditor.PackageManager;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 100;

    public void TakeDamage(int damage) 
    {
        hitPoints -= damage;
        if(hitPoints <= 0) 
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }


}
