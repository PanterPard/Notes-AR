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

    public Texture2D note_image_file;
    public float printed_target_size = 0.2f;

    // Создание заметки
    public void CreateNote()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideloadedTexture;
    }

    void CreateImageTargetFromSideloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            note_image_file,
            printed_target_size,
            input_note_name.text);
        // add the Default Observer Event Handler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        Debug.Log("Instant Image Target created " + mTarget.TargetName);

        GameObject note = GameObject.Find(input_note_name.text);

        Instantiate(note_prefab, note.transform);
    }
}
