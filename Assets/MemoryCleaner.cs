using System;
using UnityEngine;

/// <summary>
/// Unityエディタでの再生停止時（およびゲーム破棄時）に未使用メモリを解放するスクリプト
/// </summary>
public class MemoryCleaner : MonoBehaviour
{
    // 再生停止（オブジェクト破棄）時に実行
    private void OnDestroy()
    {
        CleanMemory();
    }

    /// <summary>
    /// メモリ解放の本体処理
    /// </summary>
    public void CleanMemory()
    {
        // 1. 使われていないResourcesアセットをアンロード
        Resources.UnloadUnusedAssets();

        // 2. C#マネージドメモリの強制ガベージコレクション
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Debug.Log("[MemoryCleaner] 実行終了に伴い、メモリを解放しました。");
    }
}