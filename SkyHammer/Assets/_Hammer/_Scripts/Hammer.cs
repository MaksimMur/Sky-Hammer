using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hammer : MonoBehaviour
{
    public static Hammer S;
    public static Vector3 HAMMER_POS = Vector3.zero;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health;
    [Header("Set Dynamically")]
    internal static float SPEED = 2;
    private Transform _hammerTransform;
    private Vector2 BoarderXY= Vector2.zero;
    void Awake() {
        S = this;
        _health = _maxHealth;
        _hammerTransform = GetComponent<Transform>();
        BoarderXY = new Vector2(Camera.main.orthographicSize * ((float)Screen.width / Screen.height), Camera.main.orthographicSize);
    }
    public float easing = 0.03f;


    void Update()
    {

        GetHammerPosition();
    }
    private void GetHammerPosition() {
        Vector3 pos = Input.mousePosition;

        pos.z = Camera.main.orthographicSize;
        pos = Camera.main.ScreenToWorldPoint(pos);

        _hammerTransform.position = Vector3.Lerp(_hammerTransform.position, pos, easing - _slowSpeedKof);

        SPEED = Vector3.Distance(pos, transform.position);

        Vector3 difference = pos - _hammerTransform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _hammerTransform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        HAMMER_POS = _hammerTransform.position;
    }

    private float _slowSpeedKof = 0;
    public float SSK{
        get => _slowSpeedKof;
        set => _slowSpeedKof = value;
    }
    public float Health
    {
        get => _health;
        set => _health = value;
    }
    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    
}
