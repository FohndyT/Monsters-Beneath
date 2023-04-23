using UnityEngine;

public class CrystalLightBehavior : MonoBehaviour
{

    InputsManager playerInputs;
    private void Awake()
    {
        playerInputs = GameObject.Find("Player").GetComponent<InputsManager>();
        playerInputs.canUseItem = true;
    }
    void Update()
    {
        if (playerInputs.selectedItem != 2 || playerInputs.selectedItem != 3)
            Destroy(gameObject);
    }
}
