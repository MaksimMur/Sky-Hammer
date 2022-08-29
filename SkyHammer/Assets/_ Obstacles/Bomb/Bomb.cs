using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bomb : MonoBehaviour
{

    [SerializeField] private Text _tPrefab;
    private float _damage = 10;

    private GameObject _anchor;
    private float _birthTime = 0;
    private float _timeToExplosion=2f;
    private Text _tCounter;
    private Explosion exp;
    private void Awake()
    {
        exp = GetComponent<Explosion>();
        _anchor = GameObject.Find("Anchor");
        _birthTime = Time.time;
        _tCounter = Instantiate(_tPrefab);
        _tCounter.transform.SetParent(_anchor.transform, false);
        _tCounter.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_tCounter == null) return;
        _tCounter.transform.position = new Vector3(transform.position.x+1.5f, transform.position.y, -1);
        if (_birthTime + _timeToExplosion > Time.time)
        {
            _tCounter.text = "00:0" + Mathf.Round(_birthTime + _timeToExplosion - Time.time);
        }
        else {
            if (Vector3.Distance(this.transform.position, Hammer.HAMMER_POS) < GetComponent<Explosion>().radius / 2 ) {
                if (Hammer.S.GetComponent<Shield>() != null) {
                    Destroy(Hammer.S.GetComponent<Shield>().gameObject);
                    Destroy(_tCounter.gameObject);
                    exp.Expolode();
                    return;
                }
                Hammer.S.Health -= _damage;
                UIManager.TakeHP();
            }
            Destroy(_tCounter.gameObject);
            exp.Expolode(); 
        }
    }
   
    public float TimeExp {

        get => _timeToExplosion;
        set => _timeToExplosion = value;
    }
}
