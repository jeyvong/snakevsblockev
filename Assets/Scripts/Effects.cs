using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Effects : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private int health;
    public PostProcessVolume m_Volume;
    private Vignette m_Vignette;
    void Start()
    {
        m_Volume.profile.TryGetSettings(out m_Vignette);
        m_Vignette.intensity.value = 0.17f;
        m_Vignette.color.value = new Color(150, 250, 190);

    }
    void Update()
    {
       health = player.GetComponent<PlayerScrypt>().health_; 
       if (health <=3)
       {
            var Color = new Color(255, 130, 110);
            m_Vignette.color.value = Color;
       }
       else
       {
            var Color = new Color(150, 250, 190);
            m_Vignette.color.value = Color;
       }
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }

}
