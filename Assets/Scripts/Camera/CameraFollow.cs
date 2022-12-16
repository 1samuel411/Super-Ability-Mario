using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;
    [SerializeField] private float offset;
    [SerializeField] private float followSpeed;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetTransform.transform.position.x, offset, transform.position.z), followSpeed * Time.deltaTime);
    }
}
