using Assets.Scripts.LassoMech;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EscapeAnimal : MonoBehaviour
{
    [SerializeField] private float _escapSpeed = 5f;
    [SerializeField] private float _rotation = 0;
    public bool IsEscaping = false;
    public bool IsCaptured = false;

    [SerializeField] private List<PlayerData> _playersData;
    [SerializeField] private LassoMech _lassoMech;

    [SerializeField]private float _escapeSteps;
    [SerializeField] private GameObject _lassoDonutPrefab;
    [SerializeField] private GameObject _directionIndicatorPrefab;
    [SerializeField] private GameObject _lassoDonut;
    [SerializeField] private float _score;
    [SerializeField] private float _shrinkSpeed = 1f;
    [SerializeField] private float _winScore = 1.5f;
    [SerializeField] private int _players;
    private Color[] _playerColors = new Color[] { Color.blue, Color.yellow, Color.green, Color.mistyRose };
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
    public void JoinPlayer(Transform player)
    {
        StartEscaping(); // escape when the first player joins

        PlayerData data = new PlayerData();

        //player.GetChild(0).gameObject.SetActive(true);

        data.player = player;

        data.player.gameObject.GetComponent<LassoMech>().IsAnimalEscaping = true;

        data.indicatororigin = Instantiate(_directionIndicatorPrefab, transform).transform;
        data.indicatororigin.GetChild(0).GetComponent<Renderer>().material.color = _playerColors[_playersData.Count];

        data.player.GetChild(0).gameObject.SetActive(true); // dubble check to make sure the line is active


        data.rotation = Random.Range(0f, 360f);
        data.direction = Random.Range(0, 2) == 0 ? -1 : 1;
        data.escapeSteps = Random.Range(0, 200);

        _playersData.Add(data);
    }

    private void UpdateRotation()
    {
        UpdateIndicators();

        foreach (var p in _playersData)
        {
            UpdatePlayerScore(p.player, p.player.GetChild(0), p.indicatororigin.GetChild(0));
        }

        UpdateLassoSize();

        if (_score >= _winScore) {
            EndCapturing();
        }
    }

    private void UpdateLassoSize()
    {
        float progress = Mathf.Clamp01((_score * _shrinkSpeed) / _winScore);
        float scale = Mathf.Lerp(200f, 100f, progress);

        _lassoDonut.transform.localScale = Vector3.one * scale;
    }

    private void UpdatePlayerScore(Transform playerTransform, Transform playerLine, Transform directionObject)
    {
        Vector3 PlayerToAnimal = transform.position - playerTransform.position;
        Vector3 IndicatorToAnimal = transform.position - directionObject.transform.position;
        PlayerToAnimal.y = 0;
        IndicatorToAnimal.y = 0;
        IndicatorToAnimal.Normalize();
        PlayerToAnimal.Normalize();

        playerLine.LookAt(transform.position);
        playerLine.localScale = Vector3.Distance(playerTransform.position, transform.position) * Vector3.forward + Vector3.one * 0.1f;


        if (Vector3.Dot(IndicatorToAnimal, PlayerToAnimal) > 0.6f) // see if the direction is close enough to the player direction
        {
            Debug.Log("indictor close");
            _score += 1 * Time.deltaTime;
        }
    }

    private void UpdateIndicators()
    {
        foreach (var p in _playersData)
        {
            p.escapeSteps -= 100 * Time.deltaTime;

            if (p.escapeSteps <= 0)
            {
                p.escapeSteps = Random.Range(0, 200);
                p.direction = Random.Range(0, 2) == 0 ? -1 : 1;
            }

            p.rotation += _escapSpeed * p.direction * Time.deltaTime;
            p.indicatororigin.rotation = Quaternion.Euler(0, p.rotation, 0);
        }
    }

    private void EndCapturing()
    {
        IsEscaping = false;
        IsCaptured = true;
        foreach (var player in _playersData)
        {
            player.indicatororigin.gameObject.SetActive(false);
            //player.player.GetChild(0).gameObject.SetActive(false);
            player.player.GetComponent<LassoMech>().ResetLasso();

        }



        _playersData.Clear();

        Destroy(_lassoDonut);
    }
}
