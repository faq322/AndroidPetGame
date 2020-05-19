using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopInfotext;
    void Awake()
    {
       // shopInfotext.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Appear(string _text)
    {
        StartCoroutine(Disappear(shopInfotext.transform.position, _text));
    }

    IEnumerator Disappear(Vector3 a, string _text)
    {
        var rotation = Quaternion.Euler(0, 0, 0);
        GameObject h = Instantiate(shopInfotext, shopInfotext.transform.position, rotation);
        h.transform.parent = this.transform;
        h.GetComponent<Text>().text = _text;
        h.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(h);
        //shopInfotext.SetActive(false);
        //shopInfotext.transform.position = a;
    }
}
