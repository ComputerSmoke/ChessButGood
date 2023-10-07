using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    int layer;
    Camera mainCamera;
    public GameObject shopsUI;
    public GameObject mercShopUI;
    public GameObject mageShopUI;
    public GameObject gunShopUI;
    public GameObject devilChoiceUI;
    public GameObject atheistChoiceUI;
    public GameObject gameUI;

    public void Init(Camera newCamera) {
        mainCamera = newCamera;
    }
    public void ButtonSetLayer(string layerName) {
        if(Game.placing == null && ActiveChoice() == null)
            SetLayer(layerName);
    }
    public void SetLayer(string layerName) {
        layer = LayerMask.NameToLayer(layerName);
        EnableUI(layer);
        mainCamera.cullingMask = cullingMask(layer);
    }
    private void EnableUI(int layer) {
        gameUI.SetActive(ActiveChoice() == null);
        shopsUI.SetActive(layer == LayerMask.NameToLayer("ShopsMenu"));
        mercShopUI.SetActive(layer == LayerMask.NameToLayer("MercShop"));
        mageShopUI.SetActive(layer == LayerMask.NameToLayer("MageShop"));
        gunShopUI.SetActive(layer == LayerMask.NameToLayer("GunShop"));
        devilChoiceUI.SetActive(layer == LayerMask.NameToLayer("DevilChoice"));
        atheistChoiceUI.SetActive(layer == LayerMask.NameToLayer("AtheistChoice"));
    }
    private static int cullingMask(int layer) {
        int mask = 0;
        for(int i = 0; i < 32; i++) {
            if(i == LayerMask.NameToLayer("Default") || i == layer) 
                mask = mask | (1 << i);
        }
        return mask;
    }
    public Board ActiveBoard() {
        if(layer == LayerMask.NameToLayer("Earth")) 
            return Game.earth;
        if(layer == LayerMask.NameToLayer("Hell")) 
            return Game.hell;
        if(layer == LayerMask.NameToLayer("Heaven")) 
            return Game.heaven;
        return null;
    }
    public string ActiveShop() {
        if(layer == LayerMask.NameToLayer("ShopsMenu"))
            return "ShopsMenu";
        if(layer == LayerMask.NameToLayer("GunShop"))
            return "GunShop";
        if(layer == LayerMask.NameToLayer("MageShop"))
            return "MageShop";
        if(layer == LayerMask.NameToLayer("MercShop"))
            return "MercShop";
        return null;
    }
    public string ActiveChoice() {
        if(layer == LayerMask.NameToLayer("DevilChoice"))
            return "Devil";
        if(layer == LayerMask.NameToLayer("AtheistChoice"))
            return "Atheist";
        return null;
    }
}
