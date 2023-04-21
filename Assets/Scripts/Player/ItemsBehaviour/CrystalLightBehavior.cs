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
        if (playerInputs.itemIndex != 2 || playerInputs.itemIndex != 3)
            Destroy(gameObject);
    }
}
