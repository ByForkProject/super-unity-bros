﻿using UnityEngine;
using System.Collections;

public class KoopaTroopaController : EnemyController
{
    [SerializeField]
    private float m_ShellSpeed = 72;

    private bool m_LockedInShell;

    public override void OnPlayerJump(PlayerController player)
    {
        if (lockedInShell)
        {
            if (Mathf.Approximately(m_Velocity.x, 0))
            {
                if (player.posX > posX)
                {
                    m_Velocity.x = -m_ShellSpeed;
                }
                else
                {
                    m_Velocity.x = m_ShellSpeed;
                }
            }
            else
            {
                m_Velocity.x = 0;
                player.JumpOnEnemy(this);
            }
        }
        else
        {
            // TODO: start "unlock" timer
            m_Velocity.x = 0;
            lockedInShell = true;

            player.JumpOnEnemy(this);
        }
    }

    public override void OnPlayerCollision(PlayerController player)
    {
        if (lockedInShell && Mathf.Approximately(m_Velocity.x, 0f))
        {
            if (player.posX > posX)
            {
                m_Velocity.x = -m_ShellSpeed;
            }
            else
            {
                m_Velocity.x = m_ShellSpeed;
            }
        }
        else
        {
            base.OnPlayerCollision(player);
        }
    }

    protected override void OnCollision(EnemyController enemy)
    {
        if (m_LockedInShell && !Mathf.Approximately(m_Velocity.x, 0f))
        {
            enemy.TakeDamage(this);
        }
        else
        {
            FlipHorMovement();
        }
    }

    #region Properties

    public bool lockedInShell
    {
        get { return m_LockedInShell; }
        set
        {
            m_LockedInShell = value;
            animator.SetBool("Shell", value);
        }
    }

    #endregion
}
