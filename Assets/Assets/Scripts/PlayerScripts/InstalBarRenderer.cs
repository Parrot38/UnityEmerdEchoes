using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InstalBarRenderer:MonoBehaviour
{
    public List<SpriteRenderer> instalBar = new List<SpriteRenderer>();
    public SpriteRenderer burstRenderer;
    public int currentIndex = 0;
    public float instalPoints = 0f;
    private int currentInt;
    private int lastInt;
    public bool instalActivated = false;
    public float neutralTimer = 0f;

    private void Start()
    {
        foreach (SpriteRenderer sr in instalBar)
        {
            sr.color = new Color(1f,1f,1f,0f);
        }
        lastInt = 0;
        instalActivated = false;
    }

    private void Update()
    {
        currentInt = (int)instalPoints;
    }

    private void FixedUpdate()
    {
        if (neutralTimer > 0) neutralTimer -= Time.fixedDeltaTime;
        SetBarsAlpha();
        if (instalPoints == 4f)
        {
            instalActivated = true;
        }
        if (instalActivated) InstalBarActivated();
        if (!instalActivated) burstRenderer.color = new Color(1f, 1f, 1f, 0f);

    }

    private void SetBarsAlpha()
    {
        float alpha;
        int alphaCount;
        float lastValue = 0;
        if (currentInt != lastInt) 
        { 
            alphaCount = currentInt;
            foreach (SpriteRenderer sr in instalBar)
            {
                if (alphaCount > 0) alpha = 1;
                else alpha = 0;
                sr.color = new Color(1f, 1f, 1f, alpha);
                alphaCount -= 1;
            }
            lastInt = currentInt;
        }
        if (instalPoints != lastValue)
        {
            alpha = instalPoints - currentInt;
            if (currentInt == 4 ) instalBar[currentInt-1].color = new Color(1f, 1f, 1f, alpha);
            else instalBar[currentInt].color = new Color(1f, 1f, 1f, alpha);
            lastValue = instalPoints;
        }
    }
    public void ChangeInstalPoints(float points)
    {
        if (instalActivated) instalPoints += points/2;
        else instalPoints += points;
        instalPoints = math.clamp(instalPoints, 0f, 4f);
    }
    private void InstalBarActivated()
    {
        ChangeInstalPoints(-0.05f);
        burstRenderer.color = new Color(1f, 1f, 1f, 1f);
        if (instalPoints == 0f) instalActivated = false;
    }
}
