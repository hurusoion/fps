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
                    enemy.PerformAction(GameManager.instance.player); // �T�O�ĤH������
                    yield return new WaitForSeconds(1f); // ����1��H�����ĤH��ʶ��j
                }
            }

            // �ˬd�Ҧ��ĤH�O�_�w���`
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
