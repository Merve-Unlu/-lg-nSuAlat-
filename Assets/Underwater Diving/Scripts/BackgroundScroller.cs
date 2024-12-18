using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 2f; // Hareket hýzý
    public float resetPosition = -10f; // Sýfýrlanacaðý pozisyon
    public float startPosition = 10f; // Baþlangýç pozisyonu

    void Update()
    {
        // Arka planý sola doðru hareket ettir
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Eðer belirli bir pozisyona gelirse baþa al
        if (transform.position.x <= resetPosition)
        {
            Vector3 newPosition = new Vector3(startPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
