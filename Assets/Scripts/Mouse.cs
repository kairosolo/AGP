using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mouse : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    private bool isDead = false;

    public void TriggerMouseDeath()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Die");
        ScoreManager.Instance.IncreaseScore(50);
        EnemySpawner.Instance.DecreaseCurrentEnemies();
    }
}