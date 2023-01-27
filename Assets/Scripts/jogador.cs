using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jogador : MonoBehaviour
{
    public Text timer;
    public bool timeLimit = false, running = false, finished = false, isGrounded = true;
    public float startTime;
    public float jumpPower;

    //Variável contador
    private int contador = 0;

    //variável para suporte do Contador
    public Text contar;

    //variável para suportar o texto de vitória
    public Text vitoria;

    //Mais info da class Input: https://docs.unity3d.com/Script Reference/Input.html
    //Mais info da class Rigidbody: https://docs.unity3d.com/Script Reference/Rigidbody.html
    //Mais info estrutura Vector3: https://docs.unity3d.com/Script Reference/Vector3.html

    //A variável é declarada como pública (variáveis declaradas como públicas são adicionadas como propriedades do componente)
    //quando adicionamos variáveis como publicas podemos fazer alterações diretamente no editor
    //Variável de controlo de velocidade é adicionada como propriedade do componente (script)
    public float velocidade;
    //Variável de controlo do componente RigidBody do objeto de jogo que estamos a programar
    //Esta variável vai guardar uma referencia do componente a que queremos ter acesso
    private Rigidbody rb;
    void Start()
    {
          //inicializar a variável contador com valor.
         contador = 0;

        //Fisica é adicionada ao componente Rigidbody
        //Guardar a referencia do objeto Rigidbody (se este axistir)
        rb = GetComponent<Rigidbody>();

        //adicionar valor à variável contar
        Contador();

        //o texto de vitoria deve iniciar sem valor
        vitoria.text = "";
    }
    //0 método FixedUpdate é chamado antes de ser efetuado qualquer cálculo de física.
    //Física deve ser adicionada neste método
    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !running && !timeLimit && !finished)
        {
            startTime = Time.time;
            running = true;
        }

        if (timeLimit)
        {
            vitoria.text = " Ficou sem tempo! Pressione a tecla r para tentar novamente";
            running = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (running)
        {
            float t = Time.time - startTime;
            string min = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timer.text = min + ":" + seconds;

            //Variáveis que vão armazenar a posição do jogardor
            //A função GetAxis permite aceder ao eixo que queremos manipular

            float moverNaHorizontal = Input.GetAxis("Horizontal");
            float moverNaVertical = Input.GetAxis("Vertical");

            //RigidBody Vector3 guarda as coordenadas nos eixos. em 3D(x,y,z)
            //como não queremos movimento no eixo do y configuramos da seguinte forma 0.0f

            Vector3 movimento = new Vector3(moverNaHorizontal, 0.0f, moverNaVertical);

            //Método AddForce permite adicionar uma força ao objeto que neste caso é
            //movimento * velocidade

            rb.AddForce(movimento * velocidade);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                isGrounded = false;
                rb.AddForce(Vector3.up * jumpPower * Time.deltaTime, ForceMode.Impulse);
            }
            
            rb.AddForce(movimento * velocidade);



            if (min == "2")
            {
                timeLimit = true;
            }
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "chao")
        {
            isGrounded = true;
        }
    }

    //evento OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        //se o GameObject for um Coletável
        if (other.gameObject.CompareTag("Coletavel"))
        {
            //então desativa
            other.gameObject.SetActive(false);
            //quando a colisão é detetada então
            contador = contador + 1;

            //adicionar valor à variável contar
            Contador();
        }
        if (other.gameObject.CompareTag("Coletavel verde"))
        {
            //então desativa
            other.gameObject.SetActive(false);
            //quando a colisão é detetada então
            contador = contador + 2;

            //adicionar valor à variável contar
            Contador();
        }
        if (other.gameObject.CompareTag("Coletavel vermelho"))
        {
            //então desativa
            other.gameObject.SetActive(false);
            //quando a colisão é detetada então
            contador = contador + 5;

            //adicionar valor à variável contar
            Contador();
        }

    }

    //método contador à propriedade texto de contar
    //escreve Contador: contador
    void Contador()
    {
        contar.text = "Contador = " + contador.ToString();

        //se o contador for maior ou igual ao número de coletáveis
        //então executa
        if (contador>=65)
        {
            running = false;
            finished = true;
            vitoria.text = "Ganhou! Parabéns";
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}

