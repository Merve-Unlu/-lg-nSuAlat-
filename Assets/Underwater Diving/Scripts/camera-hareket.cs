using UnityEngine;

public class camera_hareket : MonoBehaviour
{
    private GameObject Player; // Takip edilecek obje
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // Kamera ofseti (y�kseklik ve uzakl�k ayar�)

    public float smoothTime = 0.3f; // Kameran�n ge�i� yumu�akl�k s�resi
    private Vector3 velocity = Vector3.zero; // SmoothDamp i�in h�z vekt�r�

    void Start()
    {
        // "Player" tagine sahip objeyi sahnede bul
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("Player tagine sahip bir GameObject bulunamad�!");
        }
    }

    void LateUpdate()
    {
        if (Player != null)
        {
            // Kamera hedef pozisyonu: Player pozisyonu + kamera offseti
            Vector3 targetedPosition = Player.transform.position + cameraOffset;

            // Kameray� SmoothDamp ile hedef pozisyona yumu�ak bir �ekilde ta��
            transform.position = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
        }
    }
}