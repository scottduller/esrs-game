using System.Collections;
using GameScripts;
using GameScripts.Player;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    private int currentHealth;
    ArrayList healthBoxes;
    public Color noHealth;
    private Color startColor;


    public GameObject healthBoxPrefab;
    // Start is called before the first frame update
    void Start()
    {
        startColor = healthBoxPrefab.transform.Find("Image").GetComponent<Image>().color;
       

    }

    public void setHealth(int health)
    {
        healthBoxes = new ArrayList();
        currentHealth = health;
        Debug.Log(healthBoxPrefab);
        for(int i = 0; i< currentHealth; i++)
        {
            healthBoxes.Add(Instantiate(healthBoxPrefab, transform));
        }
    }
    

    public void UpdateDisplay(int health)
    {
        currentHealth = health;
        int i = currentHealth-1;
        foreach (GameObject boxTochange in healthBoxes)
        {
            if (i< 0)
            {
                boxTochange.transform.Find("Image").GetComponent<Image>().color = noHealth;
            }
            else
            {
                boxTochange.transform.Find("Image").GetComponent<Image>().color = startColor;
                i--;
            }

        }
    }
}
