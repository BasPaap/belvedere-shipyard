using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bob : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 destinationPosition;

    Quaternion startRotation;
    Quaternion destinationRotation;

    [SerializeField] private Vector3 offset = new(0, 1.5f);
    [SerializeField] private Quaternion maxRotation;
    [SerializeField, Range(0,10f), FormerlySerializedAs("speed")] private float translationSpeed = 2f;
    [SerializeField, Range(0, 10f)] private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = Quaternion.Euler(transform.rotation.eulerAngles - maxRotation.eulerAngles / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Translate();
        Rotate();
    }

    private void Translate()
    {
        var endPosition = startPosition + offset;

        if (Vector3.Distance(transform.position, startPosition) <= 0.001f)
        {
            destinationPosition = endPosition;
        }
        else if (Vector3.Distance(transform.position, endPosition) <= 0.001f)
        {
            destinationPosition = startPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, translationSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        var endRotation = Quaternion.Euler(startRotation.eulerAngles + maxRotation.eulerAngles / 2f);

        if (Vector3.Distance(transform.rotation.eulerAngles, startRotation.eulerAngles) <= 0.001f)
        {
            destinationRotation = endRotation;
        }
        else if (Vector3.Distance(transform.rotation.eulerAngles, endRotation.eulerAngles) <= 0.001f)
        {
            destinationRotation = startRotation;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, destinationRotation, rotationSpeed * Time.deltaTime);
    }
}