using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : LTWEntity {
    private float score;
    // Use this for initialization
    public override void StatUp(float scale, float forceX, float forceY, float ttl)
    {
        base.StatUp(scale, forceX, forceY, ttl);
        this.score = forceY / scale;
    }
    protected override void Update()
    {
        base.Update();
        body.AddForce(Vector3.up * forceY + Vector3.right * (forceX * Mathf.Sin(liveTime * 2 * Mathf.PI)));
    }
    protected override void OnMouseDown()
    {
        GameController.Instance.IncrementScore(score);
        Collapse();
    }

    public override void Collapse()
    {
        GameController.Instance.Emmiter.InstantBubbleCollapce(this.transform.position, this.transform.localScale);
        Destroy(this.gameObject);
    }
}
