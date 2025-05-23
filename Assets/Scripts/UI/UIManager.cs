
using UnityEngine;
using UnityEngine.UI;   

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    void Start()
    {
        GameManager.Instance.uiManager = this;
    }

    public void SetRetryButton()
    {
        retryButton.gameObject.SetActive(true);
    }
}
