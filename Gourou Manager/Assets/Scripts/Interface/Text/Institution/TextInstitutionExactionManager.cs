using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInstitutionExactionManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_textNom;
    [SerializeField] TMP_Text m_textDescription;

    ExactionSO m_exaction;

    public void Display(ExactionSO p_exaction)
    {
        m_exaction = p_exaction;

        m_textNom.text = "" + p_exaction.Name;
        m_textDescription.text = "" + p_exaction.Description;

        // if (p_exaction.Conditions.IsOneValid())
    }

    public void ExecuteExaction()
    {
        if (!GameManager.Instance.PlayerHasExectuteExaction && m_exaction.IsValid())
        {
            GameManager.Instance.PendingExactions.Add(m_exaction);
            GameManager.Instance.m_InterfaceManager.DisallowHeavyInstitution();
            Debug.Log("Exaction lancée");

            GameManager.Instance.PlayerHasExectuteExaction = true;
        }
        else
        {
            Debug.Log("Vous avez déjà fait une exaction ou cette exaction n'est pas valide");
        }
    }
}
