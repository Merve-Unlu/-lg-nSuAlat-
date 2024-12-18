using UnityEngine;

public class camera_hareket : MonoBehaviour
{
    private GameObject Player; // Takip edilecek obje
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // Kamera ofseti (yükseklik ve uzaklýk ayarý)

    public float smoothTime = 0.3f; // Kameranýn geçiþ yumuþaklýk süresi
    private Vector3 velocity = Vector3.zero; // SmoothDamp için hýz vektörü

    void Start()
    {
        // "Player" tagine sahip objeyi sahnede bul
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("Player tagine sahip bir GameObject bulunamadý!");
        }
    }

    void LateUpdate()
    {
        if (Player != null)
        {
            // Kamera hedef pozisyonu: Player pozisyonu + kamera offseti
            Vector3 targetedPosition = Player.transform.position + cameraOffset;

            // Kamerayý SmoothDamp ile hedef pozisyona yumuþak bir þekilde taþý
            transform.position = Vector3.SmoothDamp(transform.position, targetedPosition, ref velocity, smoothTime);
        }
    }
}