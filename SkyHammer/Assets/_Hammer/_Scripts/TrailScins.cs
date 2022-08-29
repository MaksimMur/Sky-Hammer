using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScins :MonoBehaviour
{
    private static int index;
    private GameObject Anchor;
    [SerializeField]private Material materialPrefab;
    [SerializeField]private GameObject particlePrefab;
    [SerializeField]private Sprite[] sprites;
    private static TrailScins S;
    private void Awake()
    {
        S = this;
        Anchor = GameObject.Find("Trail");
        index = PlayerPrefs.GetInt("Scin");
        GameObject go = Instantiate<GameObject>(particlePrefab);
        go.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = materialPrefab;
        go.transform.SetParent(S.Anchor.transform,false);
        go.transform.position = S.Anchor.transform.position;
        ChangeScin();
    }
    public void ChangeScin() {
        index = PlayerPrefs.GetInt("Scin");
        ParticleSystem s = S.GetComponentInChildren<ParticleSystem>();
        S.materialPrefab.mainTexture = GameManager.TextureFromSprite(S.sprites[index]);
        s.GetComponent<Renderer>().material = S.materialPrefab;
    }
    public Sprite[] GetScins() {
        return sprites;
    }
}
