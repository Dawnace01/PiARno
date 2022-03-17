using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

using PianoUtilities;
using System.Linq;

public class FingerDetection : MonoBehaviour
{ 
    public GameObject gameObject;
    public GameObject target;
    public Interactable interactable;
    public States states;
    private KeyStates keyStates = null;

    private ThemeDefinition playerBlackTheme, errorBlackTheme, correctBlackTheme,
        playerWhiteTheme, errorWhiteTheme, correctWhiteTheme;


    private void Start()
    {
        keyStates = gameObject.GetComponent<KeyStates>();

        // Get the default configuration for the Theme engine InteractableColorTheme
        playerBlackTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        // Define a color for every state in our Default Interactable States
        playerBlackTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
                new ThemePropertyValue() { Color = Color.black },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.yellow },   // Pressed
                new ThemePropertyValue() { Color = Color.black  },   // Disabled
            };

        errorBlackTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        errorBlackTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
                new ThemePropertyValue() { Color = Color.black },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.red },   // Pressed
                new ThemePropertyValue() { Color = Color.black  },   // Disabled
            };

        correctBlackTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        correctBlackTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
                new ThemePropertyValue() { Color = Color.black },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.cyan },   // Pressed
                new ThemePropertyValue() { Color = Color.black },   // Disabled
            };

        /*************/

        playerWhiteTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        playerWhiteTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
                new ThemePropertyValue() { Color = Color.white },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.yellow },   // Pressed
                new ThemePropertyValue() { Color = Color.white  },   // Disabled
            };

        errorWhiteTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        errorWhiteTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
            /* **** PROBLEME AVEC `new Color(...)` **** */
                new ThemePropertyValue() { Color = Color.white },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.red },   // Pressed
                new ThemePropertyValue() { Color = Color.white  },   // Disabled
            };

        correctWhiteTheme = ThemeDefinition.GetDefaultThemeDefinition<InteractableColorTheme>().Value;
        correctWhiteTheme.StateProperties[0].Values = new List<ThemePropertyValue>()
            {
                new ThemePropertyValue() { Color = Color.white },  // Default
                new ThemePropertyValue() { Color = Color.grey }, // Focus
                new ThemePropertyValue() { Color = Color.cyan },   // Pressed
                new ThemePropertyValue() { Color = Color.white  },   // Disabled
            };

    }

    private void Update()
    {    
    }

    private void setKeyColor(string tag, string keyTheme)
    {
        switch (tag)
        {
            case "error":
                if (keyTheme == "white")
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { errorWhiteTheme })
                        },
                        Target = target,
                    },
                };
                }
                else
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { errorBlackTheme })
                        },
                        Target = target,
                    },
                };
                }
                break;
            case "correct":
                if (keyTheme == "white")
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { correctWhiteTheme })
                        },
                        Target = target,
                    },
                };
                }
                else
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { correctBlackTheme })
                        },
                        Target = target,
                    },
                };
                }
                break;
            case "player":
                if (keyTheme == "white")
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { playerWhiteTheme })
                        },
                        Target = target,
                    },
                };
                }
                else
                {
                    interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>()
                        {
                            Interactable.GetDefaultThemeAsset(new List<ThemeDefinition>() { playerBlackTheme })
                        },
                        Target = target,
                    },
                };
                }
                break;
            default:
                Debug.LogError("Error : missmatch in setKeyColor() function.");
                break;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Sphere "))
        {

            Debug.Log("Collision received : " + collision.transform.name.Replace("Sphere ","") + " hits " + this.transform.name);
            
            if (gameObject.transform.name.Contains("b") && gameObject.transform.name.Contains("#"))
            {
                if (!keyStates.isPlayerMode)
                {
                    setKeyColor("player", "black");
                }
                else if (keyStates._isError)
                {
                    setKeyColor("error", "black");
                }
                else
                {
                    setKeyColor("correct", "black");
                }
            }
            else
            {
                if (!keyStates.isPlayerMode)
                {
                    setKeyColor("player", "white");
                }
                else if (keyStates._isError)
                {
                    setKeyColor("error", "white");
                }
                else
                {
                    setKeyColor("correct", "white");
                }
            }

            interactable.Profiles[0].Themes[0].States = states;
            interactable.SetState(InteractableStates.InteractableStateEnum.Pressed, true);
        }
        else if (collision.transform.name.Contains("A") ||
            collision.transform.name.Contains("B") ||
            collision.transform.name.Contains("C") ||
            collision.transform.name.Contains("D") ||
            collision.transform.name.Contains("E") ||
            collision.transform.name.Contains("F") ||
            collision.transform.name.Contains("G")
            )
        {
            keyStates = gameObject.GetComponent<KeyStates>();
            if (gameObject.transform.name.Contains("/")) {
                string[] tabNames = gameObject.transform.name.Split('/');

                foreach (string s in tabNames)
                {
                    if (s.Equals(collision.transform.name))
                    {
                        Debug.Log("Collision received : " + "'Bloc " + collision.transform.name + "' hits " + this.transform.name);
                        if (gameObject.transform.name.Contains("b") && gameObject.transform.name.Contains("#"))
                        {
                            if (!keyStates.isPlayerMode)
                            {
                                setKeyColor("player", "black");
                            }
                            else if (keyStates._isError)
                            {
                                setKeyColor("error", "black");
                            }
                            else
                            {
                                setKeyColor("correct", "black");
                            }
                        }
                        else
                        {
                            if (!keyStates.isPlayerMode)
                            {
                                setKeyColor("player", "white");
                            }
                            else if (keyStates._isError)
                            {
                                setKeyColor("error", "white");
                            }
                            else
                            {
                                setKeyColor("correct", "white");
                            }
                        }

                        interactable.Profiles[0].Themes[0].States = states;
                        interactable.SetState(InteractableStates.InteractableStateEnum.Pressed, true);
                    }
                }
            }
            else if (gameObject.transform.name.Equals(collision.transform.name))
            {
                Debug.Log("Collision received : " + "'Bloc " + collision.transform.name + "' hits " + this.transform.name);
                interactable.Profiles[0].Themes[0].States = states;
                interactable.SetState(InteractableStates.InteractableStateEnum.Pressed, true);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        interactable.SetState(InteractableStates.InteractableStateEnum.Pressed, false);
    }
}
