using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBall : MonoBehaviour
{

    public List<AudioClip> Piano_sounds = new List<AudioClip>();
    public int sound_num;
    public AudioClip playSound;
    [SerializeField]
    GameObject CenterEyeAnchor;

    public Vector3 init_head;
    Vector3 ball_pos;
    public string pich;
    public int onka;
    public float time;
    float bpm;
    float top_pos;
    public float timing;
    public bool start;
    public Text text;
    public float volume;

    public string name;
    public string soundPich;
    public string soundLength;

    AudioSource audioSource;

    [SerializeField]
    GameObject IndexTip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        bpm = 120;
        onka = 4;
        timing = 1;
        start = false;
        top_pos = 1.0f;
        ball_pos.y = 100;
        volume = 1.0f;
        sound_num = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position = IndexTip.transform.position;
        if(start == false && time < 2.0f)
        {
            init_head = CenterEyeAnchor.transform.position;
            start = true;
            time += Time.deltaTime;
        }
        else if(start == true)
        {
            time += Time.deltaTime;
        }
        ball_pos.y = this.transform.position.y - init_head.y;
        ball_pos.x = this.transform.position.x - init_head.x;

        timing = (float)bpm*((float)onka/4);
        if (time > (60 /timing))
        {
            time = 0;
            audioSource.PlayOneShot(Piano_sounds[sound_num],volume);

        }

        if (this.transform.position.z > 0.5f)
        {
            volume = 0.25f;
        } else if (this.transform.position.z > 0.4f && this.transform.position.z < 0.5f)
        {
            volume = 0.5f;
        } else if (this.transform.position.z > 0.3f && this.transform.position.z < 0.4f)
        {
            volume = 0.75f;
        }else if(this.transform.position.z < 0.3f)
        {
            volume = 1.0f;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        name = other.gameObject.name;
        pich = null;
        

        for(int i = 0; i < name.Length; i++)
        {
            if(name[i]== ':'){
                soundPich = name.Substring(0, i);
                soundLength = name.Substring(i + 1, name.Length-(i+1));

                break;
            }
        }

        switch (soundPich)
        {
            case "C3":
                sound_num = 0;
                break;
            case "C#3":
                sound_num = 1;
                break;
            case "D3":
                sound_num = 2;
                break;
            case "D#3":
                sound_num = 3;
                break;
            case "E3":
                sound_num = 4;
                break;
            case "F3":
                sound_num = 5;
                break;
            case "F#3":
                sound_num = 6;
                break;
            case "G3":
                sound_num = 7;
                break;
            case "G#3":
                sound_num = 8;
                break;
            case "A3":
                sound_num = 9;
                break;
            case "A#3":
                sound_num = 10;
                break;                
            case "B3":
                sound_num = 11;
                break;
            case "C4":
                sound_num = 12;
                break;
        }
        text.text = soundPich + ":" +onka;
        onka = int.Parse(soundLength);
        Debug.Log(soundPich);
    }
}
