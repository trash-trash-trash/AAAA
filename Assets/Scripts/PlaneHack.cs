using UnityEngine;

public class PlaneHack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
