//Jeremy Legault

using UnityEngine;

public class CrystalLightBehavior : MonoBehaviour
{

    InputsManager playerInputs;
    private void Awake()
    {
        playerInputs = GameObject.Find("Player").GetComponent<InputsManager>();
        playerInputs.canUseItem = true;
        transform.position = GameObject.Find("PlayerLeftHandPos").transform.position;
    }
    void Update()
    {
        if (playerInputs.selectedItem != 2)
            Destroy(gameObject);
    }
}
