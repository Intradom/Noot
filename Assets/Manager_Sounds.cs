using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource source_music = null;
    [SerializeField] private AudioSource source_sfx = null;

    [SerializeField] private AudioClip track_retro = null;
    [SerializeField] private AudioClip sfx_jump = null;
    [SerializeField] private AudioClip sfx_land = null;
    [SerializeField] private AudioClip sfx_form_change = null;
    [SerializeField] private AudioClip sfx_hurt = null;
    [SerializeField] private AudioClip sfx_goal = null;

    public static Manager_Sounds Instance = null;

    public void AdjustMusicVolume(float v)
    {
        source_music.volume = v;
    }

    public void AdjustSFXVolume(float v)
    {
        source_sfx.volume = v;
    }

    public void PlayJump()
    {
        source_sfx.PlayOneShot(sfx_jump);
    }

    public void PlayLand()
    {
        source_sfx.PlayOneShot(sfx_land);
    }

    public void PlayFormChange()
    {
        source_sfx.PlayOneShot(sfx_form_change);
    }

    public void PlayHurt()
    {
        source_sfx.PlayOneShot(sfx_hurt);
    }

    public void PlayGoal()
    {
        source_sfx.PlayOneShot(sfx_goal);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        source_music.loop = true;
        source_music.volume = 1f;
        source_music.clip = track_retro;
        source_music.Play();
    }
}
