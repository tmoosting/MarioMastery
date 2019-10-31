using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
   
    public enum HeadType { Head1, Head2, Head3, Head4  }
    public enum BodyType { Body1, Body2, Body3, Body4  }
    public enum HandsType { Hands1, Hands2, Hands3, Hands4  }
    public enum FeetType { Feet1, Feet2, Feet3, Feet4  }

    public Animation anim;

    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public SpriteRenderer feetSprite;

     

    public HeadType chosenHeadType;
    public BodyType chosenBodyType;
    public HandsType chosenHandsType;
    public FeetType chosenFeetType;

    public GameObject skeletonSprite;

    int flipCounter;
    bool flipped = false;
    public bool failTriggered = false;
 
    private void Update()
    { 
        if (gameObject.transform.localPosition.x > 2.9f && gameObject.transform.localPosition.y > 2.71f)
            if (failTriggered == false)
            {
                failTriggered = true;
                MarioController.Instance.MarioHasFailedJump();
            } 
       
        // fix for fall through floor after failed jump
        if ( gameObject.transform.localPosition.y < 0.8f) 
            gameObject.transform.localPosition  = new Vector3(gameObject.transform.localPosition.x, 1f, 0); 

        // fix for stutter on ground level
        if (gameObject.transform.localPosition.y < 1.023633f) 
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 1.021633f, 0); 
    }

    // --------------------------- WALK OFF

    public void MarioWalksOff()
    {
        StartCoroutine(WalkOff());
    }
    void MarioHasWalkedOff()
    {
        GameController.Instance.SetGameState(GameController.GameState.EndScreen);
    }
    IEnumerator WalkOff()
    {
        float runDistance = 4.5f;
        float runSpeed =  0.7f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        MarioHasWalkedOff();
    }

    // --------------------------- FREAKOUT

    public void MarioFreaks()
    {
        StartCoroutine(VeryFirstLeft());
    }
    void MarioDoneFreaking()
    {
        GameController.Instance.SetGameState(GameController.GameState.DoneFreaking);
    }

    // TODO: Combine all below into one adjustable coroutine
    IEnumerator VeryFirstLeft()
    {
        float runDistance = 0.3f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 2f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FirstRight());
    }

    IEnumerator FirstRight()
    {
        float runDistance = 0.3f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.5f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        { 
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FirstLeft());  
    }
    IEnumerator FirstLeft()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.3f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SecondRight());
    }
    IEnumerator SecondRight()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.1f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SecondLeft());
    }
    IEnumerator SecondLeft()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.2f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ThirdRight());
    }
    IEnumerator ThirdRight()
    {
        float runDistance = 0.35f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1.1f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ThirdLeft()); 
    }
    IEnumerator ThirdLeft()
    {
        float runDistance = 0.2f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed *1.1f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
            marioTransform.position += Vector3.left * runSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FourthRight());
    }
    IEnumerator FourthRight()
    {
        float runDistance = 0.25f;
        float runSpeed = MarioController.Instance.FreakoutBaseSpeed * 1f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
            marioTransform.position += Vector3.right * runSpeed * Time.deltaTime;
            yield return null;
        }
        MarioDoneFreaking();
    }
    // --------------------------- SPINJUMP

    public void MarioSpinsAndJumps()
    {
        StartCoroutine(StartCoroutineAfterSeconds(SpinJump(), MarioController.Instance.PreSpinPauseTime));
    }
    void MarioHasJumped()
    {
        StartCoroutine(FinishSpin());
    }
    void MarioHasSpinned()
    {
        GameController.Instance.SetGameState(GameController.GameState.ClothesDropped);
    }
    IEnumerator StartCoroutineAfterSeconds(IEnumerator routine, float seconds)
    {
        for (float t = 0.0f; t < seconds; t += Time.deltaTime * 2)
        {
            yield return null;
        }
        StartCoroutine(routine);
    }
    IEnumerator SpinJump()
    {
        float jumpHeight = 0.8f;
        float jumpSpeed = 2f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;

        for (float t = 0.0f; t < jumpHeight; t += Time.deltaTime)
        {
            flipCounter++;
            if (flipCounter > 3)
            {
                flipCounter = 0;
                flipped = !flipped;
            }
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            marioTransform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            yield return null;
        }
        MarioHasJumped();
    }
    IEnumerator FinishSpin()
    {
        float jumpHeight = 0.3f;
        float jumpSpeed = 5f;
        Mario mario = MarioController.Instance.misterMario;
        Transform marioTransform = mario.gameObject.transform;
        for (float t = 0.0f; t < jumpHeight; t += Time.deltaTime)
        {
            flipCounter++;
            if (flipCounter > 3)
            {
                flipCounter = 0;
                flipped = !flipped;
            }
            mario.skeletonSprite.GetComponent<SpriteRenderer>().flipX = flipped;
            marioTransform.position += Vector3.down * jumpSpeed * Time.deltaTime;
            yield return null;

        }
        MarioHasSpinned();
    }
}
