using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//La classe deve ereditare da IPointerDownHandler, IPointerUpHandler, IDragHandler
//per poter usare le funzioni di UNity OnDrag, OnPointerDown e OnPointerUp
public class VirtualPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform externSpace;
    public RectTransform innerPad;
    public static Vector2 inputDir;
    public static bool padReleased = false;

    //valuto in che direzione sto muovendo la levetta tramite una differenza tra la sua posizione al tocco
    //e quella dello spazio intermedio al virtual joystick. Se dal modulo risulta che sono al di fuori di 
    //quest'ultimo normalizzo la posizione e la moltiplico per la larghezza dello spazio intermedio
    //per assicurarmi di restarci. 
    //Predispongo la direzione che verrà usata dallo SphereController
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 directPos = eventData.position - (Vector2)externSpace.position;
        if (directPos.magnitude > externSpace.rect.width / 2f)
            directPos = directPos.normalized * externSpace.rect.width / 2f;
        innerPad.anchoredPosition = directPos;
        inputDir = innerPad.anchoredPosition / (externSpace.rect.size / 2f);
    }

    //valuta se il virtual joystick è stato premuto
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    //valuta se il virtual joystick è stato rilasciato.
    //Riporto la levetta nella posizione originale e setto padReleased
    //perché possa essere usato da SphereController
    public void OnPointerUp(PointerEventData eventData)
    {
        innerPad.anchoredPosition = Vector2.zero;
        innerPad.localRotation = Quaternion.identity;
        inputDir = Vector2.zero;
        padReleased = true;
    }


}
