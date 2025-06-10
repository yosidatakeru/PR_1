using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInDownScript : MonoBehaviour
{
    float targetY = -700f;
    float speed = 100f;

    private RectTransform rectTransform;
    private bool hasReachedTarget = false;




    void Start()
    {
        // UI•”•i‚ÌŽæ“¾
        rectTransform = GetComponent<RectTransform>();

    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            Vector2 anchoredPos = rectTransform.anchoredPosition;
            float newY = Mathf.MoveTowards(anchoredPos.y, targetY, speed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(anchoredPos.x, newY);

            if (Mathf.Approximately(newY, targetY))
            {
                hasReachedTarget = true;
               
            }
        }
    }
}
