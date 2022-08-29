using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public float radius = 1f;
    private Transform _transform;
    private float _camWidth;
    private float _camHeight;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _camHeight = Camera.main.orthographicSize;
        _camWidth = Camera.main.aspect * _camHeight;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = _transform.position;
        if (pos.x > _camWidth - radius) {
            pos.x = _camWidth - radius;
        }
        if (pos.x < -_camWidth + radius)
        {
            pos.x = -_camWidth + radius;
        }
        if (pos.y > _camHeight - radius)
        {
            pos.y = _camHeight - radius;
        }
        if (pos.y < -_camHeight + radius)
        {
            pos.y = -_camHeight + radius;
        }
        _transform.position = pos;
    }
}
