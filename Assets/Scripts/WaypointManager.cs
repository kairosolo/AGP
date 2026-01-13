using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPointList;
    public List<Transform> WayPointList
    { get { return wayPointList; } }
}