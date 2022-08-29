using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage=5;
    private Explosion exp;
    void Awake()
    {
        exp=GetComponent<Explosion>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer") {
            Shield s = Hammer.S.GetComponentInChildren<Shield>();
            if (s != null)
            {
                s.gameObject.SetActive(false);
            }
            else
            {
                Hammer.S.Health -= _damage;
                UIManager.TakeHP();
            }
        }
        exp.Expolode(true);
        this.gameObject.SetActive(false);
    }
}
