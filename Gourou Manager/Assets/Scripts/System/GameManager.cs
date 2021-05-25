﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IInitializable
{
    [SerializeField] GameObject m_MainInstitution;
    [SerializeField] List<GameObject> m_Institutions = new List<GameObject>();

    [SerializeField] InstitutionSO m_MainInstitutionSO;
    [SerializeField] List<InstitutionSO> m_institutions = new List<InstitutionSO>();

    //[SerializeField] public ScriptableObject[] m_Institutions;
    //[SerializeField] public ScriptableObject[] m_Crise;

    [SerializeField] public RoundManager m_roundManager;
    [SerializeField] public InterfaceManager m_InterfaceManager;
    [SerializeField] public GameObject m_Camera;
    
    [SerializeField] private static List<ExactionSO> m_pendingExactions = new List<ExactionSO>();
    [SerializeField] private List<EventSO> m_activeEvents = new List<EventSO>();

    [SerializeField] private List<ConditionSO> m_cdtVictory;
    [SerializeField] private List<ConditionSO> m_cdtDefeat;

    public bool m_focusOnInstitution = false;
    [SerializeField] private int m_turn;

    public List<ExactionSO> PendingExactions => m_pendingExactions;
    public List<EventSO> ActiveEvents => m_activeEvents;
    public int Turn => m_turn;

    public InstitutionSO MainInstitution => m_MainInstitutionSO;
    public List<InstitutionSO> Institutions => m_institutions;

    // Player Variable //
    
    private static bool m_playerHasExecuteExaction = false; // définit si le joueur à déjà fait une exaction ce tour ci, static pour que l'information ne change pas entre les changments de scènes
    private static bool m_playerHasExecuteApproche = false;    // Définit si le joueur à déjà fait un dialogue ce tour ci
    
    public bool PlayerHasExecuteApproach
    {
        get { return m_playerHasExecuteApproche; }
        set { m_playerHasExecuteApproche = value;}
    }
    public bool PlayerHasExectuteExaction
    {
        get { return m_playerHasExecuteExaction; }
        set { m_playerHasExecuteExaction = value; }
    }

    public void Initialize()
    {
        m_turn = 0;
        
        foreach (InstitutionSO institution in m_institutions)
            institution.Initialize();

        // Initializastion des conditions de victoire.
        if (m_cdtVictory.Count < 1)
            Debug.Log("<color=red>ERROR :</color> Aucune condition de victoire.");
        else
            foreach (ConditionSO condition in m_cdtVictory)
                condition.Initialize();

        // Initialisation des conditions de défaite.
        if (m_cdtDefeat.Count < 1)
            Debug.Log("<color=red>ERROR :</color> Aucune condition de défaite.");
        else
            foreach (ConditionSO condition in m_cdtDefeat)
                condition.Initialize();
            
    }
    
    private void Start()
    {
        m_MainInstitutionSO = m_MainInstitution.GetComponent<InterfaceInstitution>().m_Institution;

        foreach (GameObject Institution in m_Institutions)
        {
            m_institutions.Add(Institution.GetComponent<InterfaceInstitution>().m_Institution);
        }
        Initialize();
    }

    public void EndTurn()
    {
        RoundManager.Instance.NextTurn();

        TryEndGame();
        

        m_playerHasExecuteExaction = false;
        m_playerHasExecuteApproche = false;
        
        Debug.Log("DEBUT DU DISPLAy");
        m_InterfaceManager.DisplayEndTurn();

        m_turn++;
    }

    
    // GESTION DES CONDITIONS DE VICTOIRE ET DÉFAITE
    
    private void TryEndGame()
    {
        if (IsVictory())
            Victory();
        else if (IsDefeat())
            Defeat();
    }

    private bool IsVictory()
    {
        if (m_cdtVictory.Count > 0)
            return ConditionSO.IsAllValid(m_cdtVictory);
        return false;
    }

    private bool IsDefeat()
    {
        if (m_cdtDefeat.Count > 0)
            return ConditionSO.IsAllValid(m_cdtDefeat);
        return false;
    }

    private void Victory()
    {
        Debug.Log("### VICTOIRE ###");
    }

    private void Defeat()
    {
        Debug.Log("### DEFAITE ###");
    }
}
