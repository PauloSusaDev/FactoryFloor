using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class movimento : MonoBehaviour
{
    public  float velocidade = 5f;
    public  float forcaPulo = 7f;
    private float moveHorizontal;
    private bool estaNoChao;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        moveHorizontal = inputVector.x;

    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            estaNoChao = false;
        }

    }
    private void Update()
    {
        // Considera no chão se a velocidade vertical estiver próxima de zero
        estaNoChao = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        // Atualiza as variáveis do Animator (garanta que o nome seja exatamente idêntico)
        animator.SetBool("isGrounded", estaNoChao);

        animator.SetBool("isWalking", Mathf.Abs(moveHorizontal) > 0.1f);

        // Inverte a direção do Sprite
        InverterSprite();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveHorizontal * velocidade, rb.linearVelocity.y);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Limite"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.CompareTag("Portal"))
        {
            Vector2 portal = new Vector2(-36f, -96f);
            rb.transform.position = portal;
        }
        if (collision.gameObject.CompareTag("PortalFinal"))
        {
            Vector2 PortalFinal = new Vector2(-87f, 6f);
            rb.transform.position = PortalFinal;
            rb.gravityScale = 0.1f;
        }
        if (collision.gameObject.CompareTag("Chao"))
        {
            rb.gravityScale = 1f;
        }
    }
    private void InverterSprite()
    {
        if (moveHorizontal > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
  

}
