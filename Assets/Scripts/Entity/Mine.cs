using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine: LTWEntity {

    public override void StatUp(float scale, float forceX, float forceY, float ttl)
    {
        base.StatUp(scale, forceX, forceY, ttl);
    }
    protected override void Update()
    {
        base.Update();
        body.AddForce(Vector3.up * forceY);
    }
    protected override void OnMouseDown()
    {
        GameController.Instance.DecrementLives();
        Collapse();
    }

    public override void Collapse()
    {
        GameController.Instance.Emmiter.InstantMineCollapce(this.transform.position, this.transform.localScale);
        Destroy(this.gameObject);
    }
}
