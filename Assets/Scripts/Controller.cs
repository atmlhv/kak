using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected GameManager m_gameManager;

    [SerializeField]
    protected Controller[] m_childrenControllers;

    public virtual void InitializeController(GameManager gameManager)
    {
        m_gameManager = gameManager;
        /*
        if (m_childrenControllers == null)
        {
            m_childrenControllers = new Controller[0];
        }
        */
        for (int i = 0; i < m_childrenControllers.Length; i++)
        {
            m_childrenControllers[i].InitializeController(gameManager);
        }


    }

    public virtual void InitializeManagedItems()
    {
        for (int i = 0; i < m_childrenControllers.Length; i++)
        {
            m_childrenControllers[i].InitializeManagedItems();
        }
    }

    protected T GetInheritOfController<T>(Controller[] Controllers) where T : Controller
    {
        for (int i = 0; i < Controllers.Length; i++)
        {
            if (Controllers[i].GetComponent<T>() != null)
            {
                return Controllers[i].GetComponent<T>();
            }
        }
        return default;
    }

    protected T GetController<T>(T ControllerObject, Controller[] Controllers) where T : Controller
    {
        if (ControllerObject == null)
        {
            ControllerObject = GetInheritOfController<T>(Controllers);
        }
        return ControllerObject;
    }
}
