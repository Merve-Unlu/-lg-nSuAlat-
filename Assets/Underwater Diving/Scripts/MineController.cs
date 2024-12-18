using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI sınıfını dahil edin
public class MineController : MonoBehaviour {
    public GameObject explosion;   // Patlama efekti
    public Text gameOverText;      // Oyun bitti metni

    // Use this for initialization
    void Start()
    {
        gameOverText.gameObject.SetActive(false);  // Başlangıçta "Oyun Bitti!" yazısını gizle
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            // Bomba patladı
            Destroy(gameObject);  // Bombayı yok et
            Instantiate(explosion, transform.position, transform.rotation);  // Patlama efekti oluştur

            // Oyun Bitti yazısını göster
            gameOverText.gameObject.SetActive(true);  // "Oyun Bitti!" yazısını göster
        }
    }

}
