using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayerAnimations : MonoBehaviour
{
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] jumpSprites;
    public Sprite[] attackSprites;
    public Sprite[] hurtSprites;
    public float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isHurt = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateIdle());
    }

    private IEnumerator AnimateIdle()
    {
        while (true)
        {
            spriteRenderer.sprite = idleSprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % idleSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void StartWalkAnimation()
    {
        StopAllCoroutines();
        currentSpriteIndex = 0;
        StartCoroutine(AnimateWalk());
    }

    private IEnumerator AnimateWalk()
    {
        while (true)
        {
            spriteRenderer.sprite = walkSprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % walkSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void StartJumpAnimation()
    {
        StopAllCoroutines();
        currentSpriteIndex = 0;
        isJumping = true;
        StartCoroutine(AnimateJump());
    }

    private IEnumerator AnimateJump()
    {
        while (isJumping)
        {
            spriteRenderer.sprite = jumpSprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % jumpSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void EndJumpAnimation()
    {
        isJumping = false;
        StartCoroutine(AnimateIdle());
    }

    public void StartAttackAnimation()
    {
        StopAllCoroutines();
        currentSpriteIndex = 0;
        isAttacking = true;
        StartCoroutine(AnimateAttack());
    }

    private IEnumerator AnimateAttack()
    {
        while (isAttacking)
        {
            spriteRenderer.sprite = attackSprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % attackSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void EndAttackAnimation()
    {
        isAttacking = false;
        StartCoroutine(AnimateIdle());
    }

    public void StartHurtAnimation()
    {
        StopAllCoroutines();
        currentSpriteIndex = 0;
        isHurt = true;
        StartCoroutine(AnimateHurt());
    }

    private IEnumerator AnimateHurt()
    {
        while (isHurt)
        {
            spriteRenderer.sprite = hurtSprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % hurtSprites.Length;
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    public void EndHurtAnimation()
    {
        isHurt = false;
        StartCoroutine(AnimateIdle());
    }

}
