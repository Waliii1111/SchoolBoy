using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance;
    public PlayerBehavior PlayerBehavior;
    public GamePlayCanvasManager gamePlayCanvasManager;
    public Doorhandler doorhandler;
    public PickableManager pickableManager;
     
    #region Singolton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion#

    #region UnityEventMethodes

    public GameObject ObjectivePanel;

    public void ObjClose()
    {
        ObjectivePanel.SetActive(false);
    }
    
    #endregion
}
