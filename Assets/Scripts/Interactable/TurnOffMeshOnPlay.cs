using UnityEngine;

public class TurnOffMeshOnPlay : MonoBehaviour
{
    public MeshRenderer mR;

    void Awake()
    {
        mR.enabled = false;
    }
}
