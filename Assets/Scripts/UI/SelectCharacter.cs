using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public Canvas SelectCharacterPanel;
    //public Button SelectCharacterButton;
    public ScrollRect scrollRect;
    public Button leftArrow;
    public Button rightArrow;

    public GameObject panelPrefab;
    public RectTransform content;

    public Sprite[] CharacterImages;


    // �г� ���� ����
    private int totalPanels = 4;
    private int currentIndex = 0;
    private float panelWidth = 380f;
    private float panelHeight = 430f;

    // ��ũ�� ���� ����
    public float scrollSpeed = 0.2f;
    private bool isScrolling = false;
    private float scrollLerpTime = 0f;
    private float targetPosition = 0f;
    private float startPosition = 0f;

    // Start �޼��� �ʱ�ȭ
    void Start()
    {
        totalPanels = CharacterImages.Length;

        leftArrow.onClick.AddListener(SlideLeft);
        rightArrow.onClick.AddListener(SlideRight);


        SetPanelCount(totalPanels);
        UpdateArrowButtons();
    }

    void Update()
    {
        // �ε巯�� ��ũ�� ó��
        if (isScrolling)
        {
            scrollLerpTime += Time.deltaTime / 0.2f;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, scrollLerpTime);
            if (scrollLerpTime >= 1f)
            {
                scrollRect.horizontalNormalizedPosition = targetPosition;
                isScrolling = false;
                UpdateArrowButtons();
            }
        }
    }

    public void SetPanelCount(int count)
    {
        float contentWidth = count * panelWidth;
        content.sizeDelta = new Vector2(contentWidth, panelHeight);

        // ���� �ڽ� ������Ʈ ����
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // ���ο� �г� ����
        for (int i = 0; i < CharacterImages.Length; i++)
        {
            GameObject newPanel = Instantiate(panelPrefab, content);

            RectTransform panelRectTransform = newPanel.GetComponent<RectTransform>();
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            panelRectTransform.localPosition = new Vector3(i * panelWidth, 0, 0);

            Transform imageTransform = newPanel.transform.Find("Image");
            Image img = imageTransform.GetComponent<Image>();
            img.rectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            img.sprite = CharacterImages[i];


            // ��ư Ŭ�� �� TitleSceneManager�� ĳ���� �ε����� ����
            Button imgButton = img.GetComponent<Button>();
            int characterIndex = i; 
            imgButton.onClick.AddListener(() => OnCharacterPanelSelected(characterIndex));
        }

        scrollRect.horizontalNormalizedPosition = 0;
        UpdateArrowButtons();
    }

    private void OnCharacterPanelSelected(int characterIndex)
    {

        // ���� ���� �Ŵ��� �������� (TitleSceneManager �Ǵ� GameSceneManager)
        SceneManager currentSceneManager = FindObjectOfType<SceneManager>();
        if (currentSceneManager!=null)
        {
           currentSceneManager.OnCharacterSelected(characterIndex);
        }
        SelectCharacterPanel.gameObject.SetActive(false);  // ĳ���� ���� �г� �����
    }

    void SlideLeft()
    {
        if (!isScrolling && currentIndex > 0)
        {
            currentIndex-=2;
            StartSmoothScroll(currentIndex);
        }
    }

    void SlideRight()
    {
        if (!isScrolling && currentIndex < totalPanels - 1)
        {
            currentIndex+=2;
            StartSmoothScroll(currentIndex);
        }
    }

    void StartSmoothScroll(int index)
    {
        isScrolling = true;
        scrollLerpTime = 0f;
        startPosition = scrollRect.horizontalNormalizedPosition;
        targetPosition = (float)index / (totalPanels - 2);
    }

    void UpdateArrowButtons()
    {
        leftArrow.interactable = currentIndex > 0;
        rightArrow.interactable = currentIndex < totalPanels - 1;
    }
}
