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
        StartCoroutine(SmoothMove(target));
        Debug.Log($"moving camera to {target}", this);
    }

    private IEnumerator SmoothMove(Transform target)
    {
        while (Vector3.Distance(mainCamera.transform.position, target.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(mainCamera.transform.position, target.position, Time.deltaTime * speed);
            yield return null;
            Debug.Log("moving", this);
        }
    }
}
