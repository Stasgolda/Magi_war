using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour 
{
    public string cName;
    public Sprite sprite;

    private SpriteRenderer _sr;

    void Start ()
    {
        _sr = GetComponent<SpriteRenderer>();  
        _sr.sprite = sprite;
    }
}
