using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMenu : MonoBehaviour
{
    [SerializeField] private List<Transform> menuPages = new List<Transform>();
    
    private int currentPage = 0;
    
    public virtual void NextPage()
    {
        menuPages[currentPage].gameObject.SetActive(false);
        currentPage++;
        menuPages[currentPage].gameObject.SetActive(true);
    }

    public virtual void RestartMenu()
    {
        currentPage = 0;
        foreach (var page in menuPages)
        {
            page.gameObject.SetActive(false);
        }
        menuPages[currentPage].gameObject.SetActive(true);
    }
}
