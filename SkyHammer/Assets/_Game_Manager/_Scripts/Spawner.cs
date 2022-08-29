using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField] public struct ListCannons{
    public GameObject Cannon { get; set; }
    public Vector3 PositionsCannon { get; set; }
    public bool OccupatedPosition;


    public ListCannons(GameObject p, Vector3 vector3, bool v) : this()
    {
        this.Cannon = p;
        this.PositionsCannon = vector3;
        this.OccupatedPosition = v;
    }
}
public class Spawner : MonoBehaviour
{
    [Header("Set in Inpsector: SpawnOptions")]
    public static Spawner S;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private float _delayBeetweenDropObstacles = 1.5f;
    [SerializeField] private GameObject _star;
    [SerializeField] private float _chanceToDropStar = 0.03f;
    [SerializeField] private float _delayBetwenSpawnCannons;
    [SerializeField] private GameObject cannonPrefab;
    private StarsPool _starPool;
    public List<ListCannons> listCannons;
    [SerializeField] private int maxCannonPlaces = 12;
    private float _boarderX, _boarderY;
    private void Awake()
    {
        S = this;
        _starPool = Camera.main.GetComponentInChildren<StarsPool>();
        _boarderX = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
        _boarderY = Camera.main.orthographicSize;
        //Инициализация списка пушек
        listCannons = new List<ListCannons>();
        float deltaY = 3, startY = 11.5f, posX = _boarderX + 1;

        int count = 0;
        while (count < maxCannonPlaces) {
            if (count == 6) {
                deltaY *= -1;
                posX *= -1;
                listCannons.Add(new ListCannons(null, new Vector3(posX, startY, 0), false));
            }
            else listCannons.Add(new ListCannons(null, new Vector3(posX, startY -= deltaY, 0), false));
            count++;
        }
    }
    void Start()
    {
        Invoke("SpawnCannon", _delayBetwenSpawnCannons);
        Invoke("SpawnObstacles", 2f);
    }

    // <cannon>
    public void SpawnCannon() {
        if (!CheckFreePlaces()) return;
        int place = TakeFreePlace();
        CreateCannon(place);
        Invoke("SpawnCannon", _delayBetwenSpawnCannons);
    }
    private void CreateCannon(int place) {
        GameObject go = Instantiate<GameObject>(cannonPrefab);
        go.transform.position = listCannons[place].PositionsCannon;
        ListCannons ms = new ListCannons(go, listCannons[place].PositionsCannon, true);
        listCannons[place] = ms;
    }
    private bool CheckFreePlaces() {
        for (int i = 0; i < maxCannonPlaces; i++) {
            if (listCannons[i].Cannon == null) {
                return true;
            }

        }
        return false;
    }
    private int TakeFreePlace() {

        List<int> freePlaces = new List<int>();
        for (int i = 0; i < maxCannonPlaces; i++) {
            if (!listCannons[i].OccupatedPosition) {
                freePlaces.Add(i);
            }
        }
        return freePlaces[Random.Range(0, freePlaces.Count)];

    }


    // </cannon>
    private void SpawnObstacles() {
        int index = Random.Range(0, _obstacles.Length);
        GameObject go = Instantiate<GameObject>(_obstacles[index]);

        float deltaX = Random.Range(-_boarderX + 1.5f, _boarderX - 1.5f);
        go.transform.position = new Vector3(deltaX, _boarderY + 2, z: 0);
        Invoke("SpawnObstacles", _delayBeetweenDropObstacles);
    }
    private void SpawnStar() {
        Star go = _starPool._poolStars.GetFreeElemet();
        float boarder = Camera.main.orthographicSize;
        float deltaX = Random.Range(-boarder, boarder);
        go.transform.position = new Vector3(deltaX, boarder + 2, z: 0);
    }


    //bonuses and antibonueses Fields
    private float _bonusStarValue = 0;
    
    public float BFV{
        get { return _bonusStarValue; }
        set { _bonusStarValue = value; }
    }
    void FixedUpdate()
    {
        if (Random.value < _chanceToDropStar+_bonusStarValue) {
            SpawnStar();
        }
    }
}
