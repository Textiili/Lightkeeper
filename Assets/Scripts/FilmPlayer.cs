using Cinemachine;
using UnityEngine;

public class FilmPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private void Start() {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update() {
        if (vCam.Follow != null) return;
        var player = GameObject.Find("Player");
        vCam.Follow = player.transform;
    }
}
