using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class LeapHand : MonoBehaviour
{
    [SerializeField]
    private GameObject soundball;
    private Controller controller;
    private Finger[] fingers;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        fingers = new Finger[5];
        soundball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = controller.Frame();
        if(frame.Hands.Count != 0)
        {
            foreach(Hand hand in frame.Hands)
            {
                fingers = hand.Fingers.ToArray();
                if (hand.IsLeft)
                {
                    if(!fingers[0].IsExtended && !fingers[1].IsExtended)
                    {
                        soundball.SetActive(true);
                    }
                    else
                    {
                        soundball.SetActive(false);
                    }
                }
                if (hand.IsRight)
                {

                }
            }
        }
    }
}
