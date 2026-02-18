using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{

    public KeyCode keyCode = KeyCode.Z;

    [Header("Animation setup")]
    public Animator anim;
    public string triggerOpen = "Open";
    public string tagToCompare = "Player";

    [Header("Notification Setup")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;

    private float _startScale;
    private bool _chestOpened = false;

    void Start()
    {
        HideNotification();
        _startScale = notification.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;

        anim.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification();
        Invoke(nameof(ShowItem), .2f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            Player p = other.transform.GetComponent<Player>();

            if (p != null && _chestOpened == false)
            {
                ShowNotification();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            Player p = other.transform.GetComponent<Player>();

            if (p != null && _chestOpened == false)
            {
                HideNotification();
            }

        }
    }

    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.DOScale(0, tweenDuration).From();
    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }
}
