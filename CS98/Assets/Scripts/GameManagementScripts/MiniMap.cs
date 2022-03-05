using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class MiniMap : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject playerIcon;
    public GameObject player;
    // Start is called before the first frame update

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;

    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    private Image pointerImage;

    private string currentQuest = "";        //the current quest we are on
    private GameObject NPC;  
    private string NPCName;              //name of our NPC

    private void UpdateQuest()
    {
        currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
            NPCName = questDetails[0];
            print(NPCName);
            NPC = GameObject.Find(NPCName);
        }
    }
    private void Awake() {
        UpdateQuest();
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        // pointerImage = transform.Find("Pointer").GetComponent<Image>();
        if (NPC) {
            targetPosition = NPC.transform.position;
        } else {
            targetPosition = new Vector3(1,1);
        }
        print(targetPosition);
    }

    void Start(){
        PlayerCamera = GameObject.Find("CameraPlayer/ProtagonistCamera");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = PlayerCamera.transform.position;
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        playerIcon.transform.position = new Vector3(x, y-1, player.transform.position.z);

        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        // float borderSize = 100f;
        // Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        // bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        // if (isOffScreen) {
        //     RotatePointerTowardsTargetPosition();

        //     pointerImage.sprite = arrowSprite;
        //     Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        //     if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
        //     if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
        //     if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
        //     if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

        //     Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        //     pointerRectTransform.position = pointerWorldPosition;
        //     pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        // } else {
        //     pointerImage.sprite = crossSprite;
        //     Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        //     pointerRectTransform.position = pointerWorldPosition;
        //     pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        //     pointerRectTransform.localEulerAngles = Vector3.zero;
        // }

        
    }
    private void RotatePointerTowardsTargetPosition() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
