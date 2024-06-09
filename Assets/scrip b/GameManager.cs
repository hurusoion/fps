using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action<GameState> OnStateChange;

    private GameState currentState;

    public Enemy[] enemies; // 定义敌人数组
    public Player player; // 添加 player 属性

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Exploration);
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"Changing state to: {newState}");
        if (newState != currentState)
        {
            currentState = newState;
            OnStateChange?.Invoke(newState); // 触发事件
        }
    }

    public void StartCombat()
    {
        Debug.Log("Combat started");
        ChangeState(GameState.Combat);

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.StartCombat(); // 确保每个敌人开始战斗
            }
        }

        StartCoroutine(CombatRoutine());
    }

    public void EndCombat()
    {
        Debug.Log("Combat ended");
        ChangeState(GameState.Exploration);

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.EndCombat(); // 确保每个敌人结束战斗
            }
        }
    }

    public void OpenChest()
    {
        ChangeState(GameState.OpenChest);
    }

    public void PrepareCombat()
    {
        ChangeState(GameState.PrepareCombat);
    }

    public GameState CurrentState
    {
        get { return currentState; }
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        Debug.Log($"Enemy {enemy.enemyName} has died.");
        UIManager.instance.UpdateCombatLog($"{enemy.enemyName} has died.");
        CheckEnemiesInRange(); // 每次敌人死亡后检查范围内的敌人
    }

    public void CheckEnemiesInRange()
    {
        bool enemiesInRange = false;
        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.health > 0 && Vector3.Distance(player.transform.position, enemy.transform.position) <= player.attackRange)
            {
                enemiesInRange = true;
                break;
            }
        }
        if (!enemiesInRange)
        {
            UIManager.instance.ShowEndCombatButton();
        }
    }

    private IEnumerator CombatRoutine()
    {
        while (CurrentState == GameState.Combat)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null && enemy.health > 0)
                {
                    float enemyDamage = enemy.attackDamage;
                    UIManager.instance.UpdateCombatLog($"Enemy {enemy.enemyName} attacks Player for {enemyDamage} damage.");
                    enemy.PerformAction(player); // 确保敌人执行行动
                    yield return new WaitForSeconds(1f); // 等待1秒以模拟敌人行动间隔
                }
            }
            CheckEnemiesInRange(); // 每次敌人行动后检查范围内的敌人
        }
    }
}

public enum GameState
{
    Exploration,
    Combat,
    PrepareCombat,
    OpenChest
}
