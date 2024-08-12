using BepInEx;
using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq.Expressions;
using Oculus.Interaction.Deprecated;
using System.Text;
using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using static Mono.Math.BigInteger;
using static NetworkSystem;
using Utilla;
using static Misc.Variables;
using Thermo_Template.Misc;
using Misc;
using static Thermo_Template.Misc.Customize;
using static ThermoTemplate.Misc.Buttons;
using Thermo_Template.Mods;
using static Thermo_Template.Mods.ActualMods;
using Patches;


namespace Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        private static void Prefix()
        {
            try
            {
                if (TriggerPages || GripPages)
                {
                    var InputForward = TriggerPages ? InputLib.RT() : InputLib.RG();
                    var InputBackward = TriggerPages ? InputLib.LT() : InputLib.LG();
                    if (InputForward && Time.time > TriggerGripTime + 0.5f)
                    {
                        TriggerGripTime = Time.time;
                        Toggle("NextPage");
                    }
                    if (InputBackward && Time.time > TriggerGripTime + 0.5f)
                    {
                        TriggerGripTime = Time.time;
                        Toggle("PreviousPage");
                    }
                }
                OpenInput = InputLib.Y();
                if (menu == null)
                {
                    if (OpenInput)
                    {
                        CreateMenu();
                        RepositionMenu();
                        if (reference == null)
                        {
                            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
                            reference.GetComponent<Renderer>().material = Uber;
                            reference.GetComponent<Renderer>().material.color = Color.black;
                            reference.transform.localPosition = new Vector3(0f, -0.1f, -0.12f);
                            reference.transform.localScale = new Vector3(0.023f, 0.023f, 0.023f);
                            buttonCollider = reference.GetComponent<SphereCollider>();
                        }
                    }
                }
                else
                {
                    if (OpenInput)
                    {
                        RepositionMenu();
                    }
                    else if (!OpenInput)
                    {
                        GameObject.Destroy(menu);
                        menu = null;

                        GameObject.Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", Sigma.PluginInfo.Name, exc.StackTrace, exc.Message));
            }
            try
            {
                foreach (ButtonHelper[] buttonlist in buttons)
                {
                    foreach (ButtonHelper v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.ExecutePath != null)
                            {
                                try
                                {
                                    v.ExecutePath.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", Sigma.PluginInfo.Name, v.String, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", Sigma.PluginInfo.Name, exc.StackTrace, exc.Message));
            }
        }
        public static Color BorderColor = new Color32(117, 0, 0, byte.MaxValue);
        public static void CreateMenu()
        {

            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            BG = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(BG.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(BG.GetComponent<BoxCollider>());
            BG.transform.parent = menu.transform;
            BG.transform.rotation = Quaternion.identity;
            BG.transform.localScale = new Vector3(0.1f, 1f, 1f);
            BG.transform.position = new Vector3(0.05f, 0f, 0f);


            GameObject borderobj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(borderobj1.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(borderobj1.GetComponent<BoxCollider>());
            borderobj1.transform.parent = menu.transform;
            borderobj1.transform.rotation = Quaternion.identity;
            borderobj1.name = "Right Border";
            borderobj1.GetComponent<MeshRenderer>().material.color = BorderColor;
            borderobj1.transform.localPosition = new Vector3(0.5f, -0.5f, 0f);
            borderobj1.transform.localScale = new Vector3(0.17f, 0.02f, 1f);


            // Left border
            GameObject borderobj2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(borderobj2.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(borderobj2.GetComponent<BoxCollider>());
            borderobj2.transform.parent = menu.transform;
            borderobj2.transform.rotation = Quaternion.identity;
            borderobj2.name = "Left Border";
            borderobj2.GetComponent<MeshRenderer>().material.color = BorderColor;
            borderobj2.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
            borderobj2.transform.localScale = new Vector3(0.17f, 0.02f, 1f);


            // bottom border
            GameObject borderobj3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(borderobj3.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(borderobj3.GetComponent<BoxCollider>());
            borderobj3.transform.parent = menu.transform;
            borderobj3.transform.rotation = Quaternion.identity;
            borderobj3.name = "Bottom Border";
            borderobj3.GetComponent<MeshRenderer>().material.color = BorderColor;
            borderobj3.transform.localPosition = new Vector3(0.5f, 0.001f, -0.5f);
            borderobj3.transform.localScale = new Vector3(0.17f, 1.02f, 0.01f);

            // top border
            GameObject borderobj4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(borderobj4.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(borderobj4.GetComponent<BoxCollider>());
            borderobj4.transform.parent = menu.transform;
            borderobj4.transform.rotation = Quaternion.identity;
            borderobj4.name = "Top Border";
            borderobj4.GetComponent<MeshRenderer>().material.color = BorderColor;
            borderobj4.transform.localPosition = new Vector3(0.5f, 0.001f, 0.5f);
            borderobj4.transform.localScale = new Vector3(0.17f, 1.02f, 0.01f);





            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;


            text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.color = Color.white;
            text.text = $"{MenuTitle}";
            text.fontSize = 1;
            text.color = Color.white;
            text.supportRichText = true;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (LayoutIndex == 0)
            {
                if (TabIndex > 0)
                {
                    AddPages("<"); // Prev page
                    AddPages(">"); // Next page
                }
            }
            else if (LayoutIndex == 1)
            {
                TriggerPages = true;
                GripPages = false;
                GetIndex("Change Layout : Normal").String = "Change Layout : Triggers";
            }
            else if (LayoutIndex == 2)
            {
                GripPages = true;
                TriggerPages = false;
                GetIndex("Change Layout : Triggers").String = "Change Layout : Grips";
            }
            else if (LayoutIndex >= 3)
            {
                if (TabIndex > 0)
                {
                    AddPages("<"); // Prev page
                    AddPages(">"); // Next page
                }
                GetIndex("Change Layout : Grips").String = "Change Layout : Normal";
                LayoutIndex = 0;
            }
          
            AddReturnButtonWhenInTabs();


            var btnser = buttons[TabIndex].Skip(pageNumber * PageSize).Take(PageSize).ToArray();
            for (int i = 0; i < btnser.Length; i++)
            {
                AddButtons(i * 0.1f, btnser[i]);
            }
        }
        public static Text text;
     
        public static void AddPages(string Side)
        {
            if (Side.Contains(">") || Side == ">")
            {
                //if (pageNumber != LastPage)
                {
                    GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                    {
                        gameObject2.layer = 2;
                    }
                    UnityEngine.Object.Destroy(gameObject2.GetComponent<Rigidbody>());
                    gameObject2.GetComponent<BoxCollider>().isTrigger = true;
                    gameObject2.transform.parent = menu.transform;
                    gameObject2.name = "Next Page";
                    gameObject2.transform.rotation = Quaternion.identity;
                    gameObject2.transform.localScale = new Vector3(0.09f, 0.13f, 0.93f);
                    gameObject2.transform.localPosition = new Vector3(0.66f, -0.68f, 0);
                    gameObject2.GetComponent<MeshRenderer>().material.color = ButtonColor;
                    gameObject2.AddComponent<BtnCollider>().relatedText = "NextPage";
                }
            }
            if (Side.Contains("<") || Side == "<")
            {
                if (pageNumber > 0)
                {
                    GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                    {
                        gameObject3.layer = 2;
                    }
                    UnityEngine.Object.Destroy(gameObject3.GetComponent<Rigidbody>());
                    gameObject3.GetComponent<BoxCollider>().isTrigger = true;
                    gameObject3.transform.parent = menu.transform;
                    gameObject3.name = "Previous Page";
                    gameObject3.transform.rotation = Quaternion.identity;
                    gameObject3.transform.localScale = new Vector3(0.09f, 0.13f, 0.93f);
                    gameObject3.transform.localPosition = new Vector3(0.66f, 0.68f, 0);
                    gameObject3.GetComponent<MeshRenderer>().material.color = ButtonColor;
                    gameObject3.AddComponent<BtnCollider>().relatedText = "PreviousPage";
                }
            }
            
        }
        public static void AddReturnButtonWhenInTabs()
        {
            if (TabIndex > 0)
            {
                GameObject Returner = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    Returner.layer = 2;
                }
                UnityEngine.Object.Destroy(Returner.GetComponent<Rigidbody>());
                Returner.GetComponent<BoxCollider>().isTrigger = true;
                Returner.transform.parent = menu.transform;
                Returner.transform.rotation = Quaternion.identity;
                Returner.transform.localScale = new Vector3(0.09f, 0.93f, 0.1f);
                Returner.transform.localPosition = new Vector3(0.56f, 0, -0.6f);
                Returner.GetComponent<Renderer>().material.color = Color.gray * Color.black * Color.gray * 0.5f;
                Returner.AddComponent<BtnCollider>().relatedText = "ReturnToHome";
                Text text = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Text>();
                text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
                text.text = "Return";
                text.supportRichText = true;
                text.fontSize = 1;
                text.alignment = TextAnchor.MiddleCenter;
                text.fontStyle = FontStyle.Bold;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(.2f, .03f);
                component.localPosition = new Vector3(.064f, 0f, -.231f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
        }
        public static void RepositionMenu()
        {
            menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
        }
        public static void AddButtons(float offset, ButtonHelper method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.88f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<BtnCollider>().relatedText = method.String;
            if (method.enabled)
            {
                gameObject.GetComponent<Renderer>().material.color = ButtonColorEnabled;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = ButtonColor;
            }
            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.text = method.String;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.BoldAndItalic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
            }
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = ((buttons[TabIndex].Length + PageSize - 1) / PageSize) - 1;
            if (buttonText == "ReturnToHome")
            {
                EnterThings.EnterTab(0);
                pageNumber = 0;
            }
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                }
                else
                {
                    ButtonHelper target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.Tog)
                        {

                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            if (target.ExecutePath != null)
                            {
                                try { target.ExecutePath.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }
        public static ButtonHelper GetIndex(string buttonText)
        {
            foreach (ButtonHelper[] buttons in ThermoTemplate.Misc.Buttons.buttons)
            {
                foreach (ButtonHelper button in buttons)
                {
                    if (button.String == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

    }
    internal class BtnCollider : MonoBehaviour
    {
        public string relatedText;

        public static float buttonCooldown = 0f;

        public void OnTriggerEnter(Collider collider)
        {
            if (Time.time > buttonCooldown && collider == buttonCollider && menu != null)
            {
                buttonCooldown = Time.time + 0.2f;
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(64, false, 0.4f);
                Main.Toggle(relatedText);
            }
        }
    }


}
