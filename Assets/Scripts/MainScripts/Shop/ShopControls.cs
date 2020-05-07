using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControls : MonoBehaviour
{
    [SerializeField]
    public GameObject shop; //объект всей панели магазина

    public GameObject shop_mainMenu;
    public GameObject shop_gunsMenu;
    public GameObject shop_cavesMenu;
    public GameObject shop_buildingsMenu;

    public GameObject shop_backButton;

    public static bool shopActive=false;
    public void ShowShop()
    {
        if (!shopActive)
        {
            shop.SetActive(true); // false to hide, true to show
            GoToMainMenu();
            shopActive = true;
            Controls.shopMenuOpened = true;
        }
    }

    public void HideShop()
    {
        shop.SetActive(false); // false to hide, true to show
        shopActive = false;
        Controls.shopMenuOpened = false;
    }

    //Navigation in shop
    void HideAllMenus() {
        shop_gunsMenu.SetActive(false);
        shop_mainMenu.SetActive(false);
        shop_cavesMenu.SetActive(false);
        shop_buildingsMenu.SetActive(false);
    }
    public void GoToGunsList()
    {
        HideAllMenus();
        shop_gunsMenu.SetActive(true);
        shop_backButton.SetActive(true);
    }

    public void GoToCavesList()
    {
        HideAllMenus();
        shop_cavesMenu.SetActive(true);
        shop_backButton.SetActive(true);
    }

    public void GoToMainMenu()
    {
        HideAllMenus();
        shop_mainMenu.SetActive(true);
        shop_backButton.SetActive(false);
    }

    public void GoToBuildingsMenu()
    {
        HideAllMenus();
        shop_buildingsMenu.SetActive(true);
        shop_backButton.SetActive(true);
    }
}
