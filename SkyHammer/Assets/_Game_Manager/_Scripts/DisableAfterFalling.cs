using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterFalling : MonoBehaviour
{
    [SerializeField] private float DestroyedDistanse = -10;
    void LateUpdate()
    {
        if (transform.position.y < DestroyedDistanse)
        {
            DestroyRootAfterLoosingChildren go = GetComponentInParent<DestroyRootAfterLoosingChildren>();
            if (go != null)
            {
                go.listObstacles.Remove(this.gameObject.transform);
                if (go.listObstacles.Count <= 1) Destroy(go.gameObject);
            }
            this.gameObject.SetActive(false);
        }
    }
}
