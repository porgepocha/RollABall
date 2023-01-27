using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //Mais info sobre GameObject: https://docs.unity3d.com/ScriptReference/GameObject.html
    //Vamos criar uma propriedade para o nosso script chamada jogador
    //a propriedade é publica e é do tipo GameObject (permite referenciar um objeto do nosso jogo)
    public GameObject jogador;

    //variável privado pois podemos definir o valor diretamente no nosso código sem precisar de reajustar
    //vai subtrair a posição do jogador à posição atual da câmara
    //para encontrar a diferença entre os dois
    private Vector3 offset;
    void Start()
    {
        //no inicio vamos subtrair a posição do jogador à posição atual da câmara
        offset = transform.position - jogador.transform.position;
    }
    //Para a camara utilizamos o método LateUpdate
    //Desta forma garantimos que a camara só é movimentada
    //depois de todos os outros componentes do jogo sofrerem modificações (movimento)
    //as câmaras que seguem outros objetos devem ser configuradas no método LateUpdate()
    void LateUpdate()
    {
        //quando atualizar a posição do jogador a câmara é movimentada para uma nova posição
        //perfeitamente alinhada com o jogador
        transform.position = jogador.transform.position + offset;
    }
}