using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        // Escapeキーが押された瞬間
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    private void Quit()
    {
        // ビルド版（PC等）での終了
        Application.Quit();

        // Unityエディター上で動作確認する場合の停止処理
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
