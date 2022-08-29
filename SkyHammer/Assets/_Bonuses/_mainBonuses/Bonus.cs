using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [Header("Set in Inspector:Bonuses options")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _magnite;
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float timeToDestroy=5f;
    [Header("Set Dynamically")]
    internal BonusName name;
    private float _birthTime;
    private Vector3 _rotPerSecond;
    private delegate void BonusDelegate();
    private BonusDelegate BonusEvent;
    private bool _bonusFeature=false;
    private Transform _bonusTransform;
    private float _posibilityToTakeBonusTime = 0.3f;
    void Awake()
    {
        _bonusTransform=GetComponent<Transform>();
        _birthTime = Time.time;
        _bonusTransform.rotation = Quaternion.identity;
        _rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hammer" && _posibilityToTakeBonusTime+_birthTime<Time.time) {

            CallBonusEvent();
        }
    }
    private void CallBonusEvent() {
        TakeBonusEvent(name);
        BonusEvent?.Invoke();
    }
    private void TakeBonusEvent(BonusName name) {
        switch(name){
            case BonusName.a1_SpawnEnemy:
                BonusEvent = SpawnEnemies;
                break;
            case BonusName.a2_MinusHealth:
                BonusEvent = MinusHealth;
                break;
            case BonusName.a3_slowdown:
                BonusEvent = SlowDown;
                break;
            case BonusName.g1_StarRain:
                BonusEvent = StarsRain;
                break;
            case BonusName.g2_destroyCannon:
                BonusEvent = DestroyCannon;
                break;
            case BonusName.g3_x2:
                BonusEvent = ScoreUP;
                break;
            case BonusName.h1_shield:
                BonusEvent = Shield;
                break;
            case BonusName.h2_health:
                BonusEvent =GetHealth;
                break;
            case BonusName.h3_magnite:
                BonusEvent = Magnite;
                break;
        }
    }
    private void MinusHealth() { Hammer.S.Health = Mathf.Max(0, Hammer.S.Health - 20);UIManager.TakeHP(); _bonusFeature = true; }
    private void GetHealth() { Hammer.S.Health = Mathf.Min(100, Hammer.S.Health + 20); UIManager.TakeHP(); _bonusFeature = true; }
    [SerializeField] GameObject CannonDestroyedDebris;
    private void DestroyCannon() {
        List<ListCannons> l = Spawner.S.listCannons;
        for (int i = 0; i < l.Count; i++) {
            if (l[i].Cannon != null) {
                ListCannons go = new ListCannons(null, l[i].PositionsCannon, false);
                GameObject cannon = Instantiate<GameObject>(CannonDestroyedDebris);
                cannon.transform.position = go.PositionsCannon;
                if (go.PositionsCannon.x > 0) cannon.transform.rotation = Quaternion.Euler(-90, 180, 0);
                Destroy(Spawner.S.listCannons[i].Cannon.gameObject);
                l[i] = go;
                _bonusFeature = true;
                return;
            }
        }
        _bonusFeature = true;
    }
    private void Shield() {
        if (Hammer.S.GetComponentInChildren<Shield>() != null) {
            BonusEvent = null;
            return; 
        }
        GameObject go = Instantiate<GameObject>(_shield);
        go.transform.SetParent(Hammer.S.transform);
        go.transform.position = Hammer.S.transform.position;
        _bonusFeature = true;

    }
    public Sprite spriteMagnite;
    private void Magnite()
    {
        Magnite m = Hammer.S.GetComponentInChildren<Magnite>();
        if (m != null) {
            float tl = m.LiveTime + m.BT - Time.time;
            m.LiveTime += m.liveTimeMagnite - tl;
            _bonusFeature = true;
            UIManager.S.AddToLayout(spriteMagnite, m.liveTimeMagnite);
            return; 
        }
        GameObject go = Instantiate<GameObject>(_magnite);
        UIManager.S.AddToLayout(spriteMagnite, _magnite.GetComponent<Magnite>().liveTimeMagnite);
        go.transform.SetParent(Hammer.S.transform);
        go.transform.position = Hammer.S.transform.position;
        _bonusFeature = true;
    }


    private void SpawnEnemies()
    {
        Spawner.S.SpawnCannon();
        _bonusFeature = true;
    }

    //<x2 bonus> (check UIManager Class)
    private float _timeX2 = 5f;
    public Sprite spriteX2;
    private void ScoreUP()
    {
        UIManager.S.TX2= UIManager.S.TX2<Time.time?UIManager.S.TX2=Time.time+_timeX2:Time.time +(  Time.time- UIManager.S.TX2 +_timeX2);
        UIManager.S.AddToLayout(spriteX2, _timeX2);
        _bonusFeature = true;
    }
    // </x2 bonus>

    //<timingBonuses> 
    private bool _enter = false;
    private float _startTime;
    private float _bonusContunueTime;

    

    //<stars bonus> (check Spawner Class)

    private float _bonusChance = 0.67f;
    public Sprite spriteStarsRain;
    private void StarsRain() {
        Spawner.S.BFV = _bonusChance;
        if (!_enter)
        {
            _bonusContunueTime = 2;
            UIManager.S.AddToLayout(spriteStarsRain, _bonusContunueTime);
            _startTime = Time.time;
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        } 
        _enter = true;
        if (_bonusContunueTime + _startTime < Time.time) 
        {
            _bonusFeature = true;
            Spawner.S.BFV = 0;
        }
        Invoke("StarsRain", 0f);

    }
    // </stars bonus>

    //<slowdown> (check Hammer Class)
    private float _timeSlowing=0.05f;
    public Sprite spriteSlowDown;
    private void SlowDown() {
            Hammer.S.SSK = _timeSlowing;
        if (!_enter)
        {
            _bonusContunueTime = 5f;
            UIManager.S.AddToLayout(spriteSlowDown, _bonusContunueTime);
            _startTime = Time.time;
            Hammer.S.SSK = _timeSlowing;
            GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        }
        _enter = true;
        if (_bonusContunueTime + _startTime < Time.time)
        {
            _bonusFeature = true;
            Hammer.S.SSK = 0;
        }
        Invoke("SlowDown", 0f);
    }

    //</slowdown>

    //</timingBonuses>
    void Update()
    {
        if((_birthTime+timeToDestroy)/Time.time<1 &&BonusEvent==null) Destroy(this.gameObject); 
        if (_bonusFeature) Destroy(this.gameObject);
        _bonusTransform.rotation = Quaternion.Euler(_rotPerSecond * Time.time);
    }
}
