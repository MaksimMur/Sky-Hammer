using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Material _prefabMaterial;
    [SerializeField] private Sprite _shieldSprite;
    private void Awake()
    {
        Material mat = Instantiate<Material>(_prefabMaterial);
        mat.mainTexture = GameManager.TextureFromSprite(_shieldSprite);
        GetComponent<Renderer>().material = mat;
    }
}
