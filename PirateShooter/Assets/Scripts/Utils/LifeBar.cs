using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField]
    private Transform fillTransform;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color mediumColor = Color.yellow;
    [SerializeField]
    private Color dangerColor = Color.red;

    private Transform target = null;
    private float totalLife;

    public void Initialize(Transform target, float totalLife)
    {
        this.target = target;
        this.totalLife = totalLife;
        spriteRenderer.color = defaultColor;
    }

    public void SetLife(float currentLife)
    {
        float percentage = currentLife == 0 ? 0.01f : currentLife / totalLife;

        fillTransform.localScale = new Vector3(percentage, fillTransform.localScale.y, fillTransform.localScale.z);

        if (percentage > 0.6f)
            spriteRenderer.color = defaultColor;
        else if (percentage > 0.25f)
            spriteRenderer.color = mediumColor;
        else
            spriteRenderer.color = dangerColor;
    }

    public void DestroyBar()
    {
        Debug.Log("Destroy bar");
        Destroy(gameObject);
    }

    private void Update()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x - 0.5f, target.position.y + 0.8f, 0f);
    }
}
