﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;
using KSPPluginFramework;

namespace TransferWindowPlanner
{
    public partial class TransferWindowPlanner
    {
        void OnGUIAppLauncherReady()
        {
            MonoBehaviourExtended.LogFormatted_DebugOnly("AppLauncherReady");
            if (ApplicationLauncher.Ready)
            {
                if (settings.ButtonStyleChosen == Settings.ButtonStyleEnum.Launcher )
                {
                    btnAppLauncher = InitAppLauncherButton();
                }
            }
            else { LogFormatted("App Launcher-Not Actually Ready"); }
        }

        void OnGameSceneLoadRequestedForAppLauncher(GameScenes SceneToLoad)
        {
            LogFormatted_DebugOnly("GameSceneLoadRequest");
            DestroyAppLauncherButton();
        }
        internal ApplicationLauncherButton btnAppLauncher = null;

        internal ApplicationLauncherButton InitAppLauncherButton()
        {
            ApplicationLauncherButton retButton = null;

            try
            {
                retButton = ApplicationLauncher.Instance.AddModApplication(
                    onAppLaunchToggleOn, onAppLaunchToggleOff,
                    onAppLaunchHoverOn, onAppLaunchHoverOff,
                    null, null,
                    ApplicationLauncher.AppScenes.ALWAYS,
                    (Texture)Resources.texAppLaunchIcon);

                //AppLauncherButtonMutuallyExclusive(settings.AppLauncherMutuallyExclusive);

                //appButton = ApplicationLauncher.Instance.AddApplication(
                //    onAppLaunchToggleOn, onAppLaunchToggleOff,
                //    onAppLaunchHoverOn, onAppLaunchHoverOff,
                //    null, null,
                //    (Texture)Resources.texAppLaunchIcon);
                //appButton.VisibleInScenes = ApplicationLauncher.AppScenes.FLIGHT;

            }
            catch (Exception ex)
            {
                MonoBehaviourExtended.LogFormatted("Failed to set up App Launcher Button\r\n{0}",ex.Message);
                retButton = null;
            }
            return retButton;
        }


        internal void DestroyAppLauncherButton()
        {
            if (btnAppLauncher != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(btnAppLauncher);
            }
        }

        void onAppLaunchToggleOn() {
            MonoBehaviourExtended.LogFormatted_DebugOnly("TOn");

            windowMain.Visible = true;
        }
        void onAppLaunchToggleOff() {
            MonoBehaviourExtended.LogFormatted_DebugOnly("TOff");

            windowMain.Visible = false;
        }
        void onAppLaunchHoverOn() {
            MonoBehaviourExtended.LogFormatted_DebugOnly("HovOn");
            MouseOverAppLauncherBtn = true;
        }
        void onAppLaunchHoverOff() {
            MonoBehaviourExtended.LogFormatted_DebugOnly("HovOff");
            MouseOverAppLauncherBtn = false; 
        }

        Boolean MouseOverAppLauncherBtn = false;
    }
}
