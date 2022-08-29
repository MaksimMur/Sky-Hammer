using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedObstacles : MonoBehaviour
{
    [Header("Set in Inscpetor: Obstacles option")]
    [SerializeField] private GameObject _prefabDebris;
    [SerializeField] private float _speedForDestroyed = 2f;
    [SerializeField] private int _scorePoints=20;
    [Header("Exp Options")]
    [SerializeField] private float _chanceToSpawnExp=0.2f;
    [SerializeField] private GameObject expPrefab;

    [Header("Bonus Options")]
    [SerializeField] private float _chanceToSpawnBonus=0.3f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            // print(Hammer.SPEED + "|" + Hammer.LEVEL_OF_DAMAGE);
            if (Hammer.SPEED > _speedForDestroyed )
            {
                UIManager.S.TakeScore(_scorePoints);
                DestroyedObstacle();
                SpawnSurprise(this.tag);
            }
        }
    }
    private void SpawnSurprise(string tag) {
        if (Random.value < _chanceToSpawnBonus){
            int index = Random.Range(0,BonusGenerate.S.bonusChanceSpawn.Length);
            BonusGenerate.S.TakePrefab(BonusGenerate.S.bonusChanceSpawn[index], transform.position); 
            return; 
        }
        if (Random.value > _chanceToSpawnExp) return;
        switch (tag) {
            case "Box":
            case "Safe":
            case "Chest":
                GameObject go = Instantiate<GameObject>(expPrefab);
                go.transform.position = transform.position;
                float diference = this.transform.position.y + Camera.main.orthographicSize;
                go.GetComponent<Bomb>().TimeExp =Mathf.Abs( diference / 20);
                return;
        }
       
    }


    private void DestroyedObstacle() {
       
        GameObject go = Instantiate<GameObject>(_prefabDebris);
        go.transform.position = this.transform.position;
        Destroy(this.gameObject);
   
    }
}
