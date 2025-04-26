using UnityEngine;

public class Enemy2D : MonoBehaviour
{   
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;

    public PlayerController2D player;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if (transform.position.x < -10f)  // 이 값은 화면 크기에 따라 조정 가능
        {
            Destroy(gameObject);
        }
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
