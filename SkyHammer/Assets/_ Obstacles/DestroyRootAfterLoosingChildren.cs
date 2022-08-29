using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRootAfterLoosingChildren : MonoBehaviour
{
    private List<Transform> childrenObstacles;

    private void Awake()
    {
        childrenObstacles = new List<Transform>(GetComponentsInChildren<Transform>());
    }
    public List<Transform> listObstacles{
        get { return childrenObstacles; }
        set { childrenObstacles = value; }
    }
}
