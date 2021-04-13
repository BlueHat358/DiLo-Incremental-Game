using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class ResourceController : MonoBehaviour{

    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;

    private ResourceConfig _config;
    private int _level = 1;

    private static GameManager _instance = null;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public void SetConfig(ResourceConfig config) {
        _config = config;

        // ToString("0") berfungsi untuk membuang angka di belakang koma
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutput().ToString("0") }";
        ResourceUnlockCost.text = $"Unlock Cost\n{ _config.UnlockCost }";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
    }

    public double GetOutput() {

        return _config.Output * _level;

    }

    public double GetUpgradeCost() {

        return _config.UpgradeCost * _level;

    }

    public double GetUnlockCost() {
        return _config.UnlockCost;
    }

    public void UpgradeLevel() {
        double upgradeCost = GetUpgradeCost();

        if (GameManager.Instance.TotalGold < upgradeCost) {
            return;
        }

        GameManager.Instance.AddGold(-upgradeCost);

        _level++;

        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutput().ToString("0") }";
    }

    // Start is called before the first frame update
    void Start(){
        ResourceButton.onClick.AddListener(UpgradeLevel);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
