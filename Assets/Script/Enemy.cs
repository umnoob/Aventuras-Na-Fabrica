using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Chama Rigidbody2D
    public Rigidbody2D GravidadeInimigo;
    // Pulo
    public float ForcaPulo = 150;
    private bool Echao;

    private bool IrESQ = true;
    private bool IrDIR = false;
    public float VelocidadeInimigo = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // So pula se for inimigo 1
        if (gameObject.name == "Inimigo")
        {
            Pular();
        }
        if (gameObject.name == "Inimigo 2")
        {
            Movimentar();
        }
    }
    void OnCollisionEnter2D(Collision2D coli)
    {
        // Pulo único
        if (coli.gameObject.tag == "Plataforma")
        {
            Echao = true;
        }
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (gameObject.name == "Inimigo 2")
        {
            if (coli.gameObject.name == "TrocaESQ")
            {
                IrDIR = true;
                IrESQ = false;
            }
            if (coli.gameObject.name == "TrocaDIR")
            {
                IrDIR = false;
                IrESQ = true;
            }
        }
    }
    void Pular()
    {
        // Pulo unico somente no chao
        if (Echao)
        {
            GravidadeInimigo.AddForce(new Vector2(0, ForcaPulo));
            Echao = false;
        }
    }
    void Movimentar()
    {
        if(IrESQ == true)
        {
            transform.position += Vector3.left * VelocidadeInimigo * Time.deltaTime;
            
        }
        if (IrDIR == true)
        {
            transform.position += Vector3.right * VelocidadeInimigo * Time.deltaTime;

        }
    }
}
