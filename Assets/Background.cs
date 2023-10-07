using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject earth;
    public GameObject heaven;
    public GameObject hell;
    public GameObject mageShop;
    public GameObject gunShop;
    public GameObject mercShop;
    public GameObject shopsMenu;
    public GameObject mainMenu;
    public GameObject reddit;
    private GameObject activeBackground;
    public LayerController layerController;
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        GameObject background = NextBackground();
        if(activeBackground == background)
            return;
        if(activeBackground != null)
            activeBackground.SetActive(false);
        background.SetActive(true);
        background.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, mainCamera.gameObject.transform.position.y, 0);
        background.transform.localScale = new Vector3(200, 200, 1);
        activeBackground = background;
    }
    private GameObject NextBackground() {
        if(Game.earth == null)
            return mainMenu;
        if(layerController.ActiveBoard() == Game.earth)
            return earth;
        if(layerController.ActiveBoard() == Game.hell) {
            if(Game.hell.destroyed)
                return reddit;
            return hell;
        }
        if(layerController.ActiveBoard() == Game.heaven) {
            if(Game.heaven.destroyed)
                return reddit;
            return heaven;
        }
        if(layerController.ActiveShop() == "ShopsMenu")
            return shopsMenu;
        if(layerController.ActiveShop() == "GunShop")
            return gunShop;
        if(layerController.ActiveShop() == "MageShop")
            return mageShop;
        if(layerController.ActiveShop() == "MercShop")
            return mercShop;
        if(layerController.ActiveChoice() == "Devil")
            return hell;
        if(layerController.ActiveChoice() == "Atheist")
            return reddit;
        
        return mainMenu;
    }
}
