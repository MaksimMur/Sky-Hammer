using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager S;
    public Image prefabHealth;
    public float deltaX = -50;
    private GameObject _healthAnchor;
    private float _amountOfHealth = 20;
    internal static int COUNTER_STARS = 0;
    internal static int SCORE = 0;
    private Text _tScore, _tStars, _tScoreCounter;
    private List<Image> listHP;
    private void Awake()
    {
        S = this;
        COUNTER_STARS = 0;
        SCORE = 0;
        listHP = new List<Image>();
        _healthAnchor = GameObject.Find("HealthAnchor");
        GameObject go = GameObject.Find("Score");
        if (go != null) _tScore = go.GetComponent<Text>();
        go = GameObject.Find("StarsCounter");
        if (go != null) _tStars = go.GetComponent<Text>();
        go = GameObject.Find("ScoreCounter");
        if (go != null) _tScoreCounter = go.GetComponent<Text>();

    }
    void Start()
    {

        // print("Health" + Hammer.S.Health / _amountOfHealth);
        for (int i = 0; i < (Hammer.S.Health / _amountOfHealth); i++)
        {
            Image im = Instantiate<Image>(prefabHealth);
            im.transform.SetParent(_healthAnchor.transform, false);
            im.transform.localPosition = new Vector2(deltaX * i, _healthAnchor.transform.position.y);
            Image backIm = Instantiate<Image>(prefabHealth);
            backIm.transform.SetParent(_healthAnchor.transform, false);
            backIm.transform.localPosition = im.transform.localPosition;
            backIm.color = new Color(1, 1, 1, 0.5f);
            listHP.Add(im);
        }
    }

    // Update is called once per frame

    void Update()
    {

        _tStars.text = COUNTER_STARS.ToString();

    }
    public static void TakeHP() {
        float diffHp = Hammer.S.MaxHealth - Hammer.S.Health;

        for (int i = S.listHP.Count - 1; i > -1; i--) {
            if (diffHp <= 0) {
                S.listHP[i].fillAmount = 1;
            }
            if (diffHp > S._amountOfHealth)
            {
                S.listHP[i].fillAmount = 0;
            }
            else {
                S.listHP[i].fillAmount = 1 - diffHp * ((float)1 / S._amountOfHealth);
            }
            diffHp -= 20;
        }
        if (Hammer.S.Health <= 0)
        {
            GameManager.S.EndGame();
            return;
        }
    }

    [SerializeField] private Text textFly;
    private float _timeX2 = 0;
    public float TX2 {
        get => _timeX2;
        set => _timeX2 = value;
    }
    [SerializeField] private Transform _bonusLayout;
    [SerializeField] private Image _bonusImgForLayout;
    public void AddToLayout(Sprite sprite,float time) {
        Image[] masLayout = _bonusLayout.GetComponentsInChildren<Image>();
        for (int i = 0; i < masLayout.Length; i++) {
            if (masLayout[i].sprite == sprite) {
                Destroy(masLayout[i].gameObject);
                break;
            }
        }
        Image go = Instantiate<Image>(_bonusImgForLayout);
        go.GetComponent<BonusEffectTime>().effectTime=time;
        go.transform.SetParent(_bonusLayout,false);
        go.sprite = sprite;
    }
    public void TakeScore(int score) {
        if (_timeX2 > Time.time) {score *= 2; }
       // print(score);
        SCORE += score;
        _tScoreCounter.text = SCORE.ToString();
        GameObject go = GameObject.Find("ScoreCounter");
        Text textT = Instantiate(textFly);
        textT.text = "+" + score.ToString();
    }
}
