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


    // 패널 관련 변수
    private int totalPanels = 4;
    private int currentIndex = 0;
    private float panelWidth = 380f;
    private float panelHeight = 430f;

    // 스크롤 관련 변수
    public float scrollSpeed = 0.2f;
    private bool isScrolling = false;
    private float scrollLerpTime = 0f;
    private float targetPosition = 0f;
    private float startPosition = 0f;

    // Start 메서드 초기화
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
        // 부드러운 스크롤 처리
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

        // 기존 자식 오브젝트 삭제
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 새로운 패널 생성
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


            // 버튼 클릭 시 TitleSceneManager에 캐릭터 인덱스를 전달
            Button imgButton = img.GetComponent<Button>();
            int characterIndex = i; 
            imgButton.onClick.AddListener(() => OnCharacterPanelSelected(characterIndex));
        }

        scrollRect.horizontalNormalizedPosition = 0;
        UpdateArrowButtons();
    }

    private void OnCharacterPanelSelected(int characterIndex)
    {

        // 현재 씬의 매니저 가져오기 (TitleSceneManager 또는 GameSceneManager)
        SceneManager currentSceneManager = FindObjectOfType<SceneManager>();
        if (currentSceneManager!=null)
        {
           currentSceneManager.OnCharacterSelected(characterIndex);
        }
        SelectCharacterPanel.gameObject.SetActive(false);  // 캐릭터 선택 패널 숨기기
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
