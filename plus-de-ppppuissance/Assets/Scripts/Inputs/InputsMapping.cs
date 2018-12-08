using UnityEngine;

public enum XboxInputAxis
{
    LeftStickX, 
    LeftStickY,
    RightStickX,
    RightStickY,
    LeftTrigger,
    RightTrigger
}


public enum XboxInputButton
{
    ButtonA,
    ButtonB,
    ButtonX,
    ButtonY,
    LeftBumper,
    RightBumper,
    BackButton,
    StartButton
}

public static class InputsMapping
{
    public static string Axis(XboxInputAxis axis)
    {
        string stringAxis = "";
        switch(axis)
        {
            case XboxInputAxis.LeftStickX:
                stringAxis = "Horizontal";
                break;
            case XboxInputAxis.LeftStickY:
                stringAxis = "Vertical";
                break;
            case XboxInputAxis.RightStickX:
                stringAxis = "XboxRightStickX";
                break;
            case XboxInputAxis.RightStickY:
                stringAxis = "XboxRightStickY";
                break;
            case XboxInputAxis.LeftTrigger:
                stringAxis = "XboxLeftTrigger";
                break;
            case XboxInputAxis.RightTrigger:
                stringAxis = "XboxRightTrigger";
                break;
        }
        return stringAxis;
    }

    public static KeyCode Button(XboxInputButton button)
    {

        KeyCode keyCode = KeyCode.End;
        switch (button)
        {
            case XboxInputButton.ButtonA:
                keyCode = KeyCode.JoystickButton0;
                break;
            case XboxInputButton.ButtonB:
                keyCode = KeyCode.JoystickButton1;
                break;
            case XboxInputButton.ButtonX:
                keyCode = KeyCode.JoystickButton2;
                break;
            case XboxInputButton.ButtonY:
                keyCode = KeyCode.JoystickButton3;
                break;
            case XboxInputButton.LeftBumper:
                keyCode = KeyCode.JoystickButton4;
                break;
            case XboxInputButton.RightBumper:
                keyCode = KeyCode.JoystickButton5;
                break;
            case XboxInputButton.BackButton:
                keyCode = KeyCode.JoystickButton6;
                break;
            case XboxInputButton.StartButton:
                keyCode = KeyCode.JoystickButton7;
                break;
        }
        return keyCode;
    }
}