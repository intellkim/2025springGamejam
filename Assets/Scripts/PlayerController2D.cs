using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController2D : MonoBehaviour
{   
    public GameObject grimReaperPrefab; // 소환할 저승사자 프리팹
    private bool grimReaperSpawned = false;
    public float moveSpeed = 5f;
    public float checkDistance = 1.5f;
    public Animator animator;
    public LayerMask enemyLayer;
    private Vector2 lastMoveDir = Vector2.down;
    public TextMeshProUGUI pocketMoneyText;
    public int bowCount = 0;
    public int killCount = 0;
    public int pocketMoney = 0;

    void Update()
    {
        Move();
        // 절하기
        if (Input.GetKeyDown(KeyCode.Z))
        {   
            animator?.SetTrigger("Bow"); // 애니메이션 있으면 실행
            TryBowHit();
        }
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(x, y);

        if (moveDir != Vector2.zero)
        {
            animator?.SetBool("IsWalking", true);
            lastMoveDir = moveDir.normalized;
            
            if (moveDir.x > 0)
                GetComponent<SpriteRenderer>().flipX = false; // 오른쪽
            else if (moveDir.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
            // ✅ 추가: 이동할 때 걷는 애니메이션 켜기
        }
        else
        {
            // ✅ 추가: 멈출 때 걷기 끄고 Idle로 전환
            animator?.SetBool("IsWalking", false);
        }
    }

    void TryBowHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMoveDir, checkDistance, enemyLayer);
        
        Debug.DrawRay(transform.position, lastMoveDir * checkDistance, Color.red, 2f);

        if (hit.collider != null)
        {
            Debug.Log("Ray가 맞은 오브젝트: " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy2D>().TakeBowDamage();
            }
        }
        else
        {
            Debug.Log("절했지만 맞은 대상 없음");
        }
    }

    public void RegisterKill(){
        killCount++;
        Debug.Log($"죽인 적 수: {killCount}");

        if (killCount >= 1){
            TriggerSsggEnding();
        }
    }
    void TriggerSsggEnding()
    {
        Debug.Log("😈 싸가지 엔딩: 넙죽 깡패 🩸");
        SceneManager.LoadScene("SsggEndingScene");
    }
    public void RegisterHit(int money)
    {
        bowCount++;
        pocketMoney += money;
        UpdatePocketMoneyUI();
        Debug.Log($"절이 맞았음! 현재 절 카운트: {bowCount}, 세뱃돈: {pocketMoney}만원");

        if (!grimReaperSpawned && bowCount >= 5)
        {
            SpawnGrimReaper();
        }

        if (bowCount >= 10 && killCount == 0)
        {
            TriggerHeavenEnding();
        }
    }
    void TriggerHeavenEnding()
    {
        Debug.Log("🌸 극락왕생 엔딩 🌸");
        SceneManager.LoadScene("HeavenEndingScene");
    }
    void UpdatePocketMoneyUI()
    {
        if (pocketMoneyText != null)
        {
            pocketMoneyText.text = $"세뱃돈: {pocketMoney}만원";
        }
    }
    void SpawnGrimReaper()
    {
        grimReaperSpawned = true;
        Instantiate(grimReaperPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        Debug.Log("👻 저승사자 등장!!");
    }

    Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-6f, 6f);
        return new Vector2(x, y);
    }
}
