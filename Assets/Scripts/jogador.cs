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

    //Vari�vel contador
    private int contador = 0;

    //vari�vel para suporte do Contador
    public Text contar;

    //vari�vel para suportar o texto de vit�ria
    public Text vitoria;

    //Mais info da class Input: https://docs.unity3d.com/Script Reference/Input.html
    //Mais info da class Rigidbody: https://docs.unity3d.com/Script Reference/Rigidbody.html
    //Mais info estrutura Vector3: https://docs.unity3d.com/Script Reference/Vector3.html

    //A vari�vel � declarada como p�blica (vari�veis declaradas como p�blicas s�o adicionadas como propriedades do componente)
    //quando adicionamos vari�veis como publicas podemos fazer altera��es diretamente no editor
    //Vari�vel de controlo de velocidade � adicionada como propriedade do componente (script)
    public float velocidade;
    //Vari�vel de controlo do componente RigidBody do objeto de jogo que estamos a programar
    //Esta vari�vel vai guardar uma referencia do componente a que queremos ter acesso
    private Rigidbody rb;
    void Start()
    {
          //inicializar a vari�vel contador com valor.
         contador = 0;

        //Fisica � adicionada ao componente Rigidbody
        //Guardar a referencia do objeto Rigidbody (se este axistir)
        rb = GetComponent<Rigidbody>();

        //adicionar valor � vari�vel contar
        Contador();

        //o texto de vitoria deve iniciar sem valor
        vitoria.text = "";
    }
    //0 m�todo FixedUpdate � chamado antes de ser efetuado qualquer c�lculo de f�sica.
    //F�sica deve ser adicionada neste m�todo
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

            //Vari�veis que v�o armazenar a posi��o do jogardor
            //A fun��o GetAxis permite aceder ao eixo que queremos manipular

            float moverNaHorizontal = Input.GetAxis("Horizontal");
            float moverNaVertical = Input.GetAxis("Vertical");

            //RigidBody Vector3 guarda as coordenadas nos eixos. em 3D(x,y,z)
            //como n�o queremos movimento no eixo do y configuramos da seguinte forma 0.0f

            Vector3 movimento = new Vector3(moverNaHorizontal, 0.0f, moverNaVertical);

            //M�todo AddForce permite adicionar uma for�a ao objeto que neste caso �
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
        //se o GameObject for um Colet�vel
        if (other.gameObject.CompareTag("Coletavel"))
        {
            //ent�o desativa
            other.gameObject.SetActive(false);
            //quando a colis�o � detetada ent�o
            contador = contador + 1;

            //adicionar valor � vari�vel contar
            Contador();
        }
        if (other.gameObject.CompareTag("Coletavel verde"))
        {
            //ent�o desativa
            other.gameObject.SetActive(false);
            //quando a colis�o � detetada ent�o
            contador = contador + 2;

            //adicionar valor � vari�vel contar
            Contador();
        }
        if (other.gameObject.CompareTag("Coletavel vermelho"))
        {
            //ent�o desativa
            other.gameObject.SetActive(false);
            //quando a colis�o � detetada ent�o
            contador = contador + 5;

            //adicionar valor � vari�vel contar
            Contador();
        }

    }

    //m�todo contador � propriedade texto de contar
    //escreve Contador: contador
    void Contador()
    {
        contar.text = "Contador = " + contador.ToString();

        //se o contador for maior ou igual ao n�mero de colet�veis
        //ent�o executa
        if (contador>=65)
        {
            running = false;
            finished = true;
            vitoria.text = "Ganhou! Parab�ns";
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}

