using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour {

    public Material mat;
    private void Update()
    {
        mat.SetFloat("_LerpValue", Mathf.Sin(Time.time));

    }
}
