using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{

    Transform tf;
    [SerializeField] private float power;

    [SerializeField] private GameObject selectCircle;
    //[SerializeField] GameObject cir;

    //選択用
    [SerializeField] private Transform c_Select;
    [SerializeField] private GameObject selA;
    [SerializeField] private GameObject selB;
    private bool isSelect = false;

    //変更の親保存用
    [SerializeField] private Transform selTf;

    //ぽいんたーが重なってるオブジェ
    [SerializeField] private GameObject olObj;

    //角度
    [SerializeField] private float ang;

    //吸いつき範囲
    [SerializeField, Range(0, 100)] private float dist = 1.5f;

    //ゲームオブジェクト用
    private GameObject[] circles;
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        tf = transform;

        RegisterCircles();
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 ppos = new Vector3(hori * power, vert * power, 0);

        tf.position = ppos;

        //スティックの角度を求める
        ang = Mathf.Atan2(vert, hori) * 180 / Mathf.PI;
        if (ang < 0) ang = 360.0f + ang;

        //吸いつき
        {

            //ポインターが一定以上の範囲に出た時
            if (Vector3.Distance(Vector3.zero, ppos) > 0.3f)
            {

                foreach (GameObject o in circles)
                {
                    GoToParent gp = o.GetComponent<GoToParent>();

                    olObj = null;

                    //魔法陣の中心からdist分の範囲内に入ったら
                    if (Vector3.Distance(ppos, o.transform.position) < dist)
                    {

                        //うんこ
                        olObj = o;

                        //選択サークルを出させる
                        gp.ShowSelectCircle(selectCircle);

                        //吸いつき
                        tf.position = o.transform.position;

                        //Bボタンで反転処理
                        if (Input.GetButtonDown("Fire2"))
                            gp.ChangeColor();
                    }
                    else {  //入って無ければ
                        gp.FadeSelectCircle();
                    }

                    //Aボタン選択
                    {

                        //されてない状態
                        if (!isSelect)
                        {
                            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
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
                            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
                            {
                                selB = olObj;
                                selA.transform.parent = selB.transform.parent;
                                selB.transform.parent = selTf;
                                selA.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                                selA = null;
                                selB = null;
                                selTf = null;
                                isSelect = false;
                            }
                        }

                    }
                }
            }
        }

        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "My") {
    //        cir = Instantiate(selectCircle, other.gameObject.transform.position,Quaternion.identity);

    //        if (olObj)
    //        {
    //            if (olObj.transform.parent == c_Select)
    //                return;
    //        }

    //        olObj = other.gameObject;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "My")
    //    {
    //        Destroy(cir);

    //        olObj = null;
    //    }
    //}

    public void RegisterCircles() {
        circles = GameObject.FindGameObjectsWithTag("My");
        num = circles.Length;
    }
}
