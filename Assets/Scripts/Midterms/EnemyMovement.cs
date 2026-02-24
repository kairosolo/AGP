using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void DisableAnim()
    {
        anim.enabled = false;
    }
}