using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pick(int i)
    {
        Debug.Log(i);
        PlayerStats.gun = i;
    }
}
