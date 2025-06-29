using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingCredits : MonoBehaviour
{
    public RectTransform panelTransform;
    public float moveDuration = 11f;

    void Start()
    {
        // Calcula la posición final: su posición inicial + la altura del panel + altura de pantalla
        float startY = panelTransform.anchoredPosition.y;
        float panelHeight = panelTransform.rect.height;
        float screenHeight = ((RectTransform)panelTransform.parent).rect.height;
        float endY = startY + panelHeight + screenHeight;

        StartCoroutine(RollCredits(new Vector2(panelTransform.anchoredPosition.x, endY)));
    }

    IEnumerator RollCredits(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 initialPosition = panelTransform.anchoredPosition;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            panelTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que termine exactamente en target
        panelTransform.anchoredPosition = targetPosition;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
