using System;
using UnityEngine;

public class IsCameraLookingAtMe : MonoBehaviour
{
    public Camera targetCamera;
    public bool isInView = false;

    public event Action<bool> AnnounceInView;
    
    public Renderer myRenderer;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        CheckIfInView();
    }

    void CheckIfInView()
    {
        // Get camera frustum planes
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);

        // Check if renderer's bounds intersect camera frustum
        isInView = GeometryUtility.TestPlanesAABB(planes, myRenderer.bounds);
        
        AnnounceInView?.Invoke(isInView);
    }
}
