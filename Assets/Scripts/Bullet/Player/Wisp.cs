using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    float damage = 1F;
    public float ang = 0F;
    GameSystem GS;
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        transform.localPosition = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang)*0.8F);
    }
    // Update is called once per frame
    void Update()
    {
        ang += Time.deltaTime * 3F;
        transform.localPosition = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang) * 0.8F);
        if (GS.dayOrNight == GameSystem.DayOrNight.Day)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec)
                ec.Damage(damage);
        }

    }
}
