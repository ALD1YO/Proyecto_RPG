using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        IAction currentAction;

        //Si currentAction es lo mismo cuando se vuelve a llamar, salte
        //Si currentAction no es nulo y es distinto a lo que llamaste, asignale la nueva action
        //Con la interfaz se cambia el valor currentAction dependiendo de que clase la llame
        //Mismo nombre, distintas acciones
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();

            }
            currentAction = action;

        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}