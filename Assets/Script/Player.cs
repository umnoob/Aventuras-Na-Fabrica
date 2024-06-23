using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Movimentação
    Rigidbody2D meuPlayer;
    public float DirecaoPlayer;
    public float VelocidadePlayer = 5;
    // Limite Mov
    public Transform LESQ;
    public Transform LDIR;
    // Pulo único
    public float ForcaPulo = 300;
    private bool Pulo;
    private bool Echao;
    // Pause
    public GameObject Pause;
    public bool Pausado = false;
    // Contagem canos
    public int NumCano;
    public Text TxtCano;
    // Pontuacao
    public int NumScore;
    public Text TxtScore;
    // Vida
    public int Vida = 3;
    public Text TxtVida;
    // Passar de fase
    public GameObject FaseCompleta;
    public bool Passou = false;
    public bool Passou2 = false;
    public bool Passou3 = false;
    public int CenaSeguinte = 0;
    // Derrota
    public GameObject GameOver;
    // Animacao
    Animator animado_player;

    // Start is called before the first frame update
    void Start()
    {
        animado_player = GetComponent<Animator>();
        // Chama o Rigidbody2D para o codigo
        meuPlayer = GetComponent<Rigidbody2D>();
        // Colocando a Vida na HUD
        TxtVida.text = Vida.ToString();
        // Chave Score
        NumScore = PlayerPrefs.GetInt("_ChaveScore");
        TxtScore.text = NumScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Chama Movimentação
        Movimentar();
        // Chama Limitador
        LimitaMovimento();
        // Chama Pulo
        Pular();
        // Chama Pause
        Pausar();
        // Chama Fim de jogo
        FimDeJogo();
    }
    void OnCollisionEnter2D(Collision2D coli)
    {
        // Pulo único
        if (coli.gameObject.tag == "Plataforma")
        {
            Echao = true;
        }
        // Perdendo vida
        if (coli.gameObject.tag == "Inimigo")
        {
            Vida--;
            TxtVida.text = Vida.ToString();
        }
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        // Coletar Canos
        if (coli.gameObject.tag == "Coletavel")
        {
            // Cano
            NumCano++;
            TxtCano.text = NumCano.ToString();
            // Pontos
            NumScore += 10;
            TxtScore.text = NumScore.ToString();
            Destroy(coli.gameObject);
            // Áudio
            // SomCano.Play();
            // Chaves para Gema e Score
            PlayerPrefs.SetInt("_ChaveScore", NumScore);
        }
        // Perder vida quando coleta o cano errado
        if (coli.gameObject.tag == "ItemErrado")
        {
            // Vida
            Vida --;
            TxtVida.text = Vida.ToString();
            // Pontos
            NumScore -= 10;
            TxtScore.text = NumScore.ToString();
            Destroy(coli.gameObject);
            // Áudio
            // SomCano.Play();
            // Chaves para Gema e Score
            PlayerPrefs.SetInt("_ChaveScore", NumScore);
        }
        // Local de entrega dos canos
        if (coli.gameObject.name == "Barril" && NumCano >= 5)
        {
           FaseCompleta.SetActive(true);
        }
        // Local onde passa para a proxima fase
        if(coli.gameObject.name == "Fase2")
        {
            CenaSeguinte ++;
            Passou = true;
        }
        // Local onde passa para a proxima fase
        if (coli.gameObject.name == "Fase3")
        {
            CenaSeguinte += 2;
            Passou = false;
            Passou2= true;
        }
        if (coli.gameObject.name == "Final")
        {
            CenaSeguinte += 3;
            Passou = false;
            Passou2 = false;
            Passou3 = true;
        }
    }
    void Movimentar()
    {
        // Usar A e D para movimentacao horizontal
        DirecaoPlayer = Input.GetAxis("Horizontal");
        // Movimeto
        meuPlayer.velocity = new Vector2(DirecaoPlayer * VelocidadePlayer, meuPlayer.velocity.y);
        if (DirecaoPlayer != 0)
        {
            animado_player.SetBool("Running",true);
        }
        else
        {
            animado_player.SetBool("Running", false);
        }
        if (Echao == true)
        {
            animado_player.SetBool("Jumping", false);
        }
    }
    void LimitaMovimento()
    {
        // Recebe X
        float posX = transform.position.x;
        // Impede de passar direita
        if (posX > LDIR.position.x)
        {
            transform.position = new Vector2(LDIR.position.x, transform.position.y);
        }
        // Impede de passar esquerda
        else if (posX < LESQ.position.x)
        {
            transform.position = new Vector2(LESQ.position.x, transform.position.y);
        }
    }
    void Pular()
    {
        // Usar Espaco para pular
        Pulo = Input.GetButtonDown("Jump");
        // Pulo unico e somente no chao
        if (Pulo && Echao)
        {
            meuPlayer.AddForce(new Vector2(0, ForcaPulo));
            Echao = false;
            animado_player.SetBool("Jumping", true);
        }
    }
    void Pausar()
    {
        // Usar tecla P para pausar
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pausado = true;
            if (Pausado == true)
            {
                // Pause ativado
                Pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    void FimDeJogo()
    {
        // Quando vida estiver zerada
        if (Vida == 0)
        {
            // Fim de jogo ativado
            GameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
