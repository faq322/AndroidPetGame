using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public Transform playerTarget;
    public Transform firePoint;

    //скорость движения прицела
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    public bool circularBorder = true;
    [SerializeField]
    public bool squareBorder = false;
    void OnMouseDrag ()
    {
        if (PlayerStats.inGame)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //square border
            if (squareBorder)
            {
                mousePos.x = mousePos.x < -1.35f ? -1.35f : mousePos.x;
                mousePos.x = mousePos.x > 8f ? 8f : mousePos.x;
                mousePos.y = mousePos.y < -2.5f ? -2.5f : mousePos.y;
                mousePos.y = mousePos.y > 2.5f ? 2.5f : mousePos.y;
                playerTarget.position = Vector2.MoveTowards(playerTarget.position, new Vector2(mousePos.x, mousePos.y), speed * Time.deltaTime);
            }
            //circular border
            if (circularBorder)
            {
                float acc = 0.2f; // погрешность
                float newZ = 0;
                mousePos.x = mousePos.x < -1.35f ? -1.35f : mousePos.x;
                mousePos.x = mousePos.x > 8f ? 8f : mousePos.x;
                mousePos.y = mousePos.y < -2.5f ? -2.5f : mousePos.y;
                mousePos.y = mousePos.y > 2.5f ? 2.5f : mousePos.y;
                if ((mousePos.y > playerTarget.position.y + acc) && (playerTarget.position.x > -2.5f))
                {
                    newZ = 15f;
                } else if ((mousePos.y < playerTarget.position.y - acc) && (playerTarget.position.y > -3.0f))
                {
                    newZ = -15f;
                }

               // playerTarget.transform.Rotate(new Vector3(0.0f, 0.0f, -newZ) * speed * Time.deltaTime);
                //playerTarget.rotation = new Vector3(0.0f, 0.0f, 0f);
                playerTarget.transform.RotateAround(firePoint.position, new Vector3(0.0f, 0.0f, newZ), 3.0f*speed * Time.deltaTime);
                playerTarget.rotation = Quaternion.identity;
                // playerTarget.position = Vector2.MoveTowards(playerTarget.position, new Vector2(mousePos.x, newY), speed * Time.deltaTime);
            }

        }

    }

/*
    float getFirePointAngle()
    {
        var x = target.position.x - firePoint.position.x;
        var y = target.position.y - firePoint.position.y;
        //чуть увеличиваем угол с расчетом на дальность прицела
        var bonusAngleFromDistance = x / (BulletMove.bulletSpeed - 3f) * 6f;
        //угол для разброса в зависимости от силы отдачи
        Random rand = new Random();
        //var randomAngle = rand.Next(-1*BulletMove.bulletPower,BulletMove.bulletPower);
        var randomAngle = 0;
        float angle = (float)System.Math.Atan(y / x) * 180f / 3.14f + bonusAngleFromDistance + randomAngle;
        Debug.Log("Bullet info: x=" + x + " y=" + y + " angle=" + angle + " +bonus=" + bonusAngleFromDistance);
        return angle;
    }*/

}
