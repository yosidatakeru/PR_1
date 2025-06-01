using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class explanationScript : MonoBehaviour
{
    public CanvasGroup MoveOperationInstructions;
    public CanvasGroup AccelerationOperationInstructions;
    public CanvasGroup DefenseOperationInstructions;
    public CanvasGroup reticleOperationInstructions;
    public CanvasGroup TrackingOperationInstructions;
    public CanvasGroup attackOperationInstructions;        
    public CanvasGroup gameOperationInstructions;
    public CanvasGroup itemOperationInstructions;
    public CanvasGroup ThepurposeOperationInstructions;

    CameraTitleScript cameraScript;

    int page = 1;
    float prevDpv = 0.0f; // クラスのフィールドに定義しておく
    private bool canTurnPage = true;

    // Start is called before the first frame update
    void Start()
    {
        MoveOperationInstructions.alpha = 0;
        AccelerationOperationInstructions.alpha = 0;
        DefenseOperationInstructions.alpha = 0;
        reticleOperationInstructions.alpha = 0;
        TrackingOperationInstructions.alpha = 0;
        attackOperationInstructions.alpha = 0;
        gameOperationInstructions.alpha = 0;
        itemOperationInstructions.alpha = 0;
        ThepurposeOperationInstructions.alpha = 0;
        page = 1;
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraTitleScript>();
    }

// Update is called once per frame
    void Update()
    {

        float dpH = Input.GetAxis("D_Pad_H");

        if (cameraScript.IsCameraMoveFinished == true)
        {
            // 右
            if ((Input.GetKeyDown(KeyCode.RightArrow) || (dpH == 1.0f && prevDpv != 1.0f)) && page <= 8 && canTurnPage)
            {
                page += 1;
                Debug.Log("ページをめくる+");
                canTurnPage = false;
            }

            // 左
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (dpH == -1.0f && prevDpv != -1.0f)) && page >= 1 && canTurnPage)
            {
                page -= 1;
                Debug.Log("ページをめくる-");
                canTurnPage = false;
            }

            // 入力が離されたときに再びページめくりを許可
            if (dpH == 0.0f)
            {
                canTurnPage = true;
            }

            // prevDpv 更新
            prevDpv = dpH;
        }

        if(page == 1) 
        {
            MoveOperationInstructions.alpha = 1;
            AccelerationOperationInstructions.alpha = 0;
        }

        if (page == 2)
        {
            MoveOperationInstructions.alpha = 0;
            AccelerationOperationInstructions.alpha = 1;
            DefenseOperationInstructions.alpha = 0;
        }

        if(page == 3) 
        {
            AccelerationOperationInstructions.alpha = 0;
            DefenseOperationInstructions.alpha = 1;
            reticleOperationInstructions.alpha = 0;
        }

        if (page == 4)
        {
            DefenseOperationInstructions.alpha = 0;
            reticleOperationInstructions.alpha = 1;
            TrackingOperationInstructions.alpha = 0;
        }

        if (page == 5)
        {
            reticleOperationInstructions.alpha = 0;
            TrackingOperationInstructions.alpha = 1;
            attackOperationInstructions.alpha = 0;

        }

        if (page == 6)
        {
            TrackingOperationInstructions.alpha = 0;
            attackOperationInstructions.alpha = 1;
            gameOperationInstructions.alpha = 0;
        }

        if (page == 7)
        {
            attackOperationInstructions.alpha = 0;
            gameOperationInstructions.alpha = 1;
            itemOperationInstructions.alpha = 0;
        }

        if (page == 8)
        {
            gameOperationInstructions.alpha = 0;
            itemOperationInstructions.alpha = 1;
            ThepurposeOperationInstructions.alpha = 0;
        }

        if (page == 9)
        {
            itemOperationInstructions.alpha = 0;
            ThepurposeOperationInstructions.alpha = 1;
        }
    }

    


}
