using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour
{
    public float checkDistance = 1.5f;
    public Animator animator;
    public LayerMask enemyLayer;
    private Vector2 lastMoveDir = Vector2.down;

    public int bowCount = 0;
    public int killCount = 0;

    void Update()
    {
        // 방향키 입력
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(x, y);

        if (moveDir != Vector2.zero)
        {
            lastMoveDir = moveDir.normalized;
        }

        // 절하기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator?.SetTrigger("Bow"); // 애니메이션 있으면 실행
            TryBowHit();
        }
    }

    void TryBowHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMoveDir, checkDistance, enemyLayer);
        Debug.DrawRay(transform.position, lastMoveDir * checkDistance, Color.red, 1f);

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
    public void RegisterHit()
    {
        bowCount++;
        Debug.Log($"절이 맞았음! 현재 절 카운트: {bowCount}");

        if (bowCount >= 108 && killCount == 0)
        {
            TriggerHeavenEnding();
        }
    }
    void TriggerHeavenEnding()
    {
        Debug.Log("🌸 극락왕생 엔딩 🌸");
        SceneManager.LoadScene("HeavenEndingScene");
    }
}
