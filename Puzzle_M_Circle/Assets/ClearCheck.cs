using UnityEngine;
using UnityEngine.UI;

public class ClearCheck : MonoBehaviour
{
    [SerializeField] private Transform[] ans;
    [SerializeField] private Transform[] play;
    [SerializeField] string answerStr;       //答えオブジェクトの名前を並べたもの
    [SerializeField] string playerStr;       //プレイヤーオブジェクトの名前を並べたもの

    [SerializeField] private Text stageNum;
    private int sNum = 1;

    private void Start()
    {
        SetAnswer();
    }

    // Update is called once per frame
    void Update()
    {
        //それぞれの名前を繋げる処理 あとで関数にまとめまーす💛
        {
            playerStr = "";

            foreach (Transform o in play) {
                if (o.childCount > 0)
                    playerStr = playerStr + o.GetChild(0).gameObject.name;
            }
        }

        if (answerStr == playerStr)
        {
            //クリア
            Shuffle();
        }
    }

    void SetAnswer() {
        answerStr = "";

        foreach (Transform o in ans)
        {
            if (o.childCount > 0)
                answerStr = answerStr + o.GetChild(0).gameObject.name;
        }
    }

    void Shuffle() {
        int n = ans.Length;

        while (n > 1) {

            n--;

            int k = UnityEngine.Random.Range(0, n + 1);
            GameObject temp = ans[k].GetChild(0).gameObject;
            ans[k].GetChild(0).gameObject.transform.parent = ans[n];
            ans[n].GetChild(0).gameObject.transform.parent = ans[k];

        }

        SetAnswer();

        //ステージ数を進める
        sNum++;
        stageNum.text = sNum.ToString("00");
    }
}
