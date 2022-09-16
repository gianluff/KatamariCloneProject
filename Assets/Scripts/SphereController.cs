using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    
    public float speed = 2000;
    void Main()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void FixedUpdate()
    {
        //Se il virtual joystick è stato appena rilasciato fermo la sfera
        if (VirtualPad.padReleased == true)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            VirtualPad.padReleased = false;
        }
        //controllo se il giocatore sta utilizzando il virtual joystick
        //Se inputDir è un vettore non nullo allora il VJ non è nella posizione
        //di riposo. Applico una sfera una forza con la direzione del VJ
        if (VirtualPad.inputDir != Vector2.zero)
        {
            Vector3 inputDir3D = new Vector3(0, 0, 0);
            inputDir3D.Set(VirtualPad.inputDir.x, 0, VirtualPad.inputDir.y);
            GetComponent<Rigidbody>().AddForce(inputDir3D * speed * Time.deltaTime);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                //Il controllo touch ha priorità minore. Se tocco un punto dello schermo ottengo un vettore direzione
                //come differenza tra le coordinate del punto dello schermo toccato e la posizione della sfera sullo schermo.
                //Imprimo alla sfera una forza in quella direzione. Normalizzo il vettore direzione dividendolo per il suo
                //modulo per ottenere una velocità coerente indipendentemente da quanto lontano dalla palla avviene il tocco
                if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Stationary)
                {
                    Vector3 ScreenSpherePos3D = Camera.main.WorldToScreenPoint(transform.position);
                    Vector2 ScreenSpherePos2D = new Vector2(ScreenSpherePos3D.x, ScreenSpherePos3D.y);
                    Vector3 touchMovement = new Vector3(0, 0, 0);
                    touchMovement.Set(Input.touches[0].position.x - ScreenSpherePos2D.x, 0, Input.touches[0].position.y - ScreenSpherePos2D.y);
                    GetComponent<Rigidbody>().AddForce(touchMovement / touchMovement.magnitude * speed * Time.deltaTime);
                }
                //Se stacco il dito dallo schermo voglio che la palla si fermi
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
            else
            {
                //Con priorità minima, se non sto usando il virtual joystick e non sto toccando lo schermo
                //valuto come input i valori forniti dall'accelerometro/giroscopio
                Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
                GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //In caso di collisione con un cubo faccio sì che questo si attacchi alla sfera
        //semplicemente settando quest'ultima come "genitore"
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collision.transform.SetParent(transform);
        }
    }
}

