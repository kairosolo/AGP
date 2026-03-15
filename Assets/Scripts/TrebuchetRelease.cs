using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class TrebuchetRelease : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private float releaseAngle;
    [SerializeField] private float currentAngle;

    [SerializeField] private Transform line;
    [SerializeField] private Rope rope;
    [SerializeField] private SpringJoint projectileJoint;
    [SerializeField] private HingeJoint hinge;

    private void Update()
    {
        currentAngle = hinge.angle;

        if (projectileJoint != null && currentAngle >= releaseAngle)
        {
            projectileJoint.connectedBody = null;
            rope.SetEndPoint(line);
            Destroy(projectileJoint);
        }
    }
}