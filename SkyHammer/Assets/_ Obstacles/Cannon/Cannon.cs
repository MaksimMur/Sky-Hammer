using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Set in Inspector: Cannon Options")]
    [SerializeField] private float _delayBeetweenShots = 2f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform launchPos;
    [SerializeField] private float _projectileSpeed;
    [Header("Set Dynamically")]
    private float _lastShotTime = 0;
    private float _rotY=90;
    void Update()
    {
        LookAthammer();
        TempFire();

    }
    private void LookAthammer() {
        Vector3 difference = Hammer.HAMMER_POS - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-rotZ, _rotY, 0f);
    }
    private void TempFire() {
        if (Time.time - _lastShotTime < _delayBeetweenShots) return;
        GameObject go = Instantiate<GameObject>(projectile);
        go.transform.position = launchPos.position;
        Rigidbody rigid = go.GetComponent<Rigidbody>();
        rigid.velocity = -(transform.position - go.transform.position) * _projectileSpeed;
        _lastShotTime = Time.time;
    }
    
}
