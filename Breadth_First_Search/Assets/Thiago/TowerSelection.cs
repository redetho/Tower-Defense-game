using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour
{
    public GameObject towerUI;
    public GameObject towerPrefab1;
    public GameObject towerPrefab2;
    
    public GamerManager GM;

    public SphereCollider sc;

    public GameObject buildSound;

    [SerializeField] private int tower1Cost = 3;
    [SerializeField] private int tower2Cost = 5;
    
    public Button towerButton1;
    public Button towerButton2;
    
    private GameObject currentObject;
    
    public float screenShakeDuration = 0.3f;
    public float screenShakeMagnitude = 0.1f;
    
    public ParticleSystem buildParticles;

    private Camera mainCamera;
    private void Awake()
    {
        GM = FindObjectOfType<GamerManager>();
        mainCamera = Camera.main;
    }
    
    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        UpdateTowerButtonState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            towerUI.SetActive(true);
            UpdateTowerButtonState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            towerUI.SetActive(false);
            UpdateTowerButtonState();
        }
    }
    public void UpdateTowerButtonState()
    {
        towerButton1.interactable = GM.CanBuildTower(tower1Cost);
        towerButton2.interactable = GM.CanBuildTower(tower2Cost);
    }
    public void SelectTower1()
    {
        if (GM.CanBuildTower(tower1Cost))
        {
            if (currentObject != null)
            {
                
                Destroy(currentObject);
            }

            currentObject = Instantiate(towerPrefab1, transform.position, Quaternion.identity);
            towerUI.SetActive(false);
            GM.SpendPoints(tower1Cost);
            UpdateTowerButtonState();
            ShakeCamera();
            PlayBuildParticles();
            sc.isTrigger = false;
            Instantiate(buildSound);
        }
    }

    public void SelectTower2()
    {
        if (GM.CanBuildTower(tower2Cost))
        {
            if (currentObject != null)
            {
                Destroy(currentObject);
            }

            currentObject = Instantiate(towerPrefab2, transform.position, Quaternion.identity);
            towerUI.SetActive(false);
            GM.SpendPoints(tower2Cost);
            UpdateTowerButtonState();
            ShakeCamera();
            PlayBuildParticles();
            sc.isTrigger = false;
            Instantiate(buildSound);
        }

    }
    private void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }
    private System.Collections.IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = mainCamera.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < screenShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * screenShakeMagnitude;
            float y = Random.Range(-1f, 1f) * screenShakeMagnitude;

            mainCamera.transform.position = originalPosition + new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPosition;
    }
    private void PlayBuildParticles()
    {
        if (buildParticles != null)
        {
            ParticleSystem particles = Instantiate(buildParticles, transform.position, Quaternion.identity);
            particles.Play();
            Destroy(particles.gameObject, particles.main.duration);
        }
    }
}