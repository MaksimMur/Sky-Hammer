using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Set Dynamically")]
    private float _easing;
    private float _depthY;
    private Vector3 _landingPoint;
    private Transform _transformStar;
    private void Awake()
    {
        _transformStar = GetComponent<Transform>();
    }
    private void OnEnable()
    {
        _easing = Random.Range(0.001f, 0.01f);
        _depthY = Random.Range(15, 30);
        float boarder = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
        float deltaX = Random.Range(-boarder, boarder);
        _landingPoint = new Vector3(deltaX, boarder - _depthY, 0);
        transform.GetComponent<TrailRenderer>().enabled = true;
    }
    private void OnDisable()
    {
        transform.GetComponent<TrailRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer") {
            UIManager.COUNTER_STARS++;
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
       MoveStar();
    }
    private Vector3 _posHammer;
    void MoveStar()
    {
        if (!Magnite.magniteForce)
        {
            _transformStar.position = Vector3.Lerp(_transformStar.position, _landingPoint, _easing);
            return;
        }
        
        
        _posHammer = Hammer.HAMMER_POS;
        if (_posHammer.y < transform.position.y)
        {
            _transformStar.position = Vector3.Lerp(_transformStar.position, _posHammer, 0.03f);
            return;
        }
        _transformStar.position = Vector3.Lerp(_posHammer, _transformStar.position, 0.03f);
        return;
        
    }
}
