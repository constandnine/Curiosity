using System.Collections;
using UnityEngine;

public class SmoothCameraSwitch : MonoBehaviour
{
    public static SmoothCameraSwitch instance;

    [SerializeField] float speed;
    [SerializeField] Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SmoothCameraRotation(Transform target)
    {
        StopCoroutine(SmoothMove(null));
        StartCoroutine(SmoothMove(target));
        Debug.Log($"moving camera to {target}", this);
    }

    private IEnumerator SmoothMove(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.0005f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            transform.LookAt(target.position);
            yield return null;
            Debug.Log("moving", this);
        }
    }
}
