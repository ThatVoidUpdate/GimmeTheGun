using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public enum MenuItemHighlightType { ColourChange, SpriteChange}

public class MenuNavigator : MonoBehaviour
{
    public MenuNavigator UpElement;
    public MenuNavigator DownElement;
    public MenuNavigator LeftElement;
    public MenuNavigator RightElement;

    [HideInInspector]
    public bool IsSelected;

    public MenuItemHighlightType ChangeType;
    public Color ColourChangeColour;
    private Color OldColour;

    public Sprite SpriteChangeSprite;
    private Sprite OldSprite;

    public void GetInput(Direction SelectDirection)
    {
        switch (SelectDirection)
        {
            case Direction.Up:
                if (UpElement != null)
                {
                    UpElement.IsSelected = true;
                    IsSelected = false;

                    OnBecomeDeselected();
                    UpElement.OnBecomeSelected();
                }
                break;
            case Direction.Down:
                if (DownElement != null)
                {
                    DownElement.IsSelected = true;
                    IsSelected = false;

                    OnBecomeDeselected();
                    DownElement.OnBecomeSelected();
                }
                break;
            case Direction.Right:
                if (RightElement != null)
                {
                    RightElement.IsSelected = true;
                    IsSelected = false;

                    OnBecomeDeselected();
                    RightElement.OnBecomeSelected();
                }
                break;
            case Direction.Left:
                if (LeftElement != null)
                {
                    LeftElement.IsSelected = true;
                    IsSelected = false;

                    OnBecomeDeselected();
                    LeftElement.OnBecomeSelected();
                }
                break;
            case Direction.None:
                //Selecting element
                GetComponent<Button>().onClick.Invoke();
                break;
            default:
                break;
        }
    }

    public void OnBecomeSelected()
    {
        switch (ChangeType)
        {
            case MenuItemHighlightType.ColourChange:
                GetComponent<Button>().image.color = ColourChangeColour;
                break;
            case MenuItemHighlightType.SpriteChange:
                GetComponent<Button>().image.sprite = SpriteChangeSprite;
                break;
            default:
                break;
        }
    }

    public void OnBecomeDeselected()
    {
        switch (ChangeType)
        {
            case MenuItemHighlightType.ColourChange:
                GetComponent<Button>().image.color = OldColour;
                break;
            case MenuItemHighlightType.SpriteChange:
                GetComponent<Button>().image.sprite = OldSprite;
                break;
            default:
                break;
        }
    }
}

[CustomEditor(typeof(MenuNavigator))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var CurrentScript = target as MenuNavigator;

        CurrentScript.ChangeType = (MenuItemHighlightType)EditorGUILayout.EnumPopup("Highlight Type", CurrentScript.ChangeType);

        if (CurrentScript.ChangeType == MenuItemHighlightType.ColourChange)
        {
            CurrentScript.ColourChangeColour = EditorGUILayout.ColorField("Selected Colour", CurrentScript.ColourChangeColour);
        }
        else if (CurrentScript.ChangeType == MenuItemHighlightType.SpriteChange)
        {
            CurrentScript.SpriteChangeSprite = (Sprite)EditorGUILayout.ObjectField("Selected Sprite", CurrentScript.SpriteChangeSprite, typeof(Sprite), true);
        }

    }
}
