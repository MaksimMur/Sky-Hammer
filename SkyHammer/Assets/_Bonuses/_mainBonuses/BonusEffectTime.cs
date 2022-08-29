using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusEffectTime : MonoBehaviour
{
    Image img;
    private float _effectTime=0;
    private float _birthTime;
    private void Awake()
    {
        img = GetComponent<Image>();
        _birthTime = Time.time;
    }
    public float effectTime { 
    set=> _effectTime = value;
    }
    
    void Update() {
        img.fillAmount = Mathf.Max(0, (_birthTime + _effectTime - Time.time) / _effectTime);
        if (img.fillAmount == 0) Destroy(this.gameObject);
    }
}
