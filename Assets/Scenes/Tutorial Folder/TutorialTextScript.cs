using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextScript : MonoBehaviour
{
    private TMP_Text Tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void TutorialText()
    {
        Tutorial = GetComponent<TMP_Text>();
        Tutorial.text = "Lスティックでプレイヤーの移動";
    }

    public void TutorialText2()
    {
        Tutorial = GetComponent<TMP_Text>();
        Tutorial.text = "Rスティックでレティクルの移動・押し込み切り替え(プレイヤー追従ON/OFF)";
    }

    public void TutorialText3()
    {
        Tutorial = GetComponent<TMP_Text>();
        Tutorial.text = "Rトリガーで攻撃";
    }
    public void TutorialText4()
    {
        Tutorial = GetComponent<TMP_Text>();
        Tutorial.text = "ロックオン中にRBで追尾弾発射";
    }
   
   

}
