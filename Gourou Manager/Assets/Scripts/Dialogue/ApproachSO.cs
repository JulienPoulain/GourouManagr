using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewApproche", menuName = "GourouManager/Dialogue/Approche")]
public class ApproachSO : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private ExactionSO m_exactionPos;
    [SerializeField] private ExactionSO m_exactionNeg;
    [SerializeField] private List<ConditionSO> m_cdtSuccess;
    [SerializeField] private int m_cooldown;
    public int m_remainingTime = 0;

    [SerializeField]
    [Tooltip("Phrase du personnage lors du dialogue (Partie de Maxime)")]
    public string m_dialogueApproach; // utiliser dans TextApprocheIndividual

    [SerializeField]
    [Tooltip("Phrase au joueur pour lui indiquer ce qu'il reçois s'il réussit l'approche, ex: si vous parvenez à l'intimider, vous pourrez obtenir ceci")]
    public string m_resultatApproach; // utiliser dans TextApprocheIndividual

    public string Name => m_name;

    public int Cooldown => m_cooldown;
    
    public int RemainingTime => m_remainingTime;

    /// <summary>
    /// Renvoie le résultat d'une tentative de cette approche.
    /// </summary>
    /// <returns>L'exaction correspondante si l'approche n'est pas en récupération. Sinon null.</returns>
   
    /*
    public ExactionSO TryApproach()
    {
        if (!(m_remainingTime > 0))
        {
            m_remainingTime = m_cooldown;
            if (ConditionsReached(m_cdtSuccess))
                return m_exactionPos;
            return m_exactionNeg;
        }
        return null;
    }
    */
    public ExactionSO TryApproach()
    {
        m_remainingTime = m_cooldown;
        if (ConditionsReached(m_cdtSuccess))
            return m_exactionPos;
        return m_exactionNeg;
    }

    public bool IsSuccessful()
    {
        return ConditionsReached(m_cdtSuccess);
    }

    private bool ConditionsReached(List<ConditionSO> p_cdt)
    {
        bool success = true;
        
        foreach (var cdt in p_cdt)
        {
            success &= cdt.IsOneValid();
        }

        return success;
    }
}
