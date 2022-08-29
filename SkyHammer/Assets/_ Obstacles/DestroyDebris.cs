using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDebris : MonoBehaviour
{
    [SerializeField] private int _scorePoints = 5;
    [SerializeField] private float _speedForDestroyed = 2;
    [SerializeField] private GameObject _prefabDestroyed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            if (Hammer.SPEED > _speedForDestroyed)
            {

                UIManager.S.TakeScore(_scorePoints);
                GameObject go = Instantiate<GameObject>(_prefabDestroyed);
                DestroyRootAfterLoosingChildren got = GetComponentInParent<DestroyRootAfterLoosingChildren>();
                if (got != null) { 
                    got.listObstacles.Remove(this.gameObject.transform);
                    if (got.listObstacles.Count <= 1) Destroy(got.gameObject);
                }
                go.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
    }
}
