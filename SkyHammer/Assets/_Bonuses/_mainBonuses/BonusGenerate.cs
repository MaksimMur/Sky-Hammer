using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusName { 
    a1_SpawnEnemy,
    a2_MinusHealth,
    a3_slowdown,
    g1_StarRain,
    g2_destroyCannon,
    g3_x2,
    h1_shield,
    h2_health,
    h3_magnite
}


public class BonusGenerate : MonoBehaviour
{
    public static BonusGenerate S;
    public Material prefabMaterial;
    public GameObject bonusPrefab;
    public BonusName[] bonusChanceSpawn = new BonusName[] {
        BonusName.g1_StarRain
    };
    [SerializeField] private Sprite[] _Bonuses;
    private void Awake()
    {
        S = this;
    }
  

    public void TakePrefab(BonusName name,Vector3 pos) {
        GameObject go = Instantiate<GameObject>(bonusPrefab);
        go.transform.position = pos;
        go.GetComponent<Bonus>().name = name;
        Material mat = Instantiate<Material>(prefabMaterial);
        mat.mainTexture=GameManager.TextureFromSprite(_Bonuses[(int)name]);
        go.GetComponent<Renderer>().material = mat;
    }
    public string BonusEnumToString(BonusName name)
    {
        return name.ToString();
    }

}
