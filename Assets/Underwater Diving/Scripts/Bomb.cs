using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne kontrol� i�in gerekli k�t�phane
public class Bomb : MonoBehaviour
{

    public GameObject explosionEffect; // Patlama efekti prefab
    public float explosionDelay = 0.5f; // Patlama sonras� gecikme s�resi

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // E�er Player dokunursa
        {
            Explode(other.gameObject); // Patlama fonksiyonu �a�r�l�r
            GameOver(); // Oyunu sonland�r
        }
    }

    void Explode(GameObject player)
    {
        // Patlama efekti olu�tur
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Player'� etkisiz hale getir
        Destroy(player, explosionDelay);

        // Bombay� yok et
        Destroy(gameObject);
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // Konsola kaybedildi mesaj�
        // Sahneyi yeniden y�kleyerek oyunu ba�tan ba�lat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
