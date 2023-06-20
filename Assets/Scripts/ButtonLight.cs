using UnityEngine;

public class ButtonLight : MonoBehaviour
{
    [SerializeField] private GameObject frontLight;
    
    private bool isLight = true;
    
    public void OnButtonLight()
    {
        if (isLight)
        {
            frontLight.SetActive(false);
            isLight = false;
        }
        else
        {
            frontLight.SetActive(true);
            isLight = true;
        }
        
    }
    
}
