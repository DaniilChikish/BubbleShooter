using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lighter Than Water
public abstract class LTWEntity : MonoBehaviour {
    [SerializeField]
    protected float forceX;
    [SerializeField]
    protected float forceY;
    protected Rigidbody body;

    protected float liveTime;
    protected float timeToLive;

    private const float density = 1;
    private const float friction = 1;

    void Start()
    {
    }


    // Use this for initialization
    protected void OnEnable()
    {
        //timeToLive = 100;
        body = this.gameObject.GetComponent<Rigidbody>();
    }
    public virtual void StatUp(float scale, float forceX, float forceY, float ttl)
    {
        this.gameObject.transform.localScale = Vector3.one * scale;
        body.drag = friction;
        body.mass = Volume(scale) * density;
        timeToLive = ttl;
        this.forceX = forceX * Mathf.Sqrt(Volume(scale));
        this.forceY = forceY * Mathf.Sqrt(Volume(scale));
    }
    protected float Volume(float rad)
    {
        return 3f / 4f * Mathf.PI * Mathf.Pow(rad, 3);
    }
    protected virtual void Update()
    {
        liveTime += Time.deltaTime;
        if (liveTime > timeToLive)
            Collapse();
    }
    protected abstract void OnMouseDown();

    public abstract void Collapse();
}
