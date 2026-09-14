
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vida : MonoBehaviour
{
    public TextMeshProUGUI vidasText;
    public static int vidas;
    void Start()
    {
        vidas = 5;
        vidasText.text = "Vidas: " + vidas;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("traps"))
        {
            if (vidas > 1)
            {
                vidas --;
                vidasText.text = "Vidas: " + vidas;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
