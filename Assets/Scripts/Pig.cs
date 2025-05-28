<<<<<<< HEAD
ï»¿using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour
{
    public float Health = 150f;
    public Sprite SpriteShownWhenHurt;
    private float ChangeSpriteHealth;

    void Start()
    {
        ChangeSpriteHealth = Health - 30f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // ìƒˆì™€ ì¶©ëŒ ì‹œ ì¦‰ì‹œ ì œê±°
        if (col.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
        }
        else
        {
            // ì¶©ê²© ë°ë¯¸ì§€ ê³„ì‚°
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;

            // ì²´ë ¥ ë³€í™”ì— ë”°ë¼ ìŠ¤í”„ë¼ì´íŠ¸ ë³€ê²½
            if (Health < ChangeSpriteHealth)
            {
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }

            // ì²´ë ¥ì´ 0 ì´í•˜ì´ë©´ ì œê±°
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
=======

// Àû (µÅÁö)ÀÇ Ã¼·Â, Ãæµ¹ Ã³¸®, µ¥¹ÌÁö Ã³¸®, ½ºÇÁ¶óÀÌÆ® º¯°æ
using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour // MonoBehaviour »ó¼Ó
{
    // Àû (µÅÁö)ÀÇ Ã¼·Â ±âº» 150
    public float Health = 150f;
    // Ã¼·ÂÀÌ ÀÏÁ¤ ÀÌÇÏ·Î ¶³¾îÁ³À» ¶§ ¹Ù²ğ ½ºÇÁ¶óÀÌÆ®
    public Sprite SpriteShownWhenHurt;
    // ½ºÇÁ¶óÀÌÆ® ¹Ù²ğ Ã¼·Â°ª (±âÁØ)
    private float ChangeSpriteHealth;

    void Start() // ½ºÇÁ¶óÀÌÆ® ¹Ù²Ùµµ·Ï ±âÁØ ¼³Á¤
    {
        ChangeSpriteHealth = Health - 30f; // Ã¼·Â 30 ÁÙ¾îµé¸é ½ºÇÁ¶óÀÌÆ® º¯°æ
    }

    void OnCollisionEnter2D(Collision2D col)
    {	// ¿òÁ÷ÀÌÁö ¾Ê´Â ¿ÀºêÁ§Æ® Ãæµ¹ ¹«½Ã
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // »õ (ÅºÈ¯, ºÎ)°¡ ºÎµúÈù °æ¿ì
        if (col.gameObject.tag == "Bird")
        {	// ¿ÀºêÁ§Æ® Á¦°Å
            Destroy(gameObject);
        }
        else // »õ°¡ ¾Æ´Ñ ´Ù¸¥ ¿ÀºêÁ§Æ®¿¡ ºÎµúÈù °æ¿ì
        {
            // µ¥¹ÌÁö= ¼Óµµ * 10
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            // Ã¼·Â- µ¥¹ÌÁö
            Health -= damage;

            if (Health < ChangeSpriteHealth) // Ã¼·ÂÀÌ ½ºÇÁ¶óÀÌÆ® ¹Ù²î´Â ±âÁØ°ªº¸´Ù ³·À¸¸é
            {	// ½ºÇÁ¶óÀÌÆ® º¯°æ
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }
            // Ã¼·ÂÀÌ 0 ÀÌÇÏ°¡ µÇ¸é ¿ÀºêÁ§Æ® Á¦°Å
            if (Health <= 0) Destroy(this.gameObject);
        }
    }


}


>>>>>>> upstream/master
