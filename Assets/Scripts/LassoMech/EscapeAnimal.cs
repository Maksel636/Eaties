using Assets.Scripts.LassoMech;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EscapeAnimal : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _directionIndicatorPrefab;
    [SerializeField] private GameObject _lassoDonutPrefab;

    [Header("DifficutlyLasso")]
    [SerializeField] private float _winScore;
    [SerializeField] private float _escapSpeed;
    [SerializeField]private float _escapeSteps;

    //[SerializeField] private LassoMech _lassoMech;
    [Header("Extra Data")]
    [SerializeField] private float _shrinkSpeed;
    //[SerializeField] private Color[] _playerColors = new Color[] { Color.blue, Color.yellow, Color.green, Color.mistyRose };
    [SerializeField] private float _score;
    [SerializeField] private List<PlayerData> _playersData;

    [Header("publics")]
    public bool IsEscaping = false;
    public bool IsCaptured = false;

    private GameObject _lassoDonut;
  //  private float _rotation = 0;
    //[SerializeField] private int _players;
  //  [SerializeField] private GameObject testObject;
    void Awake()
    {
        _playersData = new List<PlayerData>();
    }

    void Start()
    {
        //StartEscaping();
    }

    void Update()
    {
        if (IsEscaping)
            UpdateRotation();
    }
    public void StartEscaping()
    {
        if(IsEscaping) return;
        if(IsCaptured) return;

        IsEscaping = true;
        _score = 0;
        _lassoDonut = Instantiate(_lassoDonutPrefab, transform.parent);
    }
    public void JoinPlayer(Transform player, Color playerColor)
    {
        StartEscaping(); // escape when the first player joins

        PlayerData data = new PlayerData();

        //player.GetChild(0).gameObject.SetActive(true);

        data.Player = player;

        data.Player.gameObject.GetComponent<LassoMech>().IsAnimalEscaping = true;

        data.Indicatororigin = Instantiate(_directionIndicatorPrefab, transform).transform;
        data.Indicatororigin.GetChild(0).GetComponent<Renderer>().material.color = playerColor;
        data.PlayerColor = playerColor;
        data.Player.GetChild(0).gameObject.SetActive(true); // dubble check to make sure the line is active


        data.Rotation = Random.Range(0f, 360f);
        data.Direction = Random.Range(0, 2) == 0 ? -1 : 1;
        data.EscapeSteps = Random.Range(0, 200);

        _playersData.Add(data);
    }

    private void UpdateRotation()
    {
        UpdateIndicators();

        foreach (var p in _playersData)
        {
            UpdatePlayerScore(p.Player, p.Player.GetChild(0), p.Indicatororigin.GetChild(0), p.PlayerColor);
        }

        UpdateLassoSize();

        if (_score >= _winScore) {
            EndCapturing();
        }
    }

    private void UpdateLassoSize()
    {
        float progress = Mathf.Clamp01((_score * _shrinkSpeed) / _winScore);
        float scale = Mathf.Lerp(200f, 70f, progress);

        _lassoDonut.transform.localScale = Vector3.one * scale;
    }

    private void UpdatePlayerScore(Transform playerTransform, Transform playerLine, Transform directionObject, Color playerColor)
    {
        Vector3 PlayerToAnimal = transform.position - playerTransform.position;
        Vector3 IndicatorToAnimal = transform.position - directionObject.transform.position;
        PlayerToAnimal.y = 0;
        IndicatorToAnimal.y = 0;
        IndicatorToAnimal.Normalize();
        PlayerToAnimal.Normalize();

        Renderer directioRenderer = directionObject.GetComponentInChildren<Renderer>();


        if (Vector3.Dot(IndicatorToAnimal, PlayerToAnimal) > 0.6f) // see if the direction is close enough to the player direction
        {
            directioRenderer.material.SetColor("_EmissionColor", playerColor);           // Debug.Log("indictor close");
            _score += 1 * Time.deltaTime;
        }
        else
        {
            directioRenderer.material.SetColor("_EmissionColor", Color.black);           // Debug.Log("indictor close");
        }
    }
    private void LateUpdate()
    {
        foreach (var p in _playersData)
        {
            LateUpdateLine(p.Player, p.Player.GetChild(0));
        }
        
    }

    private void LateUpdateLine(Transform playerTransform, Transform line)
    {
        line.LookAt(transform.position);
        line.localScale = Vector3.Distance(playerTransform.position, transform.position) * Vector3.forward + Vector3.one * 0.1f;

    }

    private void UpdateIndicators()
    {
        foreach (var p in _playersData)
        {
            p.EscapeSteps -= 100 * Time.deltaTime;

            if (p.EscapeSteps <= 0)
            {
                p.EscapeSteps = Random.Range(0, 200);
                p.Direction = Random.Range(0, 2) == 0 ? -1 : 1;
            }

            p.Rotation += _escapSpeed * p.Direction * Time.deltaTime;
            p.Indicatororigin.rotation = Quaternion.Euler(0, p.Rotation, 0);
        }
    }

    private void EndCapturing()
    {


        IsEscaping = false;
        IsCaptured = true;

        _playersData[0].Player.gameObject.GetComponent<PlayerMovement>()
            .GrabCapturedAnimal(transform.parent.gameObject);    // first player that captured the animal gets the meat

        foreach (var player in _playersData)
        {

            player.Indicatororigin.gameObject.SetActive(false);
            //player.player.GetChild(0).gameObject.SetActive(false);
            player.Player.GetComponent<LassoMech>().ResetLasso();

        }



        _playersData.Clear();

        Destroy(_lassoDonut);
    }
}
