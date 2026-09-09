// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UxrMetaTouchQuestOpenXRInput.cs" company="VRMADA">
//   Copyright (c) VRMADA, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace UltimateXR.Devices.Integrations.Meta
{
    /// <summary>
    ///     Meta Quest Touch controller input through Unity's OpenXR Plugin, with no Oculus SDK
    ///     dependency. <see cref="UxrMetaTouchQuest3Input" /> matches "Oculus Touch Controller - Left/Right",
    ///     the legacy Oculus XR Plugin's own device names. A project with no Oculus Integration,
    ///     running OpenXR through Unity XR Management instead, sees different
    ///     <c>UnityEngine.XR.InputDevice.name</c> values: each OpenXR interaction profile feature
    ///     (<c>com.unity.xr.openxr</c>, <c>Runtime/Features/Interactions/*ControllerProfile.cs</c>)
    ///     registers one <c>ActionMapConfig.localizedName</c>, which is what populates the legacy
    ///     XR input device's own <c>name</c>, and that string carries an "OpenXR" suffix rather than
    ///     the Oculus SDK's " - Left"/" - Right" one. Both hands report the SAME name; handedness
    ///     comes from <c>InputDeviceCharacteristics.Left</c>/<c>.Right</c> alone, which is exactly
    ///     what <see cref="UxrUnityXRControllerInput" />'s own <c>IsLeftController</c>/
    ///     <c>IsRightController</c> already check.
    /// </summary>
    public class UxrMetaTouchQuestOpenXRInput : UxrUnityXRControllerInput
    {
        #region Public Overrides UxrControllerInput

        /// <inheritdoc />
        public override UxrControllerSetupType SetupType => UxrControllerSetupType.Dual;

        /// <inheritdoc />
        public override bool IsHandednessSupported => true;

        /// <inheritdoc />
        public override bool MainJoystickIsTouchpad => false;

        /// <inheritdoc />
        public override bool HasControllerElements(UxrHandSide handSide, UxrControllerElements controllerElements)
        {
            uint validElements = (uint)(UxrControllerElements.Joystick |
                                        UxrControllerElements.Grip |
                                        UxrControllerElements.Trigger |
                                        UxrControllerElements.ThumbCapSense |
                                        UxrControllerElements.Button1 |
                                        UxrControllerElements.Button2 |
                                        UxrControllerElements.Menu |
                                        UxrControllerElements.DPad);

            if (handSide == UxrHandSide.Right)
            {
                // Remove menu button from right controller, which is reserved.
                validElements = validElements & ~(uint)UxrControllerElements.Menu;
            }

            return (validElements & (uint)controllerElements) == (uint)controllerElements;
        }

        #endregion

        #region Public Overrides UxrUnityXRControllerInput

        /// <summary>
        ///     Every OpenXR interaction profile's own <c>ActionMapConfig.localizedName</c> a Quest
        ///     3's Touch controllers could bind to (source: this project's
        ///     <c>com.unity.xr.openxr@1.17.1</c> package), not a single guessed name: the runtime
        ///     picks whichever interaction profile it and the controller actually negotiate, and
        ///     all three of these are enabled in this project's own OpenXR Package Settings
        ///     (Touch Plus is the controller Quest 3 ships with; Touch Pro and the generic Oculus
        ///     Touch profile are enabled fallbacks). Confirmed by reading the package source
        ///     directly, not assumed: "Meta Quest Touch Plus Controller OpenXR"
        ///     (<c>MetaQuestTouchPlusControllerProfile.cs</c>), "Meta Quest Pro Touch Controller OpenXR"
        ///     (<c>MetaQuestTouchProControllerProfile.cs</c>), "Oculus Touch Controller OpenXR"
        ///     (<c>OculusTouchControllerProfile.cs</c>).
        /// </summary>
        public override IEnumerable<string> ControllerNames
        {
            get
            {
                yield return "Meta Quest Touch Plus Controller OpenXR";
                yield return "Meta Quest Pro Touch Controller OpenXR";
                yield return "Oculus Touch Controller OpenXR";
            }
        }

        #endregion
    }
}
