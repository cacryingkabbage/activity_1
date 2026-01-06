using UnityEngine;
using UnityEngine.SceneManagement;

public class Nogo : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public SpriteRenderer spriteRenderer;

    [Header("Distances")]
    public float warningDistance = 3f;
    public float deathDistance = 1.5f;

    [Header("Shake")]
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 20f;

    [Header("Colors")]
    public Color safeColor = Color.white;
    public Color warningColor = Color.red;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= deathDistance)
        {
            RestartScene();
        }
        else if (distance <= warningDistance)
        {
            WarningEffect(distance);
        }
        else
        {
            ResetZone();
        }
    }

    void WarningEffect(float distance)
    {
        float shakeStrength = Mathf.InverseLerp(warningDistance, deathDistance, distance);
        float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount * shakeStrength;
        float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount * shakeStrength;
        transform.position = startPosition + new Vector3(offsetX, offsetY, 0);

        // Color
        spriteRenderer.color = Color.Lerp(safeColor, warningColor, shakeStrength);
    }

    void ResetZone()
    {
        transform.position = startPosition;
        spriteRenderer.color = safeColor;
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
