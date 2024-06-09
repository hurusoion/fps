using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerReference : MonoBehaviour
{
    public static CanvasScalerReference Instance { get; private set; }
    public float ScaleFactor { get; private set; }

    private CanvasScaler canvasScaler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        canvasScaler = GetComponent<CanvasScaler>();
        ScaleFactor = canvasScaler.scaleFactor;
    }
}
