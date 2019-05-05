using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    public float RotationSpeed = 150f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
    }
}
