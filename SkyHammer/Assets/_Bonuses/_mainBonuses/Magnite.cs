using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnite : MonoBehaviour
{
    [Header("Set in Inpsector: Magnite Options")]
    public static bool magniteForce=false;
    public float liveTimeMagnite = 10;
    [SerializeField] private Material _prefabMaterial;
    [SerializeField] private Sprite _magniteSprite;

    [Header("Set Dynamically")]
    private float _timeToDestroy;
    private float _birthTime;


    private void Awake()
    {
        _timeToDestroy = liveTimeMagnite;
        magniteForce = true;
        _birthTime = Time.time;
        Material mat = Instantiate<Material>(_prefabMaterial);
        mat.mainTexture = GameManager.TextureFromSprite(_magniteSprite);
        GetComponent<Renderer>().material = mat;
    }
    public void Update()
    {
        if ((_birthTime + LiveTime) / Time.time < 1) {
            magniteForce = false;
            Destroy(this.gameObject); 
        }
    }
    public float BT {
        get => _birthTime;
    }
    public float LiveTime {
        get { return _timeToDestroy; }
        set { _timeToDestroy = value; }
    }
}
