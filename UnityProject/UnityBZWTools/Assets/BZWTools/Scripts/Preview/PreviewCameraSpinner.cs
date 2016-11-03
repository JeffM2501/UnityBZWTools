using UnityEngine;
using System.Collections;

public class PreviewCameraSpinner : MonoBehaviour
{
	public float SpinSpeed = 10;

    public bool TakeScreenshots = false;
    public string OutputPath = string.Empty;

    public bool AutoConvert = false;
    public string ImageMagickPath = string.Empty;
    protected float TotalSpin = 0;

    protected int Count = 0;
    protected bool convertDone = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
    
	// Update is called once per frame
	void Update ()
	{
        float delta = 1;

        if (TakeScreenshots && OutputPath != string.Empty)
        {
            if (TotalSpin > 360.0)
            {
                if (AutoConvert && !convertDone)
                {
                    convertDone = true;
                    try
                    {
                        System.Diagnostics.ProcessStartInfo st = new System.Diagnostics.ProcessStartInfo();
                        st.UseShellExecute = true;
                        st.FileName = ImageMagickPath;
                        st.Arguments = "-delay 5 -loop 0 " + OutputPath + "*.png " + OutputPath + "animated.gif";

                        System.Diagnostics.Process.Start(st);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Log(ex.ToString());
                    }
                }
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                UnityEngine.Application.Quit();
#endif
                return;
            }
            Count++;

            Application.CaptureScreenshot(System.IO.Path.Combine(OutputPath, "frame_" + Count.ToString("D4") + ".png"));
        }
        else
            delta = (Time.deltaTime * SpinSpeed) / Mathf.PI;

        TotalSpin += delta;
        transform.Rotate(Vector3.up, delta);
	}
}
