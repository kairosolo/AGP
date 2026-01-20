using System.Collections;
using UnityEngine;

public class PopAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 popScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    public void TriggerPopAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(PopAnimationCoroutine());
    }

    private IEnumerator PopAnimationCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(initialScale, popScale, elapsedTime / (animationDuration / 2f));
            elapsedTime += Time.deltaTime;
        }
        transform.localScale = popScale;

        elapsedTime = 0f;

        while (elapsedTime < animationDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(popScale, initialScale, elapsedTime / (animationDuration / 2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = initialScale;
    }
}