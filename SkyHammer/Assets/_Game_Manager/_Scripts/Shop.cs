using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Shop : MonoBehaviour
{
    [SerializeField] private int[] _scinsCost;
    [SerializeField] private Text _tTextCounterStars;
    private int _indexer=0;
    private int _indexerChoosen = 0;
    private int _amountStars=0;
    [SerializeField] private Image[] _scinsImages;
    private List<string> _boughtScins;
    [SerializeField] private Button _button;

    public void Awake()
    {
        _amountStars = PlayerPrefs.GetInt("StarCounter");
        _boughtScins = new List<string>();
        if (!PlayerPrefs.HasKey("Scin")) PlayerPrefs.SetInt("Scin", 0);
        if (!PlayerPrefs.HasKey("Scins")) PlayerPrefs.SetString("Scins", "0");
        else {
            _boughtScins = new List<string>(PlayerPrefs.GetString("Scins").Split(' '));
        }
        _indexer =_indexerChoosen = PlayerPrefs.GetInt("Scin");
        _scinsImages[_indexerChoosen].color = new Color(1,1,0.5f,0.5f);
        _tTextCounterStars.text = _amountStars.ToString();
        PressTheScin(_indexerChoosen);
    }

    public void PressTheScin(int scinIndex) {
        _button.onClick.RemoveAllListeners();
        _indexer = scinIndex;
        GetEvtForButton();
    }
    public void GetEvtForButton() {
        if (_indexer == _indexerChoosen) {
            _button.onClick.RemoveAllListeners();
            _button.GetComponentInChildren<Text>().text = "Choosen";
            return;
        }
        if (!_boughtScins.Contains((_indexer).ToString()))
        {
            _button.GetComponentInChildren<Text>().text = _scinsCost[_indexer].ToString();
            _button.onClick.AddListener(delegate {
                BuyScin(_indexer);
                ChooseScin(_indexer);
               _button.onClick.RemoveAllListeners();
            });
            return;
        }
       _button.GetComponentInChildren<Text>().text = "Choose";
       _button.onClick.AddListener(delegate { 
       
           ChooseScin(_indexer);
           _button.onClick.RemoveAllListeners();
       });
        
    }
    public void BuyScin(int scinIndex) {
        if (_amountStars >= _scinsCost[scinIndex])
        {
            print(_scinsCost[scinIndex]);
            _amountStars -= _scinsCost[scinIndex];
            _tTextCounterStars.text = _amountStars.ToString();
            PlayerPrefs.SetInt("StarCounter", _amountStars);
            _boughtScins.Add(scinIndex.ToString());
            PlayerPrefs.SetString("Scins", string.Concat(_boughtScins.ToArray()));
            ChooseScin(scinIndex);
        }    
    }
    public void ChooseScin(int scinIndex)
    {
        if (_boughtScins.Contains((_indexer).ToString())) {
            _scinsImages[_indexerChoosen].color = new Color(0, 0, 0, 0);
            _indexerChoosen = scinIndex;
            _scinsImages[_indexerChoosen].color = new Color(1, 1, 0.5f, 0.5f);
            _button.GetComponentInChildren<Text>().text = "Choosen";
            PlayerPrefs.SetInt("Scin", scinIndex);
            SetTrailForHammer();
        }
    }
    private void SetTrailForHammer() {
        GameObject.Find("FantasyHammer").GetComponent<TrailScins>().ChangeScin();
    } 

}
