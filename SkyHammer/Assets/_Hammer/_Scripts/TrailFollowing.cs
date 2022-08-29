using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailFollowing : MonoBehaviour
{
    Transform transformTrail;
    Transform transformPoi;
    private void Awake()
    {
        transformTrail = this.transform;
        transformPoi = GameObject.Find("Trail").GetComponent<Transform>();
    }
    float easing = 0.05f;
    void Update()
    {
        try
        {
            transformTrail.position = Vector3.Lerp(transformPoi.position, transformTrail.position, easing);
        }
        catch (Exception) {
            return;
        }
    }
}
