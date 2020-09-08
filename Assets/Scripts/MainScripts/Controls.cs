using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    Vector3 touchStart;

    public float zoomOutMin = 5;
    public float zoomOutMax = 10;

    public static bool shopMenuOpened = false;

    public bool border = true;

    public float minX = -2;
    public float maxX = 10;
    public float minY = -10;
    public float maxY = 0;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        Borders();
    }
    bool InactiveArea()
    {
        var positionX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        if (shopMenuOpened && positionX > 0.6) return false;
        return true;
    }
    void Borders()
    {
        if (border==true)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY),transform.position.z);
        }
    }
    void CameraMovement()
    {
        if (InactiveArea())
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}