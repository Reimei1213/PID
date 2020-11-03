using UnityEngine;
using System.Collections;
 
/// <summary>
/// 照度センサ
/// </summary>
public class IlluminanceSensor : MonoBehaviour {
    /// <summary>
    /// センシング対象カメラ
    /// </summary>
    [SerializeField]
    private Camera dispCamera;
 
    /// <summary>
    /// 照度センサ値
    /// </summary>
    public float illuminance;
 
    /// <summary>
    /// センシング対象カメラのRenderTexture
    /// </summary>
    private Texture2D targetTexture;
 
    /// <summary>
    /// 照度センサの値を計測する
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start() {
        var tex = dispCamera.targetTexture;
        targetTexture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
 
        while ( true ) {
            yield return new WaitForEndOfFrame();
 
            // RenderTextureキャプチャ
            RenderTexture.active = dispCamera.targetTexture;
            targetTexture.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            targetTexture.Apply();
 
            // 照度を取得する
            illuminance = GetLightValue(targetTexture);
        }
    }
 
    /// <summary>
    /// 画像全体の照度計算
    /// </summary>
    /// <param name="tex"></param>
    /// <returns></returns>
    private float GetLightValue(Texture2D tex) {
        var cols = tex.GetPixels();
 
        // 平均色計算
        var avg = new Color(0, 0, 0);
        foreach ( var col in cols ) {
            avg += col;
        }
        avg /= cols.Length;
 
        // 照度計算
        return avg.grayscale;
    }
}