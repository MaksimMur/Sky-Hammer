using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParalax : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private float dynamicallbackGroundKof=30f;
    // Update is called once per frame
    void Update()
    {
        Vector3 hammerPos = Hammer.HAMMER_POS;

        Vector3 pos = new Vector3(hammerPos.x, hammerPos.y, 0);
        backGround.transform.position = -pos/dynamicallbackGroundKof;
    }
}
