using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager S;
    private void Awake()
    {
        S = this;
    }
    public static Texture2D TextureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
    public GameObject panel;
    public void EndGame() {
        panel.SetActive(true);
        GetScoreAndStars();
        List<ListCannons> l = Spawner.S.listCannons;
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i].Cannon != null)
            {
                ListCannons go = new ListCannons(null, l[i].PositionsCannon, false);
                Destroy(Spawner.S.listCannons[i].Cannon.gameObject);
                Spawner.S.listCannons[i] = go;
            }
        }
        Destroy(Camera.main.GetComponent<Spawner>());
        Destroy(Hammer.S.gameObject);
    }
    void GetScoreAndStars() {
        if (PlayerPrefs.HasKey("MaxScore")) {
            if (PlayerPrefs.GetFloat("MaxScore") < UIManager.SCORE) PlayerPrefs.SetFloat("MaxScore", UIManager.SCORE);
        }
        else PlayerPrefs.SetFloat("MaxScore", UIManager.SCORE);
        if (PlayerPrefs.HasKey("StarCounter"))
        {
            PlayerPrefs.SetInt("StarCounter", Mathf.Min(9999999, PlayerPrefs.GetInt("StarCounter") + UIManager.COUNTER_STARS));
        }
        else PlayerPrefs.SetInt("StarCounter", UIManager.COUNTER_STARS);
        GameObject go = GameObject.Find("YourScore");
        go.GetComponent<Text>().text = "Your Score:" + UIManager.SCORE.ToString();
        go = GameObject.Find("MaxScore");
        go.GetComponent<Text>().text = "Max Score:" + PlayerPrefs.GetFloat("MaxScore");
    }
   public  void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
   }
}
