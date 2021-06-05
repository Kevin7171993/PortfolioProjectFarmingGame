using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDialogueBox : MonoBehaviour
{
    public string current_text;
    public List<string> current_Dialogue;
    public Text mTextDisplay;
    public Text mNameDisplay;
    public float textSpeed;
    private float threshold = 10;
    private float mCount;
    private int mCurrentIndex = 0;
    private int mDialogueIndex = 0;
    public bool mEnabled = false;

    [SerializeField]
    private Image mContinueArrow;
    [SerializeField]
    private Vector3 HidePos;
    [SerializeField]
    private Transform UIAnchor;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gDialogueBoxUI = this;
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mEnabled) { return; }
        mCount += textSpeed * Time.deltaTime; //count time when to print next character
        if(mCount >= threshold && mTextDisplay.text != current_text) //print text one character at a time until done
        {
            mCount = 0;
            if (mCurrentIndex < current_text.Length)
            {
                mTextDisplay.text += current_text[mCurrentIndex];
                ++mCurrentIndex;
            }
        }
        else if(Input.GetMouseButtonUp(0) && mTextDisplay.text != current_text)
        {
            mTextDisplay.text = current_text;
        }
        else if(mTextDisplay.text == current_text && Input.GetMouseButtonUp(0)) //if done and player pressed left mouse button, go back and print the next line
        {
            if(current_Dialogue.Count > 0) //Print next line
            {
                current_text = current_Dialogue[0]; //Set the front as the next line to print
                current_Dialogue.RemoveAt(0); //delete front
                mCurrentIndex = 0;
                mTextDisplay.text = "";
            }
            else //There is no more lines in the string list for printing, clean up the dialogue box and close it
            {
                Close();
            }
        }

        if(mTextDisplay.text == current_text && current_Dialogue.Count > 0)
        {
            mContinueArrow.GetComponent<UI_Anim_UpDownArrow>().mEnabled = true;
            mContinueArrow.GetComponent<UI_Anim_UpDownArrow>().mVisible = true;
        }
        else
        {
            mContinueArrow.GetComponent<UI_Anim_UpDownArrow>().mVisible = false;
        }
    }

    public void PrintDialogue(string body, string speakerName)
    {
        Open();
        mNameDisplay.text = speakerName;
        current_Dialogue.Add(body);
    }
    public void PrintDialogue(string body)
    {
        Open();
        mNameDisplay.text = "";
        current_Dialogue.Add(body);
    }
    public void PrintDialogue(List<string> body, string speakerName)
    {
        Open();
        mNameDisplay.text = speakerName;
        current_Dialogue = new List<string>(body);
    }
    public void PrintDialogue(List<string> body)
    {
        Open();
        mNameDisplay.text = "";
        current_Dialogue = new List<string>(body);
    }
    public void Close()
    {
        current_Dialogue.Clear();
        mCurrentIndex = 0;
        mDialogueIndex = 0;
        mTextDisplay.text = "";
        mNameDisplay.text = "";
        current_text = "";
        transform.position = HidePos;
        UIManager.UILock = false;
        mEnabled = false;
    }

    public void Open()
    {
        UIManager.UILock = true;
        mEnabled = true;
        transform.position = UIAnchor.position;
    }
}
