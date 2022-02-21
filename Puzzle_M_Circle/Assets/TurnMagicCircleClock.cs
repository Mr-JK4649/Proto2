using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMagicCircleClock : MonoBehaviour
{

    //距離を取得（定数）
    private float OriDis;

    //右回り用フラグ
    private bool canRightTurn;

    //左回り用フラグ
    private bool canLeftTurn;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = GameObject.Find("Two").transform.localPosition;
        OriDis = Vector2.Distance(Vector2.zero, new Vector2(pos.x, pos.z));
    }

    void Update()
    {
        TrunSelect();

        TrunCircle();

    }

    private void TrunSelect()
    {
        if (!canLeftTurn && !canRightTurn)
        {
            if (Input.GetButtonDown("Fire5")&& (Input.GetButtonDown("Fire4"))){
                return;
            }
               if (Input.GetButtonDown("Fire5"))
            {
                canRightTurn = true;
            }
            if (Input.GetButtonDown("Fire4"))
            {
                canLeftTurn = true;
            }
        }
    }

    private void TrunCircle()
    {
        if (canRightTurn)
        {
            RightTrun();
        }

        if (canLeftTurn)
        {
            LeftTrun();
        }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    float Notmuch = 72.0f;
    void RightTrun()
    {
        float TrunNum = 360.0f * Time.deltaTime;
        Notmuch -= TrunNum;

        foreach (Transform child in this.transform)
        {
            float ang = GetAngle(Vector2.zero, new Vector2(child.localPosition.x, child.localPosition.z));

            if (Notmuch < TrunNum)
            {
                ang -= Notmuch + TrunNum;
                canRightTurn = false;
            }
            else
            {
                ang -= TrunNum;
            }

            float rad = Mathf.Deg2Rad * ang;

            float posX = Mathf.Cos(rad) * OriDis;      //X軸の設定
            float posZ = Mathf.Sin(rad) * OriDis;      //Z軸の設定
            //child.localPosition = new Vector3(posX, 0, posZ);
        }
        if (!canRightTurn)
        {
            Notmuch = 72.0f;
            
            var start = transform.GetChild(0);
            transform.GetChild(0).GetChild(0).parent = transform.GetChild(1);
            transform.GetChild(1).GetChild(0).parent = transform.GetChild(2);
            transform.GetChild(2).GetChild(0).parent = transform.GetChild(3);
            transform.GetChild(3).GetChild(0).parent = transform.GetChild(4);
            transform.GetChild(4).GetChild(0).parent = start;

        }
    }

    void LeftTrun()
    {
        float TrunNum = 360.0f * Time.deltaTime;
        Notmuch -= TrunNum;

        foreach (Transform child in this.transform)
        {
            float ang = GetAngle(Vector2.zero, new Vector2(child.localPosition.x, child.localPosition.z));

            if (Notmuch < TrunNum)
            {
                ang += Notmuch + TrunNum;
                canLeftTurn = false;
            }
            else
            {
                ang += TrunNum;
            }

            float rad = Mathf.Deg2Rad * ang;

            float posX = Mathf.Cos(rad) * OriDis;      //X軸の設定
            float posZ = Mathf.Sin(rad) * OriDis;      //Z軸の設定
            //child.localPosition = new Vector3(posX, 0, posZ);
        }
        if (!canLeftTurn)
        {
            Notmuch = 72.0f;
            var last = transform.GetChild(4);
            transform.GetChild(4).GetChild(0).parent = transform.GetChild(3);
            transform.GetChild(3).GetChild(0).parent = transform.GetChild(2);
            transform.GetChild(2).GetChild(0).parent = transform.GetChild(1);
            transform.GetChild(1).GetChild(0).parent = transform.GetChild(0);
            transform.GetChild(0).GetChild(0).parent = last;
        }
    }
}
