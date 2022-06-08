using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BirdsController : MonoBehaviour
{
    #region Singleton class: GameManager

    public static BirdsController Instance;

    #endregion

    Camera cam;

    BirdEntityBase3 birdEntityEntity;

    public Trajectory trajectory;
    public float _pushForce = 4f;
    public float Timeforrespawn = 3f;
    public float timeForDisable = 3f;

    public float TimeForRespawn => Timeforrespawn;

    public bool _isDragging;
    public bool Launched;
    public bool disappeared;
    public bool midFlyAPplied;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 direction;
    private Vector3 force;
    public float distance;

    private TMP_Text text;

    public float time;

    private Vector3 initPos;
    private Vector3 initScale;
    private Quaternion initRot;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (birdEntityEntity == null)
            birdEntityEntity = GetComponentInChildren<BirdEntityBase3>();

        cam = Camera.main;
        birdEntityEntity.DisableBehave();
        initPos = birdEntityEntity.transform.position;
        initRot = birdEntityEntity.transform.rotation;
        initScale = birdEntityEntity.transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Launched)
        {
            _isDragging = true;
            OnDragStart();
        }
        else if (Input.GetMouseButtonDown(0) && Launched && !midFlyAPplied)
        {
            birdEntityEntity.GetComponentInChildren<IMidFlyAble>().PerformMidFlyBehave();
            // birdEntityEntity.PerformMidFlyBehave();
            midFlyAPplied = true;
        }

        if (Input.GetMouseButtonUp(0) && !Launched)
        {
            _isDragging = false;
            OnDragEnd();
        }

        if (_isDragging)
        {
            OnDrag();
        }

        if (Launched)
        {
            time += Time.deltaTime;

            if (time >= timeForDisable && !disappeared)
            {
                // birdEntityEntity.DisappearBird();
                birdEntityEntity.GetComponentInChildren<IDisapperable>()?.DisappearBird();
                disappeared = true;
            }

            if (time >= Timeforrespawn)
            {
                birdEntityEntity.DisableBehave();
                ResetBird();
                time = 0;
            }
        }

        text = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        text.text = time.ToString();
    }

    public void ResetBird()
    {
        birdEntityEntity.transform.position = initPos;
        birdEntityEntity.transform.rotation = initRot;
        birdEntityEntity.transform.localScale = initScale;
        birdEntityEntity.AppearBird();
        Launched = false;
        disappeared = false;
        midFlyAPplied = false;
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        birdEntityEntity.DisableBehave();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * _pushForce;

        //just for debug
        Debug.DrawLine(startPoint, endPoint);
        trajectory.UpdateDots(birdEntityEntity.movementCtrl.pos, force);
    }

    void OnDragEnd()
    {
        birdEntityEntity.MakeSound();

        //push the ball
        birdEntityEntity.movementCtrl.ActivateRb();

        birdEntityEntity.movementCtrl.Push(force);

        trajectory.Hide();

        Launched = true;
    }
}