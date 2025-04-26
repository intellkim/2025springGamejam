using UnityEngine;

public class Enemy2D : MonoBehaviour
{   
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;
    public int pocketMoneyValue = 50000;
    public PlayerController2D player;
    private bool isDead = false; // 죽었는지 여부
    public Sprite normalSprite; // 살아있을 때 스프라이트
    public Sprite skullSprite;  // 해골 스프라이트
    public Sprite soulRestSprite;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;
    public void SetMoveDirectionTowards(Vector2 targetPosition)
    {
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 12f || Mathf.Abs(transform.position.y) > 7f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeBowDamage()
    {
        currentHealth -= 50;
        Debug.Log($"적이 절을 맞음! 현재 체력: {currentHealth}");
        healthBar.SetHealth(currentHealth);
        if (isDead)
        {
            // 해골 상태라면 성불시킴
            SoulRest();
            return;
        }
        if (player != null)
        {
            player.RegisterHit(pocketMoneyValue);
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TurnIntoSkull()
    {
        if (isDead) return; // 이미 죽었으면 무시

        isDead = true;
        if (spriteRenderer != null && skullSprite != null)
        {
            spriteRenderer.sprite = skullSprite; // 해골 이미지로 교체
        }
        // 움직임 멈추기
        moveSpeed = 0f;

        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        // 일정 시간 후 Destroy
        Invoke("DestroySelf", 3f);
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    void Die()
    {
        Debug.Log("적 사망!");
        if (player != null)
        {
            player.RegisterKill();
        }
        gameObject.SetActive(false);
    }
    public bool IsDead()
    {
        return isDead;
    }
    void SoulRest()
    {
        Debug.Log("🌸 해골이 성불했습니다...");
    
        if (spriteRenderer != null && soulRestSprite != null)
        {
            spriteRenderer.sprite = soulRestSprite; // ✨ 성불 이미지로 변경
        }
        
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // 잠깐 보여주고 삭제
        Invoke("DestroySelf", 2f); // 2초 후 사라지게 (원하는 시간으로 조정 가능)
    }
}
