using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T:MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> pool;

    public PoolMono(T prefab, int count) {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }
    public PoolMono(T prefab, int count, Transform container) {
        this.prefab = prefab;
        this.container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count) {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++) {
            this.CreateObject();
        }

    }
    private T CreateObject(bool isActiveByDefault = false) {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElements(out T element) {
        for (int i = 0; i < this.pool.Count; i++) {
            if (!this.pool[i].gameObject.activeInHierarchy) {
                element = this.pool[i];
                this.pool[i].gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElemet() {
        if (this.HasFreeElements(out var element)) {
            return element;
        }
        if (this.autoExpand) {
            return this.CreateObject(true);
        }

        throw new System.Exception($"There is no free elements in pool of typr {typeof(T)}");
    }
}
