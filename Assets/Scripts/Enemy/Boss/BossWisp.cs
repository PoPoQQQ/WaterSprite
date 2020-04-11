using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossWisp : MonoBehaviour
{
    public enum State { Large, Mid, Small };
    public State state = State.Large;

    Vector2 endPos,startPos,basicPos;
    float movePhase = 0F,angle = 0F;
    float moveTime = 0F;

    int splitCnt = 0;
    public bool declining = false;

    GameObject player;
    GameObject beam,splitedWisp,haze;
    Coroutine moveCoroutine;
    EnemyController EC;
    Animator anim;

    void SetState(State s)
    {
        state = s;
        switch(s)
        {
            case State.Large:
                StartCoroutine(LargeMoveCoroutine());
                StartCoroutine(BeamCoroutine());
                break;
            case State.Mid:
                StartCoroutine(MidCoroutine());
                break;
            case State.Small:
                StartCoroutine(SmallCoroutine());
                break;
        }
    }

    int stateChangeCnt = 0;
    IEnumerator LargeMoveCoroutine()
    {
        startPos = transform.position;
        endPos = Vector2.down * 20;
        DOTween.To(() => movePhase, x => movePhase = x, 1, 3F);
        yield return new WaitForSeconds(3F);

        angle = Random.Range(-Mathf.PI, Mathf.PI);
        for(int i =1;i<=4;i++)
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
        stateChangeCnt++;
        if (stateChangeCnt <= 2)
            SetState(State.Mid);
        else
            SetState(State.Small);
    }


    void Beam()
    {
        Debug.Break();
        Vector2 vec = player.transform.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(vec.y, vec.x);
        float alpha = angle - 0.25F * Mathf.PI;
        GameObject.Instantiate(beam, transform.position + 0F*new Vector3(Mathf.Sin(alpha),Mathf.Cos(alpha)),
            Quaternion.Euler(0F, 0F, angle),transform);
        GameObject.Instantiate(beam, transform.position + 0F * new Vector3(Mathf.Sin(alpha + 0.667F*Mathf.PI), Mathf.Cos(alpha + 0.667F * Mathf.PI)),
            Quaternion.Euler(0F, 0F, angle - 120F), transform);
        GameObject.Instantiate(beam, transform.position + 0F * new Vector3(Mathf.Sin(alpha + 1.333F * Mathf.PI), Mathf.Cos(alpha + 1.333F * Mathf.PI)),
            Quaternion.Euler(0F, 0F, angle - 240F), transform);
    }
    IEnumerator BeamCoroutine()
    {
        yield return new WaitForSeconds(4F);
        while ((transform.position - player.transform.position).magnitude >= 7F)
            yield return 0;

        yield return new WaitForSeconds(Random.Range(6F, 7F));
        while (state == State.Large)
        {
            Beam();
            yield return new WaitForSeconds(Random.Range(6F, 7F));
        }
    }

    public void Split(float theta) => Split(theta, theta,false);
    public void Split(float theta,float alpha,bool outerRing)
    {
        //EC.Damage(2, true);
        if (state == State.Mid && declining)
            return;
        splitCnt++;
        var obj = GameObject.Instantiate(splitedWisp, transform.position, Quaternion.identity, transform);
        obj.GetComponent<SplitedWisp>().theta = theta;
        obj.GetComponent<SplitedWisp>().alpha = alpha;
        obj.GetComponent<SplitedWisp>().outerRing = outerRing;
    }

    IEnumerator MidCoroutine()
    {
        Debug.Log("m");
        anim.SetInteger("State", 2);
        splitCnt = 0;
        declining = false;

        for (int i =0;i<6;i++)
            Split(2F * i *Mathf.PI / 6F);
        yield return new WaitForSeconds(3F);

        angle = Random.Range(-Mathf.PI, Mathf.PI);
        while(splitCnt <= 12)
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
        anim.SetInteger("State", 1);
        SetState(State.Large);
    }


    IEnumerator SmallCoroutine()
    {
        Destroy(anim.gameObject, 2.5F);
        anim.SetInteger("State", 3);
        yield return new WaitForSeconds(2F);
        var obj = GameObject.Instantiate(haze, GameObject.Find("Camera Position").transform);
        Destroy(obj, 1F);
        for (int i =1;i<=3;i++)
        {
            Split(0F, 0F,false);
            Split(0.5F * Mathf.PI, 0F, true);
            Split(1F * Mathf.PI, 0F, false);
            Split(1.5F * Mathf.PI, 0F, true);
            yield return new WaitForSeconds(0.6F);
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(startPos, endPos, movePhase);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        beam = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/WispBeam");
        haze = Resources.Load<GameObject>("Prefabs/Enemies/WispHaze");
        anim = transform.Find("Sprite").GetComponent<Animator>();
        splitedWisp = Resources.Load<GameObject>("Prefabs/Enemies/SplitedWisp");
        EC = GetComponent<EnemyController>();
        SetState(State.Large);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
