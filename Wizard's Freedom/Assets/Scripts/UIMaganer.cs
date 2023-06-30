using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaganer : MonoBehaviour
{
    [SerializeField] private List<GameObject> listCorazones;
    [SerializeField] private Sprite corazondesactivad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestaCorazones(int indice)
    {
        try 
        {
            Image imagenCora = listCorazones[indice].GetComponent<Image>();
            imagenCora.sprite = corazondesactivad;
        }
        catch (Exception e)
        {
          Debug.Log(e.Message);
        }
        
    }
}
