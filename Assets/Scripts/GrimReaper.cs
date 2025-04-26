using UnityEngine;

public class GrimReaper : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Enemy2D targetEnemy;
    
    private int soulsCollected = 0; // 데려간 어르신 수
    private int soulTarget = 3;  

    void Start()
    {
        FindNewTarget(); // 시작하자마자 타겟 찾기
    }

    void Update()
    {   
        if (soulsCollected >= soulTarget)
        {
            Debug.Log("👻 저승사자 임무 완료! 사라진다.");
            Destroy(gameObject);
            return;
        }
        if (targetEnemy != null)
        {
            Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) < 0.5f)
            {
                targetEnemy.TurnIntoSkull();
                Debug.Log("저승사자가 어르신을 데려갔다!");
                soulsCollected++;

                FindNewTarget(); // 다음 타겟 찾기
            }
        }
        else
        {
            FindNewTarget(); // 타겟 없으면 계속 찾기 시도
        }
    }

    void FindNewTarget()
    {
        Enemy2D[] enemies = FindObjectsOfType<Enemy2D>();
        var aliveEnemies = new System.Collections.Generic.List<Enemy2D>();

        foreach (var enemy in enemies)
        {
            if (!enemy.IsDead()) // ❗ 살아있는 것만 모으기
            {
                aliveEnemies.Add(enemy);
            }
        }

        if (aliveEnemies.Count > 0)
        {
            targetEnemy = aliveEnemies[Random.Range(0, aliveEnemies.Count)];
        }
        else
        {
            targetEnemy = null;
            Debug.Log("👻 살아있는 어르신이 없다...");
        }
    }
}
