using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject megabullet;
    [SerializeField] float firerate;
    private float nextfire = 0f;
    [SerializeField] bool flag = true;
    private float timePressed = 0f;

    float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfIz = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        minX = esquinaInfIz.x;
        minY = esquinaInfIz.y;
        maxX = esquinaSupDer.x;
        maxY = esquinaSupDer.y;
    }


    //Esta función calcula el tiempo que se mantiene presionado la tecla espacio
    void keyPressedTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timePressed = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            timePressed = Time.time - timePressed;
            Debug.Log("Espacio presionado por: "+ timePressed + " Segundos");

        }
    }

    // Update is called once per frame
    void Update()
    {

        keyPressedTimer();


        if (Input.GetKeyDown(KeyCode.M))
        {
            flag = !flag;
            Debug.Log("Cambio de bala.");
        }
            

        float dirH = Input.GetAxis("Horizontal");
        float dirV = Input.GetAxis("Vertical");

        float x = dirH * speed * Time.deltaTime;
        float y = dirV * speed * Time.deltaTime;

        transform.Translate(new Vector2(x, y));

        transform.position = (new Vector2(Mathf.Clamp(transform.position.x, minX, maxX) , Mathf.Clamp(transform.position.y, minY, maxY)));

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextfire && flag == true)
        {
            nextfire = Time.time + firerate;
            Instantiate(bullet, transform.position, transform.rotation);
        }
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > nextfire && flag == false && timePressed >= 3)
        {
            nextfire = Time.time + firerate;
            Instantiate(megabullet, transform.position, transform.rotation);
        }
    }
}
