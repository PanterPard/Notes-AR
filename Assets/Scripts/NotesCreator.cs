using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.Networking;

public class NotesCreator : MonoBehaviour
{
    // �������
    public GameObject note_prefab;

    // ���� �����
    public Text input_note_name;
    public Text input_note_text;
    public Text input_note_image_url;

    Texture2D imageFromWeb;

    // �������� �������
    public void CreateNote()
    {
        GameObject note = Instantiate(note_prefab);

        note.name = input_note_name.GetComponent<Text>().text;

        Destroy(note.GetComponentInChildren<ImageTargetBehaviour>());

        // �������� ������ � �����
        note.GetComponent<NoteDataBuffer>().note_name = input_note_name.text;
        note.GetComponent<NoteDataBuffer>().note_text = input_note_text.text;
        Debug.Log("������ ���� ���������� � �����.");
    }
}
