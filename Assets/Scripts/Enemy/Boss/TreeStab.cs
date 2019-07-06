using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStab : MonoBehaviour
{
    IEnumerator StabCoroutine()
    {
        yield return new WaitForSeconds(0.8F);

        GetComponent<SpriteRenderer>().color = Color.magenta;
        var cc = gameObject.AddComponent<CapsuleCollider2D>();
        cc.isTrigger = true;
        cc.direction = CapsuleDirection2D.Horizontal;
        cc.size = new Vector2(0.8F, 0.7F);

        yield return new WaitForSeconds(1F);

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StabCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<Player>();
            p.Damage(8F);
        }
    }
}
