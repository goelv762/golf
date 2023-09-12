using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // camera used to change ortho size
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float zoomMulti;
    [SerializeField] private float minZoom, maxZoom;

    // zoom value 
    private float Zoom;

    private void Awake() {
        // sets zoom variable to current
        Zoom = virtualCamera.m_Lens.OrthographicSize;
    }

    private void FixedUpdate() {
        // zoom amount is changed
        Zoom -= BallMaster.scrollDelta * zoomMulti;

        // clamping to min and max
        Zoom = Mathf.Clamp(Zoom, minZoom, maxZoom);

        // apply zoom
        virtualCamera.m_Lens.OrthographicSize = Zoom;
    }
}
