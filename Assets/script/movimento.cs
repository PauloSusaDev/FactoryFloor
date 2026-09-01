using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class movimento : MonoBehaviour
{
    public  float velocidade = 5f;
    public  float forcaPulo = 7f;
    private Rigidbody2D rb;
    private float moveHorizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        moveHorizontal = inputVector.x;

    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        }

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
    }


}
