using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private PopAnimator popAnimator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (GameManager.Instance.IsGameOver) return;

            popAnimator.TriggerPopAnimation();
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, enemyMask))
            {
                if (hit.transform.TryGetComponent<Animator>(out Animator animator))
                {
                    animator.enabled = false;
                }
            }
        }
    }
}