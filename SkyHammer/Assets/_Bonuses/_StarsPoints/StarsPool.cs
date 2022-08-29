using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsPool : MonoBehaviour
{
    public Star starPrefab;
    public PoolMono<Star> _poolStars;
    private void Awake()
    {
        _poolStars = new PoolMono<Star>(starPrefab, 20, this.transform);
        _poolStars.autoExpand = true;
    }
}
