using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    public Text output_text;

    public void InputFieldText()
    {
        output_text.text = GetComponent<InputField>().text;
    }
}
