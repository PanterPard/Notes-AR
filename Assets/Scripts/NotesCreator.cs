using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotesCreator : MonoBehaviour
{
    public GameObject note_prefab;

    public GameObject note_name;
    public GameObject note_text;

    public void CreateNote()
    {
        GameObject note = Instantiate(note_prefab);

        note.name = note_name.GetComponent<Text>().text;
    }
}
