using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextAnimationHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI animText;
    [SerializeField] private string textWrite;
    [SerializeField] private float timePerCharacter;
    private float timer;
    private int characterIndex;
    public bool isUIOverride { get; private set; }

    public void AddWriter(TextMeshProUGUI text, string textWrite, float timePerCharacter)
    {
        text = animText;
        this.textWrite = textWrite;
        this.timePerCharacter = timePerCharacter;
    }

    private void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButton(0) && !isUIOverride)
        {
            if (animText != null)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer += timePerCharacter;

                    characterIndex++;
                    if (characterIndex >= textWrite.Length)
                    {
                        characterIndex = 0;
                    }

                    animText.text += textWrite[characterIndex];
                }
            }
        }
    }

    public void ResetWriting()
    {
        characterIndex = 0;
        animText.text = "";
    }
}