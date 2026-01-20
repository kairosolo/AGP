using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPointList1;
    [SerializeField] private List<Transform> wayPointList2;
    [SerializeField] private List<Transform> wayPointList3;

    public List<Transform> WayPointList1
    { get { return wayPointList1; } }

    public List<Transform> WayPointList2
    { get { return wayPointList2; } }

    public List<Transform> WayPointList3
    { get { return wayPointList3; } }
}