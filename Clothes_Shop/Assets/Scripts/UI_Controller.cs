using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShopSetActive(bool value)
    {
        shopPanel.SetActive(value);
    }
}
