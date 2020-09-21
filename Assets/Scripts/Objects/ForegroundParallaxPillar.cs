using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundParallaxPillar : MonoBehaviour
{
    [SerializeField] private Camera trackedObject;
    [SerializeField] private float parallaxSpeed = 1f;

    private Vector3 offset;

    private void Start()
    {
        offset = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + -1 * parallaxSpeed * trackedObject.transform.position;
    }
}
