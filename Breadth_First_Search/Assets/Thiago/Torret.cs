using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    public GameObject objeto;
    public float baseCheckRadius = 1f; // Raio de verificação de colisão com a base

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
            // Criar uma esfera de colisão em torno da torreta
            Collider[] colliders = Physics.OverlapSphere(objetoAtual.transform.position, baseCheckRadius);

            bool colideComBase = false;
            Transform baseTransform = null;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Base"))
                {
                    colideComBase = true;
                    baseTransform = collider.transform;
                    break;
                }
            }

            if (colideComBase)
            {
                // Verificar se a base já possui uma torreta
                TorretaSlot torretaSlot = baseTransform.GetComponent<TorretaSlot>();

                if (torretaSlot != null && torretaSlot.Torreta != null)
                {
                    // Destruir torreta existente
                    Destroy(torretaSlot.Torreta);
                }

                // Reposicionar na posição do primeiro filho da base
                if (baseTransform.childCount > 0)
                {
                    Transform filhoBase = baseTransform.GetChild(0);
                    objetoAtual.transform.position = new Vector3(filhoBase.position.x, filhoBase.position.y, filhoBase.position.z);
                }

                // Adicionar componente TorretaSlot à base
                if (torretaSlot == null)
                {
                    torretaSlot = baseTransform.gameObject.AddComponent<TorretaSlot>();
                }

                // Atribuir a torreta atual ao slot da base
                torretaSlot.Torreta = objetoAtual;
            }
            else
            {
                // Destruir se não colidir com a base
                Destroy(objetoAtual);
            }

            objetoAtual = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenhar a esfera de colisão na cena para fins de depuração
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, baseCheckRadius);
    }
}