using Classes;
using GorillaNetworking;
using Menu;
using Meta.WitAi;
using Misc;
using Photon.Pun;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermo_Template.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using static gs.PointSetHashtable;
using static Misc.Variables;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;
using static Thermo_Template.Misc.InputLib;

namespace Thermo_Template.Mods
{
    internal class ActualMods
    {
        public static void DisconnectOnX()
        {
            if (InputLib.X())
            {
                PhotonNetwork.Disconnect();
            }
        }

        public static void NoClip()
        {
            bool disablecolliders = ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f;
            MeshCollider[] colliders = Resources.FindObjectsOfTypeAll<MeshCollider>();

            foreach (MeshCollider collider in colliders)
            {
                collider.enabled = !disablecolliders;
            }
        }


        public static void HoldRig()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                return;
            }
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void StareAtNearest()
        {
            GorillaTagger.Instance.offlineVRRig.headConstraint.LookAt(RigManager.GetClosestVRRig().headMesh.transform.position);
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.LookAt(RigManager.GetClosestVRRig().headMesh.transform.position);
        }

        public static void NoGrav()
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
        }

        public static void HighGrav()
        {
            Physics.gravity = new Vector3(0f, -15f, 0f);
        }

        public static void NormalGrav()
        {
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
        }

        public static void BrokenNeck()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 90f;
        }

        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 50f;
        }
        public static void FixHeadV2()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }
        public static void WeirdGrav()
        {
            Physics.gravity = new Vector3(9f, 9f, 9f);
        }

        public static void UnlockComp()
        {
            GorillaComputer.instance.CompQueueUnlockButtonPress();
        }


        public static void CarMonke()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 20f;
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position -= GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
            }
        }

        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }







        public static void PbbvWalk()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }
        public static void DisconnectIsSigma()
        {
            PhotonNetwork.Disconnect();
        }
        public static void ChangeLayout()
        {
            LayoutIndex++;
            Main.RecreateMenu();
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
        public static void BigBug()
        {
            GameObject.Find("Floating Bug Holdable").transform.localScale = new Vector3(3f, 3f, 3f);
        }
        public static void BigBat()
        {
            GameObject.Find("Cave Bat Holdable").transform.localScale = new Vector3(3f, 3f, 3f);
        }



        public static void VeryBigBug()
        {
            GameObject.Find("Floating Bug Holdable").transform.localScale = new Vector3(10f, 10f, 10f);
        }

        public static void VeryBigBat()
        {
            GameObject.Find("Cave Bat Holdable").transform.localScale = new Vector3(10f, 10f, 10f);
        }
        public static void MosaBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 2.5f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 4.5f;
        }

        public static void SpeedBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 4.5f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 5f;
        }
        public static void FastestSpeedBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 999999.99f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 999999.99f;
        }
        
        public static void Helicopter()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton || ControllerInputPoller.instance.rightControllerPrimaryButton || Mouse.current.leftButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position += new Vector3(0f, 0.075f, 0f);
                GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10f, 0f));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
                return;
            }
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }




        public static void DestroyBug()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = new Vector3(-0, -0, -0);
            }
        }
        public static void DestroyBat()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = new Vector3(-0, -0, -0);
            }
        }


      
        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void GrabBugRHand()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
                // Owner ship
            }
        }

       
        public static void GrabBugLHand()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.leftHandTransform.position;
                // Owner ship
            }
        }
      
        public static void GrabBatRHand()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
                // Owner ship
            }
        }
        public static void GrabBatLHand()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.leftHandTransform.position;
                // Owner ship
            }
        }

        public static void BugHat()
        {
            GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.headCollider.transform.position;
        }

        public static void BatHat()
        {
            GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.headCollider.transform.position;
        }

        public static void RideBug()
        {
            GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
            GameObject.Find("Floating Bug Holdable").transform.localScale = new Vector3(3f, 3f, 3f);
        }

        public static void RideBat()
        {
            GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.bodyCollider.transform.position;
            GameObject.Find("Cave Bat Holdable").transform.localScale = new Vector3(3f, 3f, 3f);
        }

        public static void MergeBugAndBatR()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }

        public static void MergeBugAndBatL()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.leftHandTransform.position;
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        public static void NoFinger()
        {
            ControllerInputPoller.instance.leftControllerGripFloat = 0f;
            ControllerInputPoller.instance.rightControllerGripFloat = 0f;
            ControllerInputPoller.instance.leftControllerIndexFloat = 0f;
            ControllerInputPoller.instance.rightControllerIndexFloat = 0f;
            ControllerInputPoller.instance.leftControllerPrimaryButton = false;
            ControllerInputPoller.instance.leftControllerSecondaryButton = false;
            ControllerInputPoller.instance.rightControllerPrimaryButton = false;
            ControllerInputPoller.instance.rightControllerSecondaryButton = false;
            ControllerInputPoller.instance.leftControllerPrimaryButtonTouch = false;
            ControllerInputPoller.instance.leftControllerSecondaryButtonTouch = false;
            ControllerInputPoller.instance.rightControllerPrimaryButtonTouch = false;
            ControllerInputPoller.instance.rightControllerSecondaryButtonTouch = false;
        }

        private static int theme = 0;

        public static void ChangeTheme()
        {
            theme++;
            if (theme >= 6)
            {
                theme = 0;
            }
            switch (theme)
            {
                case 0: Main.BorderColor = new Color32(117, 0, 0, byte.MaxValue); break;
                case 2: Main.BorderColor = Color.white; break;
                case 3: Main.BorderColor = Color.grey; break;
                case 4: Main.BorderColor = Color.green; break;
                case 5: Main.BorderColor = Color.magenta; break;
                case 6: Main.BorderColor = Color.blue; break;
                case 7: Main.BorderColor = Color.cyan; break;
            }
        }


        private static int buttoncolor = 0;

        public static void ChangeButtonColor()
        {
            buttoncolor++;
            if (buttoncolor >= 6)
            {
                buttoncolor = 0;
            }
            switch (buttoncolor)
            {
                case 0: Customize.ButtonColor = new Color32(117, 0, 0, byte.MaxValue + byte.MinValue); break;
                case 2: Customize.ButtonColor = Color.white; break;
                case 3: Customize.ButtonColor = Color.grey; break;
                case 4: Customize.ButtonColor = Color.green; break;
                case 5: Customize.ButtonColor = Color.magenta; break;
                case 6: Customize.ButtonColor = Color.blue; break;
                case 7: Customize.ButtonColor = Color.cyan; break;
            }
        }

        
            
            public static void TakeScreenShot()
        {
            SteamScreenshots.TriggerScreenshot();
        }

        public static void SuperMonke()
        {
            if (B() || A())
            {
                GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * 15 * Time.deltaTime;
                GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
            }
        }

        public static void LoudHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 99;
        }
        public static void NoHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0;
        }

        public static void LoudestHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 99999999;
        }

        public static void MidHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 6;
        }
        public static void FastFly()
        {
            if (B() || A())
            {
                GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * 35 * Time.deltaTime;
                GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
            }
        }
        static GameObject rrr;
        static GameObject lll;
        public static void Platformers(bool invis)
        {
            if (InputLib.RG() || InputLib.LG())
            {
                if (InputLib.RG() && rrr == null)
                {
                    rrr = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    rrr.transform.localScale = new Vector3(3f, 0.01f,3f);
                    rrr.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    rrr.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    if (!invis)
                    {
                        rrr.GetComponent<Renderer>().material = Variables.Uber;
                        rrr.GetComponent<Renderer>().material.color = Color.black;
                    }
                    else
                    {
                        GameObject.Destroy(rrr.GetComponent<Renderer>());
                        GameObject.Destroy(rrr.GetComponent<MeshRenderer>());
                    }
                   

                    GameObject.Destroy(rrr.GetComponent<Rigidbody>());
                    GameObject.Destroy(rrr.GetComponent<Rigidbody2D>());
                }
                if (InputLib.LG() && lll == null)
                {
                    lll = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    lll.transform.localScale = new Vector3(0.1f, 0.01f, 0.01f);
                    lll.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    lll.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    if (!invis)
                    {
                        lll.GetComponent<Renderer>().material = Variables.Uber;
                        lll.GetComponent<Renderer>().material.color = Color.black;
                    }
                    else
                    {
                        GameObject.Destroy(lll.GetComponent<Renderer>());
                        GameObject.Destroy(lll.GetComponent<MeshRenderer>());
                    }

                    GameObject.Destroy(lll.GetComponent<Rigidbody>());
                    GameObject.Destroy(lll.GetComponent<Rigidbody2D>());
                }
            }
            if (!InputLib.RG())
            {
                GameObject.Destroy(rrr);
                rrr = null;
            }
            if (!InputLib.LG())
            {
                GameObject.Destroy(lll);
                lll = null;
            }
        }
        public static void TpGunModule(Vector3 postioototntntnnt)
        {
            if (Time.time > Variables.TpCooldown + 0.5f)
            {
                Variables.TpCooldown = Time.time;
                GorillaLocomotion.Player.Instance.transform.position = postioototntntnnt;
            }
        }

        
       

        public static void TpGun()
        {
            GunLib.Gun(() => TpGunModule(GunLib.p.transform.position));
        }
        private static void TagGunModule(GameObject p) 
        {
            foreach (VRRig o in GorillaParent.instance.vrrigs) 
            {
                bool stuff = Vector3.Distance(p.transform.position, o.transform.position) < 0.5f && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected") && !o.mainSkin.material.name.Contains("fected") && o!=GorillaTagger.Instance.offlineVRRig;
                if (stuff) 
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = o.transform.position - new Vector3(0,2,0);
                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = o.headMesh.transform.position;
                    RPC();
                }
                else 
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

        
         

        public static void TagGun()
        {
            GunLib.Gun(() => TagGunModule(GunLib.p));
        }
        private static void RPC() 
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig);
            PhotonNetwork.OpCleanActorRpcBuffer(GorillaTagger.Instance.myVRRig.Controller.ActorNumber);
            PhotonNetwork.SendAllOutgoingCommands();
            GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
        }

    }
}
