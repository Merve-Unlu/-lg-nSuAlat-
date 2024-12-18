using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtPlayer : MonoBehaviour {

    public float moveSpeed = 2f;  // Balık hızını ayarlayabilirsiniz
    public float followDistance = 7f; // Balığın oyuncuya ne kadar yaklaşıp takip etmesi gerektiğini belirler
    private Transform playerTransform;  // Oyuncu referansı
    private Rigidbody2D rb;
    private Animator myAnim; // Animatör referansı
    private Vector2 randomDirection;  // Balığın rastgele yönü

    public float wanderTime = 3f;  // Balığın rastgele dönme süresi
    private float wanderTimer = 0f;  // Rastgele dönüş süresi sayacı
    private float fleeDistance = 7f;  // Oyuncu balığa yaklaştığında kaçma mesafesi
    private float smoothTurnSpeed = 2f; // Yön değiştirme hızını yumuşatmak için kullanılan parametre

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;  // Oyuncuyu buluyoruz
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();  // Animator bileşeni
        randomDirection = new Vector2(1, 0);  // Başlangıçta sağa hareket etsin
    }

    // Update is called once per frame
    void Update()
    {
        // Eğer oyuncu balığa yaklaşırsa, kaçma işlemi başlat
        if (Vector2.Distance(transform.position, playerTransform.position) < fleeDistance)
        {
            FleeFromPlayer();
        }
        else
        {
            Wander();
        }
    }

    // Balık oyuncudan kaçmak için hareket eder
    void FleeFromPlayer()
    {
        // Oyuncudan kaçmak için yön hesapla
        Vector2 direction = (transform.position - playerTransform.position).normalized;  // Balık, oyuncudan uzaklaşmalı

        // Yönü kontrol et (Yatay)
        if (direction.x > 0)
        {
            myAnim.SetBool("IsMovingRight", true);  // Animasyonu sağa hareket olarak tetikle
            myAnim.SetBool("IsMovingLeft", false); // Diğer yönü sıfırla
        }
        else if (direction.x < 0)
        {
            myAnim.SetBool("IsMovingLeft", true);  // Animasyonu sola hareket olarak tetikle
            myAnim.SetBool("IsMovingRight", false); // Diğer yönü sıfırla
        }

        // Balık yukarı veya aşağıya dönmeli
        if (direction.y > 0)
        {
            myAnim.SetBool("IsMovingUp", true); // Yukarı hareket animasyonu
            myAnim.SetBool("IsMovingDown", false); // Diğer yönü sıfırla
        }
        else if (direction.y < 0)
        {
            myAnim.SetBool("IsMovingDown", true); // Aşağı hareket animasyonu
            myAnim.SetBool("IsMovingUp", false); // Diğer yönü sıfırla
        }

        // Balık hareketini uygula, sadece yatay ve dikey hareket
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(direction.x * moveSpeed, direction.y * moveSpeed), smoothTurnSpeed * Time.deltaTime);  // Yavaşça yön değiştir
    }

    // Balık kendi kendine rastgele hareket eder
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            randomDirection = new Vector2(Random.Range(0.5f, 1f), Random.Range(-0.5f, 1f)).normalized;  // Rastgele yön seç (sadece ileriye doğru)
            wanderTimer = wanderTime;  // Yeni rastgele yön için zaman sıfırlanır
        }

        // Yönü kontrol et (Yatay)
        if (randomDirection.x > 0)
        {
            myAnim.SetBool("IsMovingRight", true);  // Animasyonu sağa hareket olarak tetikle
            myAnim.SetBool("IsMovingLeft", false); // Diğer yönü sıfırla
        }
        else if (randomDirection.x < 0)
        {
            myAnim.SetBool("IsMovingLeft", true);  // Animasyonu sola hareket olarak tetikle
            myAnim.SetBool("IsMovingRight", false); // Diğer yönü sıfırla
        }

        // Balık yukarı veya aşağıya dönmeli
        if (randomDirection.y > 0)
        {
            myAnim.SetBool("IsMovingUp", true); // Yukarı hareket animasyonu
            myAnim.SetBool("IsMovingDown", false); // Diğer yönü sıfırla
        }
        else if (randomDirection.y < 0)
        {
            myAnim.SetBool("IsMovingDown", true); // Aşağı hareket animasyonu
            myAnim.SetBool("IsMovingUp", false); // Diğer yönü sıfırla
        }

        // Balığı rastgele hareket ettir
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(randomDirection.x * moveSpeed, randomDirection.y * moveSpeed), smoothTurnSpeed * Time.deltaTime);  // Yavaşça yön değiştir
    }
}
















