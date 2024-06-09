using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public IEnumerator CombatRoutine()
    {
        while (GameManager.instance.CurrentState == GameState.Combat)
        {
            foreach (var enemy in GameManager.instance.enemies)
            {
                if (enemy != null && enemy.health > 0)
                {
                    float enemyDamage = enemy.attackDamage;
                    UIManager.instance.UpdateCombatLog($"Enemy {enemy.enemyName} attacks Player for {enemyDamage} damage.");
                    enemy.PerformAction(GameManager.instance.player); // 確保敵人執行行動
                    yield return new WaitForSeconds(1f); // 等待1秒以模擬敵人行動間隔
                }
            }

            // 檢查所有敵人是否已死亡
            if (AllEnemiesDead())
            {
                UIManager.instance.ShowEndCombatButton();
                break;
            }
        }
    }

    private bool AllEnemiesDead()
    {
        foreach (var enemy in GameManager.instance.enemies)
        {
            if (enemy != null && enemy.health > 0)
            {
                return false;
            }
        }
        return true;
    }
}
