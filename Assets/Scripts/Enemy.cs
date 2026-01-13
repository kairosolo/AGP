using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material normalColor;
    [SerializeField] private Material hitColor;
    [SerializeField] private float health;
    [SerializeField] private float currentHealh;
    [SerializeField] private float speed;

    private WaypointManager waypointManager;
    private List<Transform> patrolPointList;
    private int currentPoint = 0;

    private void Awake()
    {
        currentHealh = health;
        waypointManager = FindFirstObjectByType<WaypointManager>();
    }

    private void Start()
    {
        patrolPointList = waypointManager.WayPointList;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.Translate(Vector3.MoveTowards(transform.position, patrolPointList[currentPoint].position, step) - transform.position);
        if (Vector3.Distance(transform.position, patrolPointList[currentPoint].position) <= 0.1f)
        {
            currentPoint++;
            if (currentPoint > patrolPointList.Count - 1)
            {
                currentPoint = 0;
            }
        }
    }

    public void DecreaseHealth(float decrease)
    {
        currentHealh -= decrease;
        healthBarImage.fillAmount = currentHealh / health;

        if (currentHealh <= 0)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        Instantiate(hitVFX, transform.position, Quaternion.identity);

        StopCoroutine(EnemyHit());
        StartCoroutine(EnemyHit());
    }

    private IEnumerator EnemyHit()
    {
        meshRenderer.material = hitColor;
        yield return new WaitForSeconds(.5f);
        meshRenderer.material = normalColor;
    }
}