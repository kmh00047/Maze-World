using UnityEngine;
using UnityEngine.UI;

public class ButtonTeleporter : MonoBehaviour
{
    // References to the two buttons that will be moved
    public RectTransform button1;
    public RectTransform button2;

    // References to the 3 possible positions for each button
    public RectTransform button1Position1;
    public RectTransform button1Position2;
    public RectTransform button1Position3;

    public RectTransform button2Position1;
    public RectTransform button2Position2;
    public RectTransform button2Position3;

    // Flags to track current positions for both buttons
    private int button1CurrentPosition = 0; // 0, 1, or 2 for the 3 positions
    private int button2CurrentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Add the OnClick listener to the current GameObject (this script is attached to it)
        GetComponent<Button>().onClick.AddListener(TogglePositions);
    }

    // Method to toggle the buttons' positions when the button is pressed
    void TogglePositions()
    {
        // Move button 1 to the next position
        button1CurrentPosition = (button1CurrentPosition + 1) % 3;
        MoveButton1ToCurrentPosition();

        // Move button 2 to the next position
        button2CurrentPosition = (button2CurrentPosition + 1) % 3;
        MoveButton2ToCurrentPosition();
    }

    // Method to move button 1 to the current position
    void MoveButton1ToCurrentPosition()
    {
        switch (button1CurrentPosition)
        {
            case 0:
                button1.position = button1Position1.position;
                break;
            case 1:
                button1.position = button1Position2.position;
                break;
            case 2:
                button1.position = button1Position3.position;
                break;
        }
    }

    // Method to move button 2 to the current position
    void MoveButton2ToCurrentPosition()
    {
        switch (button2CurrentPosition)
        {
            case 0:
                button2.position = button2Position1.position;
                break;
            case 1:
                button2.position = button2Position2.position;
                break;
            case 2:
                button2.position = button2Position3.position;
                break;
        }
    }
}
