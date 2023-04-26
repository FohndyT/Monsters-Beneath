using UnityEngine;

public class CrystalLaser : MonoBehaviour
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
        if (playerInputs.selectedItem != 3)     // Namor ne veut pas mourrrirr !
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
