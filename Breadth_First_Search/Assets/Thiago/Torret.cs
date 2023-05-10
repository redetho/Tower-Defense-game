using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    public GameObject objeto; 
    private GameObject objetoAtual; 
    private Vector3 posicaoInicial; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit)) 
            {
                posicaoInicial = hit.point; 
                objetoAtual = Instantiate(objeto, posicaoInicial, Quaternion.identity);
            }
        }
        else if (Input.GetMouseButton(0) && objetoAtual != null) 
        {
            Vector3 posicaoMouse = Input.mousePosition;
            posicaoMouse.z = Camera.main.WorldToScreenPoint(objetoAtual.transform.position).z; 
            Vector3 posicaoObjeto = Camera.main.ScreenToWorldPoint(posicaoMouse); 
            objetoAtual.transform.position = posicaoObjeto; 
        }
        else if (Input.GetMouseButtonUp(0) && objetoAtual != null) 
        {
            objetoAtual.transform.position = new Vector3(objetoAtual.transform.position.x, posicaoInicial.y, objetoAtual.transform.position.z); 
            objetoAtual = null; 
        }
    }
}
