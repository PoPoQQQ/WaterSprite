using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossWisp : MonoBehaviour
{
    public enum State { large,mid,small};
    public State state = State.large;

    Vector2 endPos,startPos,basicPos;
    float movePhase = 0F,angle = 0F;
    float moveTime = 0F;

    int splitCnt = 0;
    public bool declining = false;

    GameObject player;
    GameObject beam,splitedWisp;
    Coroutine moveCoroutine;
    public Sprite largeSprite, midSprite;

    void SetState(State s)
    {
        state = s;
        switch(s)
        {
            case State.large:
                StartCoroutine(LargeMoveCoroutine());
                StartCoroutine(BeamCoroutine());
                break;
            case State.mid:
                StartCoroutine(MidCoroutine());
                break;
        }
    }


    IEnumerator LargeMoveCoroutine()
    {
        GetComponent<SpriteRenderer>().sprite = largeSprite;
        startPos = transform.position;
        endPos = Vector2.down * 20;
        DOTween.To(() => movePhase, x => movePhase = x, 1, 3F);
        yield return new WaitForSeconds(3F);

        angle = Random.Range(-Mathf.PI, Mathf.PI);
        for(int i =1;i<=8;i++)
        {
            startPos = transform.position;
            angle += Random.Range(0.5F, 1F)*Mathf.PI;
            endPos = (Vector2)player.transform.position 
                + Random.Range(3F, 5F) * new Vector2(Mathf.Cos(angle) * 1.2F, Mathf.Sin(angle));
            movePhase = 0F;
            moveTime = (startPos - endPos).magnitude * 0.3F + 0.3F;
            DOTween.To(() => movePhase, x => movePhase = x, 1,moveTime);
            yield return new WaitForSeconds(moveTime);
        }
        SetState(State.mid);
    }


    void Beam()
    {
        Vector2 vec = player.transform.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(vec.y, vec.x);
        GameObject.Instantiate(beam, transform.position, Quaternion.Euler(0F, 0F, angle),transform);
        GameObject.Instantiate(beam, transform.position, Quaternion.Euler(0F, 0F, angle + 120F), transform);
        GameObject.Instantiate(beam, transform.position, Quaternion.Euler(0F, 0F, angle + 240F), transform);
    }
    IEnumerator BeamCoroutine()
    {
        yield return new WaitForSeconds(4F);
        while ((transform.position - player.transform.position).magnitude >= 7F)
            yield return 0;

        yield return new WaitForSeconds(Random.Range(6F, 7F));
        while (state == State.large)
        {
            Beam();
            yield return new WaitForSeconds(Random.Range(6F, 7F));
        }
    }

    void Split(float theta)
    {
        if (state != State.mid || declining)
            return;
        splitCnt++;
        var obj = GameObject.Instantiate(splitedWisp, transform.position, Quaternion.identity, transform);
        obj.GetComponent<SplitedWisp>().basicTheta = theta;
        obj.GetComponent<SplitedWisp>().basicAlpha = theta;
    }

    IEnumerator MidCoroutine()
    {
        GetComponent<SpriteRenderer>().sprite = midSprite;
        splitCnt = 0;
        declining = false;

        for (int i =0;i<6;i++)
            Split(2F * i *Mathf.PI / 6F);
        yield return new WaitForSeconds(3F);

        angle = Random.Range(-Mathf.PI, Mathf.PI);
        while(splitCnt <= 18)
        {
            startPos = transform.position;
            angle += Random.Range(0.5F, 1F) * Mathf.PI;
            endPos = (Vector2)player.transform.position
                + Random.Range(3F, 5F) * new Vector2(Mathf.Cos(angle) * 1.2F, Mathf.Sin(angle));
            movePhase = 0F;
            moveTime = (startPos - endPos).magnitude * 0.3F + 0.6F;
            DOTween.To(() => movePhase, x => movePhase = x, 1, moveTime);
            yield return new WaitForSeconds(moveTime);
        }
        declining = true;
        yield return new WaitForSeconds(4F);
        SetState(State.large);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(startPos, endPos, movePhase);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        beam = Resources.Load<GameObject>("Prefabs/Ammo/WispBeam");
        splitedWisp = Resources.Load<GameObject>("Prefabs/Enemies/SplitedWisp");
        SetState(State.large);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
