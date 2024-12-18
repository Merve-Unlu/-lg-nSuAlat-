using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Transform player; // Oyuncunun transformu
    //public float fleeDistance = 5f; // Oyuncuya olan korku mesafesi
    //public float speed = 2f; // Balık hareket hızı
    //public float neighborRadius = 3f; // Balıkların birbirini algılama mesafesi
    //public float separationDistance = 1.5f; // Balıkların birbirine minimum uzaklığı
    //public float maxFleeSpeed = 4f; // Kaçış sırasında maksimum hız

    //private Vector2 velocity; // Balığın mevcut hızı
    //private List<Enemy> neighbors = new List<Enemy>(); // Yakındaki balıklar

    //private void Start()
    //{
    //    velocity = Random.insideUnitCircle.normalized * speed;
    //}

    //private void Update()
    //{
    //    Vector2 moveDirection;

    //    // Oyuncu korku mesafesine girdiğinde balıklar dağılır
    //    if (Vector2.Distance(transform.position, player.position) < fleeDistance)
    //    {
    //        moveDirection = (Vector2)(transform.position - player.position).normalized;
    //        velocity = Vector2.Lerp(velocity, moveDirection * maxFleeSpeed, Time.deltaTime * 2f);
    //    }
    //    else
    //    {
    //        // Sürü davranışı
    //        neighbors.Clear();
    //        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

    //        foreach (var hit in hits)
    //        {
    //            if (hit != null && hit.gameObject != this.gameObject)
    //            {
    //                Enemy neighbor = hit.GetComponent<Enemy>();
    //                if (neighbor != null)
    //                    neighbors.Add(neighbor);
    //            }
    //        }

    //        moveDirection = Vector2.zero;

    //        // Separation: Balıklar birbirlerinden uzak durmaya çalışır
    //        foreach (var neighbor in neighbors)
    //        {
    //            Vector2 toNeighbor = (Vector2)(neighbor.transform.position - transform.position);
    //            if (toNeighbor.magnitude < separationDistance)
    //            {
    //                moveDirection -= toNeighbor.normalized;
    //            }
    //        }

    //        // Alignment: Balıklar aynı yönde hareket etmeye çalışır
    //        foreach (var neighbor in neighbors)
    //        {
    //            moveDirection += neighbor.velocity.normalized;
    //        }

    //        // Cohesion: Balıklar grup halinde kalmaya çalışır
    //        if (neighbors.Count > 0)
    //        {
    //            Vector2 centerOfMass = Vector2.zero;
    //            foreach (var neighbor in neighbors)
    //            {
    //                centerOfMass += (Vector2)neighbor.transform.position;
    //            }
    //            centerOfMass /= neighbors.Count;

    //            moveDirection += (centerOfMass - (Vector2)transform.position).normalized;
    //        }

    //        velocity = Vector2.Lerp(velocity, moveDirection.normalized * speed, Time.deltaTime);
    //    }

    //    // Balığın hareketini uygula
    //    transform.position += (Vector3)(velocity * Time.deltaTime);

    //    // Balığı hareket yönüne döndür
    //    if (velocity.sqrMagnitude > 0.01f)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, velocity);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    //    }

    //}

    //// Algılama alanlarını görselleştir
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, neighborRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, fleeDistance);
    //}
    public Transform player; // Oyuncunun transformu
    public float chaseDistance = 7f; // Kovalama mesafesi
    public float wanderSpeed = 1.5f; // Düzensiz yüzme hızı
    public float chaseSpeed = 3f; // Oyuncuya yaklaşma hızı
    public float directionChangeInterval = 2f; // Düzensiz yüzme yön değiştirme süresi

    private Vector2 targetDirection; // Hedef hareket yönü
    private float distanceToPlayer; // Oyuncuya olan mesafe
    private Rigidbody2D rb; // Fizik bileşeni

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Oyuncuya doğru hareket
            Vector2 moveDirection = (player.position - transform.position).normalized;
            rb.linearVelocity = moveDirection * chaseSpeed;
        }
        else
        {
            // Düzensiz yüzme
            rb.linearVelocity = targetDirection * wanderSpeed;
        }

        // Balığı hareket yönüne döndür
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // -90 derecelik açıyla balık yukarı bakar
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            targetDirection = Random.insideUnitCircle.normalized; // Rastgele bir yön seç
            yield return new WaitForSeconds(directionChangeInterval); // Belirtilen süre boyunca aynı yönde hareket et
        }
    }

    // Algılama alanını görselleştir
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

}
