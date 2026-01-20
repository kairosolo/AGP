using UnityEngine;

public class SampleRaycast : MonoBehaviour
{
    [SerializeField] private float dis;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material hitMaterial;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, dis, layer, QueryTriggerInteraction.Ignore))
        {
            MeshRenderer mesh = hitInfo.transform.GetComponent<MeshRenderer>();
            mesh.material = hitMaterial;
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * dis, Color.green);
        }
    }
}