using System.Collections;
using UnityEngine;

public class ScreenShakeScript : MonoBehaviour
{
    [SerializeField]
    private bool start = false;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float duration = .5f;

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            StartCoroutine(Shaking());
        }    
    }

    IEnumerator Shaking()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        while(elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        start = false;

        transform.position = startPos;
    }
}
