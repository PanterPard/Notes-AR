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
    // Образец
    public GameObject note_prefab;

    // Поля ввода
    public Text input_note_name;
    public Text input_note_text;
    public Text input_note_image_url;

    Texture2D imageFromWeb;

    // Создание заметки
    public void CreateNote()
    {
        GameObject note = Instantiate(note_prefab);

        note.name = input_note_name.GetComponent<Text>().text;

        Destroy(note.GetComponentInChildren<ImageTargetBehaviour>());

        // Отправка данных в буфер
        note.GetComponent<NoteDataBuffer>().note_name = input_note_name.text;
        note.GetComponent<NoteDataBuffer>().note_text = input_note_text.text;
        Debug.Log("Данные были отправлены в буфер.");
    }
}
