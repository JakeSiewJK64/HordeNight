using System.Collections;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fanblades, player;


    [SerializeField]
    private AudioSource audioSource;

    private float maxDistance = 60.0f;
    private float fanSpeed = 500;
    private float minVolume = 0.0f;
    private float maxVolume = 1.0f;
    private float speed = 10.0f;

    IEnumerator Depart()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.timeScale == 1f)
        {
            fanblades.transform.Rotate(Vector3.up * fanSpeed * Time.deltaTime, Space.World);
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            float distance = Vector3.Distance(transform.position, player.transform.position);
            audioSource.volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        }
    }

    private void Start()
    {
        StartCoroutine(Depart());
    }
}
