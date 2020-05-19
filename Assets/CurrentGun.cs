using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGun : MonoBehaviour
{
    public GameObject[] gun;
    // Start is called before the first frame update
    void Start()
    {
        showUsedGun();
    }

    // Update is called once per frame
    public void showUsedGun()
    {
        for (int i = 0; i < gun.Length; i++)
        {
            gun[i].SetActive(false);
        }
        gun[PlayerStats.gun].SetActive(true);

    }
}
