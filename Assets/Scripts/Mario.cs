using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject textHolder;
    public TextMeshProUGUI customText;
     

    public HeadType chosenHeadType;
    public BodyType chosenBodyType;
    public HandsType chosenHandsType;
    public FeetType chosenFeetType;

    public GameObject skeletonSprite;
    public Sprite emptyText;
    public Sprite text1;
    public Sprite text2;
    public Sprite text3;
    public Sprite text4;
    public Sprite text5;

    bool starMessageTriggered = false;

    bool hatRemoved = false;
    bool bodyRemoved = false;
    bool feetRemoved = false;

    int flipCounter;
    bool flipped = false;
    public bool failTriggered = false;
 
    private void Update()
    {
        if (gameObject.transform.localPosition.x > 1.2f && gameObject.transform.localPosition.x < 1.8f)
            if (gameObject.transform.localPosition.y > 1.9f)
                if (starMessageTriggered == false)
                {
                    starMessageTriggered = true;
                    SetText2();
                }

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

    // LAZY MODE TEXTS

        public void ClearText()
    { 
        customText.gameObject.SetActive(false);
        textHolder.SetActive(false);
    }
        public void SetCustomText(string str)
    {
        customText.gameObject.SetActive(true);
        textHolder.SetActive(true);
        customText.text = str;
        textHolder.GetComponent<SpriteRenderer>().sprite = emptyText; 
    }
    public void SetText1()
    {
        textHolder.SetActive(true);
        textHolder.GetComponent<SpriteRenderer>().sprite = text1;
    }
    public void SetText2()
    {
        textHolder.SetActive(true);
        textHolder.GetComponent<SpriteRenderer>().sprite = text2;
        StartCoroutine(ClearTextAfterAWhile());
    }
    public void SetText3()
    {
        textHolder.SetActive(true);
        textHolder.GetComponent<SpriteRenderer>().sprite = text3;
        StartCoroutine(ClearTextAfterAWhile());
    }
    public void SetText4()
    {
        textHolder.SetActive(true);
        textHolder.GetComponent<SpriteRenderer>().sprite = text4; 
        StartCoroutine(ClearTextAfterAWhile());
    }
    public void SetText5()
    {
        textHolder.SetActive(true);
        textHolder.GetComponent<SpriteRenderer>().sprite = text4;
        StartCoroutine(ClearTextAfterAWhile());
    }
    IEnumerator ClearTextAfterAWhile()
    { 
        for (float t = 0.0f; t < 6f; t += Time.deltaTime * 2)
            yield return null;
        ClearText();
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
        float runDistance = 6f;
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

      ClearText();

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
            if (t > jumpHeight / 3 && feetRemoved == false)
                RemoveFeet();
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
            if (t > jumpHeight / 3 && hatRemoved == false)
                RemoveBody();
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
        if (hatRemoved == false)
             RemoveHat();
        MarioHasSpinned(); 
    }

    void RemoveHat()
    {
        hatRemoved = true;
        StartCoroutine(FlyHat());
    }
    void RemoveBody()
    { 
        bodyRemoved = true;
        StartCoroutine(FlyBody());
    }
    void RemoveFeet()
    { 
        feetRemoved = true;
        StartCoroutine(FlyFeet());
    }


    IEnumerator FlyHat()
    {
        float runDistance = 36f;
        float runSpeed = 2.1f; 

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        { 
            headSprite.gameObject.transform.position += Vector3.left * runSpeed * Time.deltaTime;
            headSprite.gameObject.transform.position += Vector3.up * runSpeed * Time.deltaTime;
            yield return null;
        }
        headSprite.gameObject.SetActive(false); 
    }
    IEnumerator FlyBody()
    {
        float runDistance =36f;
        float runSpeed = 1.9f;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            bodySprite.gameObject.transform.position += Vector3.right * runSpeed * Time.deltaTime;
            bodySprite.gameObject.transform.position += Vector3.up * runSpeed * Time.deltaTime;
            yield return null;
        }
        bodySprite.gameObject.SetActive(false);
    }
    IEnumerator FlyFeet()
    {
        float runDistance = 36f;
        float runSpeed = 2.0f;

        for (float t = 0.0f; t < runDistance; t += Time.deltaTime)
        {
            feetSprite.gameObject.transform.position += Vector3.left * runSpeed * Time.deltaTime;
            feetSprite.gameObject.transform.position += Vector3.down * runSpeed * Time.deltaTime / 2;
            yield return null;
        }
        feetSprite.gameObject.SetActive(false);
    }
}
