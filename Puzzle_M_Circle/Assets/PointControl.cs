using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{

    Vector3 ori_Pos;    //ポインターの最初の座標(起点)
    Transform tf;
    [SerializeField] private float power;

    [SerializeField] private GameObject selectCircle;
    [SerializeField] GameObject cir;

    //選択用
    [SerializeField] private Transform c_Select;
    [SerializeField] private GameObject selA;
    [SerializeField] private GameObject selB;
    private bool isSelect = false;

    //変更の親保存用
    [SerializeField] private Transform selTf;

    //ぽいんたーが重なってるオブジェ
    [SerializeField] private GameObject olObj;

    // Start is called before the first frame update
    void Start()
    {
        ori_Pos = transform.position;   //初期位置の保存
        tf = transform;
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        tf.position = new Vector3(hori * power, vert * power, 0);

        //選択
        if (cir)
        {

            //されてない状態
            if (!isSelect)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    selA = olObj;                               //選択したオブジェ保存
                    selTf = selA.transform.parent;              //1個目の親オブジェ
                    selA.transform.parent = c_Select;           //選択位置に移動
                    
                    isSelect = true;                            //選択フラグを立てる

                    //Instantiate(selectCircle, selTf.position, Quaternion.identity);
                }
            }
            else //されてる状態
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    selB = olObj;
                    selA.transform.parent = selB.transform.parent;
                    selB.transform.parent = selTf;
                    selA.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                    selA = null;
                    selB = null;
                    selTf = null;
                    isSelect = false;
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            cir = Instantiate(selectCircle, other.gameObject.transform.position,Quaternion.identity);

            if (olObj)
            {
                if (olObj.transform.parent == c_Select)
                    return;
            }

            olObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(cir);

            olObj = null;
        }
    }
}
