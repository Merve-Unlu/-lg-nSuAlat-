using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne kontrolü için gerekli kütüphane
public class Bomb : MonoBehaviour
{

    public GameObject explosionEffect; // Patlama efekti prefab
    public float explosionDelay = 0.5f; // Patlama sonrasý gecikme süresi

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Eðer Player dokunursa
        {
            Explode(other.gameObject); // Patlama fonksiyonu çaðrýlýr
            GameOver(); // Oyunu sonlandýr
        }
    }

    void Explode(GameObject player)
    {
        // Patlama efekti oluþtur
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Player'ý etkisiz hale getir
        Destroy(player, explosionDelay);

        // Bombayý yok et
        Destroy(gameObject);
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // Konsola kaybedildi mesajý
        // Sahneyi yeniden yükleyerek oyunu baþtan baþlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
