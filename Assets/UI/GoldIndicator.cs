using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldIndicator : MonoBehaviour
{
    public int color;
    public GameObject coinPrefab;
    private Stack<GameObject> coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        int target = TargetMoney();
        while(coins.Count > target && coins.Count > 0) {
            GameObject coin = coins.Pop();
            Object.Destroy(coin);
        }
        while(coins.Count < target) {
            GameObject coin = Instantiate(coinPrefab, new Vector3(((float)coins.Count) / 5f + gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, gameObject.transform);
            coin.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
            coins.Push(coin);
        }
    }
    private int TargetMoney() {
        if(color == 0)
            return Game.whiteGold;
        return Game.blackGold;
    }
}
