using Assets.Scripts;
using UnityEngine;

public class BubbleEmmiter : MonoBehaviour {

    [SerializeField]
    private GameObject bubblePrefab;
    [SerializeField]
    private GameObject collapcePrefab;
    [SerializeField]
    private GameObject minePrefab;
    [SerializeField]
    private GameObject blastPrefab;

    [Tooltip("Emiter consumes material on each shot")]
    [SerializeField]
    private float materialCount;
    public float Materials { get { return materialCount; } }

    [Tooltip("Shots per minute")]
    [SerializeField]
    private float fireRate;
    [Tooltip("The maximum number of entities at a time")]
    [SerializeField]
    private float maxAtOnce;
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float emitArea;

    [SerializeField]
    [Range(0, 100)]
    private float bubbleChance;
    [SerializeField]
    [Range(0, 100)]
    private float mineChance;

    [SerializeField]
    private float minforceX;
    [SerializeField]
    private float maxforceX;

    [SerializeField]
    private float minforceY;
    [SerializeField]
    private float maxforceY;

    private const float materialConsumpt = 1f;
    private float nextShotTimer;
    void Start () {
		
	}
    public void Initialize(float materialCount, float firerate, float minForce, float maxForce, float bubbleChance, float mineChance)
    {
        this.materialCount = materialCount;
        this.fireRate = firerate;
        this.minforceY = minForce;
        this.maxforceY = maxForce;
        this.bubbleChance = bubbleChance;
        this.mineChance = mineChance;
    }
    public void Initialize(float materialCount, float difficultFactor, float bubbleChance, float mineChance)
    {
        this.materialCount = materialCount;
        this.fireRate = fireRate * (1 + difficultFactor / 2);
        this.minforceY = minforceY * (1 + difficultFactor / 2);
        this.maxforceY = maxforceY * (1 + difficultFactor / 2);
        this.bubbleChance = bubbleChance;
        this.mineChance = mineChance;
    }
    // Update is called once per frame
    void Update()
    {
        if (materialCount > 0)
            if (nextShotTimer <= 0 && this.transform.childCount < maxAtOnce)
                TryShot();
            else
                nextShotTimer -= Time.deltaTime;
    }
    public void TryShot()
    {
        nextShotTimer = 60f / fireRate;
        float chanse = UnityEngine.Random.Range(0, 100);
        if (chanse < bubbleChance)
            Shot(bubblePrefab);
        if (chanse < mineChance)
            Shot(minePrefab);
    }
    public void InstantBubbleCollapce(Vector3 position, Vector3 scale)
    {
        GameObject collapce = Instantiate(collapcePrefab, position, Quaternion.identity);
        collapce.transform.localScale = scale;
        collapce.GetComponent<AudioSource>().clip = GameController.Instance.Sound.GetBubbleCollapce();
        collapce.GetComponent<AudioSource>().volume = GameController.SoundLevel;
        collapce.GetComponent<AudioSource>().Play();
    }
    public void InstantMineCollapce(Vector3 position, Vector3 scale)
    {
        GameObject collapce = Instantiate(blastPrefab, position, Quaternion.identity);
        collapce.transform.localScale = scale;
        collapce.GetComponent<AudioSource>().clip = GameController.Instance.Sound.GetMineCollapce();
        collapce.GetComponent<AudioSource>().volume = GameController.SoundLevel;
        collapce.GetComponent<AudioSource>().Play();
    }
    private void Shot(GameObject prefab)
    {
        Vector3 position = SelectPosition();
        float scale = Random.Range(minScale, maxScale);

        float forceX = Random.Range(minforceX, maxforceX);
        float forceY = Random.Range(minforceY, minforceY);

        GameObject newBubble = Instantiate(prefab, position, Quaternion.identity, this.transform);
        newBubble.GetComponent<LTWEntity>().StatUp(scale, forceX, forceY, scale * 10);

        materialCount -= scale * materialConsumpt;
    }

    private Vector3 SelectPosition()
    {
        Vector3 outp = transform.position;
        float Xcomp = Random.Range(-emitArea, emitArea);
        outp.x += Xcomp;
        return outp;
    }
}
