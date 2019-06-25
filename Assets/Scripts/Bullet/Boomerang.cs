using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public Vector2 basicVec;
    Vector2 vec,compVec = Vector2.zero;
    float phase = 1F,compRate = 0F;
    bool comped = false;
    GameObject player;
    Rigidbody2D body;

    void Comp()
    {
        comped = true;
        compVec = player.transform.position - transform.position;
        compVec.Normalize();
        compVec -= basicVec * Vector2.Dot(basicVec, compVec);
        
    }

    private void FixedUpdate()
    {
        if (phase > -1.2F)
            phase -= Time.fixedDeltaTime*1F;

        if (phase <= 0.2F && comped == false)
            Comp();

        if (phase <= 0.2F && phase > -0.6F)
            compRate = (0.2F - phase) / 0.8F;
        if (phase <= -0.6F)
            compRate = 1F - 3F * (-0.6F - phase);

        body.velocity = basicVec * 17F * phase + compRate * compVec * 6F;

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
