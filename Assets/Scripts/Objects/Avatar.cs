using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    public Sprite currentSprite;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.sprite = sprites[PlayerPrefs.GetInt("avatar", 0)];
    }

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }

    public Sprite GetCurrentSprite()
    {
        return currentSprite;
    }

    public void SetSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
        currentSprite = sprites[index];
        PlayerPrefs.SetInt("avatar", index);
    }
}
