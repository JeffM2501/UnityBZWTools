using UnityEngine;
using System.Collections;

public class PreviewCameraSpinner : MonoBehaviour
{
	public float SpinSpeed = 10;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up, (Time.deltaTime * SpinSpeed)/Mathf.PI);
	}
}
