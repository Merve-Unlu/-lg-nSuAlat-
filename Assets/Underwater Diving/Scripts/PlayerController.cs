using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

 


    public float moveSpeed;
    public bool rushing = false;
    private float speedMod = 0;

    float timeLeft = 2f;

    private Rigidbody2D myRigidBody;

    private Animator myAnim;

    public GameObject bubbles;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        resetBoostTime();
        controllerManager();



        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.linearVelocity.x));



    }

    void controllerManager()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Sağ hareket
        if (horizontalInput > 0f)
        {
            // Yönü değiştirme (boyut değiştirmeden sağa hareket)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Yönü değiştirme
            movePlayer(1f); // Sağ yön
        }
        // Sol hareket
        else if (horizontalInput < 0f)
        {
            // Yönü değiştirme (boyut değiştirmeden sola hareket)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Yönü değiştirme
            movePlayer(-1f); // Sol yön
        }
        else
        {
            // Hareket yoksa yönü sıfırla (doğal yön)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            myRigidBody.linearVelocity = new Vector2(0f, myRigidBody.linearVelocity.y); // Yalnızca dikey hareket
        }

        // Yukarı hareket
        if (verticalInput > 0f)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, moveSpeed);
        }
        // Aşağı hareket
        else if (verticalInput < 0f)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, -moveSpeed);
        }

        // Rush hareketi
        if (Input.GetButtonDown("Jump") && !rushing)
        {
            rushing = true;
            speedMod = 2;  // Hız modifikasyonu
            Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
            movePlayer(Mathf.Sign(myRigidBody.linearVelocity.x)); // Mevcut yönü kullanarak hareket
        }
    }

    void movePlayer(float direction)
    {
        // Yönü değiştirme ve hızlandırma
        myRigidBody.linearVelocity = new Vector2(direction * (moveSpeed + speedMod), myRigidBody.linearVelocity.y);
    }




    void movePlayer()
    {
        if (transform.localScale.x == 1)
        {
            myRigidBody.linearVelocity = new Vector3(moveSpeed + speedMod, myRigidBody.linearVelocity.y, 0f);
        }
        else
        {
            myRigidBody.linearVelocity = new Vector3(-(moveSpeed + speedMod), myRigidBody.linearVelocity.y, 0f);
        }
    }

    void resetBoostTime()
    {
        if (timeLeft <= 0)
        {
            timeLeft = 2f;
            rushing = false;
            speedMod = 0;
        }
        else if (rushing)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    public void hurt()
    {
        if (!rushing)
        {
            gameObject.GetComponent<Animator>().Play("PlayerHurt");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Oyuncuyu geri it
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero; // Hareketini durdur
            }
        }
    }

}



