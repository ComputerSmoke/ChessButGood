using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    public GameObject prefab;
    protected Stack<GameObject> coins;
    public float spacingX;
    public int rowSize;
    public float spacingY;
    // Start is called before the first frame update
    void Start()
    {
        coins = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    protected virtual void OnUpdate() {
        int target = TargetAmount();
        while(coins.Count > target && coins.Count > 0) {
            GameObject coin = coins.Pop();
            Object.Destroy(coin);
        }
        while(coins.Count < target) {
            GameObject coin = Instantiate(
                prefab, 
                transform.rotation * new Vector3(
                    ((float)(coins.Count%rowSize)) * spacingX, 
                    -((float)(coins.Count/rowSize)) * spacingY,
                    0
                ) + transform.position, 
                Quaternion.identity, 
                gameObject.transform
            );
            coin.GetComponent<SpriteRenderer>().sortingLayerName = "Indicators";
            coin.GetComponent<SpriteRenderer>().sortingOrder = coins.Count;
            coin.layer = gameObject.layer;
            coins.Push(coin);
        }
    }
    protected abstract int TargetAmount();
}
