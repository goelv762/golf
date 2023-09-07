using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float zoomMulti;
    [SerializeField] private float minZoom, maxZoom;
    [SerializeField] private float smoothTime;

    // zoom value 
    private float Zoom;
    // ref
    private float vel;

    private void Awake() {
        Zoom = virtualCamera.m_Lens.OrthographicSize;
    }

    private void FixedUpdate() {
        Zoom -= BallMaster.scrollDelta * zoomMulti;
        Zoom = Mathf.Clamp(Zoom, minZoom, maxZoom);
        virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, Zoom, ref vel, smoothTime);
    }
}
