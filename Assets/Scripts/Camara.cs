using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public GameObject jogador;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - jogador.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = jogador.transform.position + offset;
    }
}
