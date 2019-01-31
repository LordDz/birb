using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteAnim : MonoBehaviour
{
	public Sprite[] sprites;
	private SpriteRenderer sprite;
	private int spriteIndex;
	public int fps = 12;
	
	void Start()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
    void Update()
    {
		int previousSpriteIndex = spriteIndex;
        spriteIndex = (int) Mathf.Floor((fps*Time.time) % sprites.Length);
		sprite.sprite = sprites[spriteIndex];
    }
}
