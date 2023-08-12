using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    [Header("Camera Component(s)")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Zoom Attributes")]
    // speed of zoom
    [SerializeField] private float zoomMulti;

    // limiting the zoom amount
    [SerializeField] private float minZoom, maxZoom;

    // time it takes to preform zoom
    [SerializeField] private float zoomSmoothTime;

    private float currZoom;
    // velocity (unused)
    private float vel;

    private void Awake() {
        currZoom = virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        currZoom -= BallMaster.scrollDelta * zoomMulti;
        currZoom = Mathf.Clamp(currZoom, minZoom, maxZoom);

        virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, currZoom, ref vel, zoomSmoothTime);
    }
}
