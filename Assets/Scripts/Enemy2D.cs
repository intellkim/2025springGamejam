using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;

    public PlayerController2D player;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeBowDamage()
    {
        currentHealth -= 50;
        Debug.Log($"적이 절을 맞음! 현재 체력: {currentHealth}");
        healthBar.SetHealth(currentHealth);
        
        player.RegisterHit();
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("적 사망!");
        player.RegisterKill();
        gameObject.SetActive(false);
    }
}
