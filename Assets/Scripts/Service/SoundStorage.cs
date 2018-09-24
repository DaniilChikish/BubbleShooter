using UnityEngine;
/***
 * SoundStorage
 * Stored all Audio clips.
 * Access through the GameController
 * */
public class SoundStorage : MonoBehaviour {
    [SerializeField]
    AudioClip[] bubbleCollapceClip;
    [SerializeField]
    AudioClip[] mineCollapceClip;
    public AudioClip GetBubbleCollapce()
    {
        int index = Random.Range(0, bubbleCollapceClip.Length);
        return bubbleCollapceClip[index];
    }
    public AudioClip GetMineCollapce()
    {
        int index = Random.Range(0, mineCollapceClip.Length);
        return mineCollapceClip[index];
    }
}
