using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //Mais info sobre GameObject: https://docs.unity3d.com/ScriptReference/GameObject.html
    //Vamos criar uma propriedade para o nosso script chamada jogador
    //a propriedade � publica e � do tipo GameObject (permite referenciar um objeto do nosso jogo)
    public GameObject jogador;

    //vari�vel privado pois podemos definir o valor diretamente no nosso c�digo sem precisar de reajustar
    //vai subtrair a posi��o do jogador � posi��o atual da c�mara
    //para encontrar a diferen�a entre os dois
    private Vector3 offset;
    void Start()
    {
        //no inicio vamos subtrair a posi��o do jogador � posi��o atual da c�mara
        offset = transform.position - jogador.transform.position;
    }
    //Para a camara utilizamos o m�todo LateUpdate
    //Desta forma garantimos que a camara s� � movimentada
    //depois de todos os outros componentes do jogo sofrerem modifica��es (movimento)
    //as c�maras que seguem outros objetos devem ser configuradas no m�todo LateUpdate()
    void LateUpdate()
    {
        //quando atualizar a posi��o do jogador a c�mara � movimentada para uma nova posi��o
        //perfeitamente alinhada com o jogador
        transform.position = jogador.transform.position + offset;
    }
}