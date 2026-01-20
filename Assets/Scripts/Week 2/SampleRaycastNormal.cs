using UnityEngine;
using UnityEngine.InputSystem;

public class SampleRaycastNormal : MonoBehaviour
{
    [SerializeField] private Transform objectToPlace;
    [SerializeField] private Camera gameCamera;

    private void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            objectToPlace.position = hit.point;
            objectToPlace.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
}