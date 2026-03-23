using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    [SerializeField] private bool checkPointReached = false;
    [SerializeField] private bool isFinishLine = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!checkPointReached && collision.gameObject.CompareTag("Player"))
        {
            if (isFinishLine)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                checkPointReached = true;
                GameManager.Instance.SetCheckpoint(transform.position);
            }
        }
    }
}