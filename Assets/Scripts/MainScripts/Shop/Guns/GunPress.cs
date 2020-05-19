using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPress : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject playerObject;
    public PlayerStats player;
    //public Guns gun;
    public InfoBottom informationPanel;

    public GameObject[] galochka;


    public void PickItemNum(int i)
    {
        InfoBottom.item_num = i;
        Debug.Log("Picked " + InfoBottom.item_type + ", Num " + InfoBottom.item_num);
        switch (InfoBottom.item_type)
        {
            case "Gun":
                informationPanel.setInfo(player.guns[i].name, player.guns[i].price.ToString());
                break;
            case "Cave":
                informationPanel.setInfo(player.caves[i].name, player.caves[i].price.ToString());
                break;
            default:
                Debug.Log("Wrong item type");
                break;
        }

        CheckPurchised();
    }


    public void PickItemType(string a)
    {
        InfoBottom.item_type = a;
        CheckPurchised();
    }


    public void CheckPurchised()
    {
        for (int i = 0; i < galochka.Length; i++)
        { 
            bool a = false;
            switch (InfoBottom.item_type)
            {
                case "Gun":
                    a = player.guns[i].purchised;
                    break;
                case "Cave":
                    a = player.caves[i].purchised;
                    break;
                default:
                    Debug.Log("Wrong item type");
                    break;
            }
            if (a)
            {
                galochka[i].SetActive(true);
            } else
            {
                galochka[i].SetActive(false);
            }
        }
     
    }



}
