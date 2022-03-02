using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{ 
    Rigidbody2D myBody;

    [SerializeField] float speed;
    [SerializeField] int lives = 5;

    float minX, maxX, maxY, minY;

    // Start is called before the first frame update
    void Start()
    {
        // Limite del que no pasa el personaje
        Vector2 esqInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esqInfIzq.x;
        minY = esqInfIzq.y;


        Vector2 esqSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esqSupDer.x;
        maxY = esqSupDer.y;

        myBody = GetComponent<Rigidbody2D>();
    }

    private void ChangeDirection()
    {
        if (transform.position.x == maxX || transform.position.x == minX)
        {   
            speed = -speed;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        //para los limites del que el personaje no se pasa
        transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, minX, maxX),
        Mathf.Clamp(transform.position.y, minY, maxY));

        if (lives == 0)
        {
            Destroy(this.gameObject);
            Debug.Log(this.gameObject.name+" ha sido eliminado.");
        }

        ChangeDirection();
    }

    private void FixedUpdate()
    {

        transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, minX, maxX),
        Mathf.Clamp(transform.position.y, minY, maxY));

        myBody.velocity = new Vector2(speed, myBody.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colisionando con: " + collision.gameObject.name);
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            lives = lives - 1;
            Debug.Log("Vidas restantes de "+this.gameObject.name+": "+lives);
        }

        if (collision.gameObject.name == "MegaBullet(Clone)")
        {
            lives = 0;

        }

    }

}
