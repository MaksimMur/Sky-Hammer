using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPool : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    public PoolMono<Projectile> pool;
    private void Awake()
    {
            pool = new PoolMono<Projectile>(projectile, 20, Camera.main.transform.Find("Projectiles"));
            pool.autoExpand = true;
    }
}
