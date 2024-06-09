using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float health;
    public float attackDamage;
    public float attackRange; // 攻击范围
    public string enemyName;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UIManager.instance.UpdateCombatLog($"{enemyName} took {damage} damage.");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{enemyName} is dead!");
        UIManager.instance.UpdateCombatLog($"{enemyName} died.");
        GameManager.instance.OnEnemyDeath(this);
        Destroy(gameObject); // 销毁敌人的游戏对象
    }

    public void PerformAction(Player player)
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.TakeDamage(attackDamage);
        }
        else
        {
            UIManager.instance.UpdateCombatLog("Player is out of range.");
        }
        GameManager.instance.CheckEnemiesInRange(); // 每次攻击后检查范围内的敌人
    }

    public void StartCombat()
    {
        UIManager.instance.UpdateCombatLog($"{enemyName} started combat.");
    }

    public void EndCombat()
    {
        UIManager.instance.UpdateCombatLog($"{enemyName} ended combat.");
    }

    // 在编辑器中绘制攻击范围
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
