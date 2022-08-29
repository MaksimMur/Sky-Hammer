using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCreate : MonoBehaviour
{
    [Header("Set in Inpsetor")]
    [SerializeField] private float _timeToExp=2f;
    private float _birthTime = 0;
    private void Awake()
    {
        _birthTime = Time.time;
    }
    void Update()
    {
        if (_birthTime + _timeToExp < Time.time) {
            Destroy(this.gameObject);
        }
    }
}
