using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyedAfterFalling : MonoBehaviour
{
    [SerializeField] private float DestroyedDistanse = -10;
    void LateUpdate()
    {
        if (transform.position.y < DestroyedDistanse) {
            DestroyRootAfterLoosingChildren go = GetComponentInParent<DestroyRootAfterLoosingChildren>();
            if (go != null) {
                go.listObstacles.Remove(this.gameObject.transform);
                if (go.listObstacles.Count <= 1) Destroy(go.gameObject);
            }
            Destroy(this.gameObject); 
        }
    }

}
