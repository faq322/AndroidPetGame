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
        GameObject h = Instantiate(this.shopInfotext, shopInfotext.transform.position, rotation);
        h.transform.localScale = shopInfotext.transform.localScale;
        //h.transform.parent = this.transform;
        h.transform.SetParent(this.transform, false);
        h.transform.localScale = new Vector3(1,1,1);
        h.GetComponent<Text>().text = _text;
        h.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(h);
        //shopInfotext.SetActive(false);
        //shopInfotext.transform.position = a;
    }
}
