using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMagicCircle : MonoBehaviour
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
    }

    private void TrunSelect()
    {
        foreach (Transform child in this.transform)
        {
            float ang = GetAngle(Vector2.zero, new Vector2(child.localPosition.x, child.localPosition.z));

            if (Input.GetButton("Fire4"))
            {
                ang += 2 * Mathf.PI * Time.deltaTime;
            }
            if (Input.GetButton("Fire5"))
            {
                ang -= 2 * Mathf.PI * Time.deltaTime;
            }
            float posX = Mathf.Cos(ang) * OriDis;      //X軸の設定
            float posZ = Mathf.Sin(ang) * OriDis;      //Z軸の設定

            child.localPosition = new Vector3(posX, 0, posZ);
        }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);

        return rad;
    }
}
