using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Player; // Takip edilecek oyuncunun transformu
    public Vector3 offset;   // Kamera ile oyuncu arasındaki mesafe



    void LateUpdate()
    {
        // Kameranın pozisyonunu, oyuncunun pozisyonuna göre ayarla
        if (Player != null)
        {
            transform.position = new Vector3(Player.position.x + offset.x, Player.position.y + offset.y, offset.z);
        }
    }
}
