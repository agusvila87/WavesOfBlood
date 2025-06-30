using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destruir la bala despu�s de un tiempo
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Aqu� puedes agregar l�gica para lo que sucede cuando la bala golpea algo
        Destroy(gameObject);
    }
}
