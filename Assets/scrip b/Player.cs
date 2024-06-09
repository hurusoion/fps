using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public float health;
    public float attackDamage;
    public float attackRange; // 攻击范围
    public string playerName;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        GameManager.instance.OnStateChange += HandleStateChange;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.PrepareCombat();
            GameManager.instance.StartCombat();
        }
        else if (other.CompareTag("Chest"))
        {
            GameManager.instance.OpenChest();
        }
    }

    private void HandleStateChange(GameState newState)
    {
        if (newState == GameState.PrepareCombat || newState == GameState.OpenChest || newState == GameState.Combat)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        UIManager.instance.UpdatePlayerHealthText();
        UIManager.instance.UpdateCombatLog($"Player took {damage} damage.");
        GameManager.instance.CheckEnemiesInRange(); // 每次受伤后检查范围内的敌人
    }

    public void PerformAction(Enemy enemy)
    {
        if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= attackRange)
        {
            enemy.TakeDamage(attackDamage);
            UIManager.instance.UpdateCombatLog($"Player attacked {enemy.enemyName} for {attackDamage} damage.");
        }
        else
        {
            UIManager.instance.UpdateCombatLog("Enemy is out of range.");
        }
        GameManager.instance.CheckEnemiesInRange(); // 每次攻击后检查范围内的敌人
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
        UIManager.instance.UpdateCombatLog("Player died.");
        GameManager.instance.EndCombat();
    }

    // 在编辑器中绘制攻击范围
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
