using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteDataBuffer : MonoBehaviour
{
    // ������
    public string note_name;
    public string note_text;
    public int note_id;

    // �������
    public Text note_name_obj;
    public Text note_text_obj;

    // ���������� ������ � �������
    public void UpdateData()
    {
        note_name_obj.text = note_name;
        note_text_obj.text = note_text;
    }
}
