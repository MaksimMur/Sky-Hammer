using UnityEngine;
using UnityEngine.UI;
public class FlyText : MonoBehaviour
{
    private GameObject _anchor;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _landingPoint = Vector3.one;
    private float _diffPivot = 0.5f;
    Transform transformText;
    private void Awake()
    {
        transformText = GetComponent<Transform>();
    }
    private int TakeSigns(int number)
    {
        if (number == 0) return 1;
        int count = 0;
        while (number > 0)
        {
            count++;
            number /= 10;
        }
        return count;
    }
    private void Start()
    {
        int kof = TakeSigns(UIManager.SCORE);
        //print(kof);
        _anchor = GameObject.Find("Anchor");
        transform.SetParent(_anchor.transform, false);
        GameObject go = GameObject.Find("ScoreCounter");
        Vector3 pos = go.transform.position;
        pos = new Vector3(pos.x + kof * _diffPivot, pos.y, pos.z);
        _landingPoint = new Vector3(pos.x + kof * _diffPivot, pos.y + 1.5f, pos.z);
        this.transform.position = pos;
    }


    // Update is called once per frame
    
    void FixedUpdate()
    {
        transformText.position = Vector3.Lerp(this.transform.position, _landingPoint, 0.03f);
    }
}
