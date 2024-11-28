using UnityEngine;

public class SunControls : MonoBehaviour
{
    [SerializeField] private float minXAngle = -90f;
    [SerializeField] private float maxXAngle = 90f;
    [SerializeField] private float minYAngle = 123f;
    [SerializeField] private float maxYAngle = 235;
    [SerializeField, Range(0, 10f)] private float rotationSpeed = 1.0f;

    private void Awake()
    {
        
    }

    void Update()
    {
        var minRotation = Quaternion.Euler(minXAngle, minYAngle, transform.rotation.eulerAngles.z);
        var maxRotation = Quaternion.Euler(maxXAngle, maxYAngle, transform.rotation.eulerAngles.z);
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, minRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, maxRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
