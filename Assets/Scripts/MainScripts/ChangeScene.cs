using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Start Game
    public void changemenuscene(string scenename)
    {
        if (!ShopControls.shopActive)
        {
            PlayerStats.inGame = true; //игровая сцена
            PlayerStats.alive = true; //жив ли игрок
            PlayerStats.win = false; //победа
            PlayerStats.lose = false; //проигрыш
            PlayerStats.start = false; //начало волны
           // Application.LoadLevel (scenename);
            SceneManager.LoadScene(scenename);

        }
    }

    //void checkBeforeStart()
}
