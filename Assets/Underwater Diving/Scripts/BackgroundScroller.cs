using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 2f; // Hareket h�z�
    public float resetPosition = -10f; // S�f�rlanaca�� pozisyon
    public float startPosition = 10f; // Ba�lang�� pozisyonu

    void Update()
    {
        // Arka plan� sola do�ru hareket ettir
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // E�er belirli bir pozisyona gelirse ba�a al
        if (transform.position.x <= resetPosition)
        {
            Vector3 newPosition = new Vector3(startPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
