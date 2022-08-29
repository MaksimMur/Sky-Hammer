using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float _force;
    [SerializeField] private float _radius;
    public void Expolode(bool makeObjectDisable=false)
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                
                rigidbody.AddExplosionForce(_force, transform.position, _radius);
            }
        }
        GameObject go = Instantiate<GameObject>(explosionPrefab);
        go.transform.position = this.transform.position;
        if (!makeObjectDisable) Destroy(this.gameObject);
        else this.gameObject.SetActive(false);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius / 2);
    }
    public float radius { get=> _radius;}
}
