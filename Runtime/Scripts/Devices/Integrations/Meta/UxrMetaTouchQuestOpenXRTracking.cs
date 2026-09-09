// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UxrMetaTouchQuestOpenXRTracking.cs" company="VRMADA">
//   Copyright (c) VRMADA, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace UltimateXR.Devices.Integrations.Meta
{
    /// <summary>
    ///     Tracking for Meta Quest Touch controllers through Unity's OpenXR Plugin. See
    ///     <see cref="UxrMetaTouchQuestOpenXRInput" /> for why this exists beside
    ///     <see cref="UxrMetaTouchQuest3Tracking" /> rather than reusing it.
    /// </summary>
    public class UxrMetaTouchQuestOpenXRTracking : UxrUnityXRControllerTracking
    {
        #region Public Overrides UxrControllerTracking

        /// <inheritdoc />
        public override Type RelatedControllerInputType => typeof(UxrMetaTouchQuestOpenXRInput);

        #endregion

        #region Public Overrides UxrTrackingDevice

        /// <summary>
        ///     No SDK dependency: this reads <c>UnityEngine.XR.InputDevices</c> through Unity's
        ///     OpenXR Plugin and XR Management, neither of which
        ///     <see cref="UltimateXR.Core.UxrManager" />'s own named SDK constants cover, and this
        ///     project has no Oculus Integration for <see cref="UltimateXR.Core.UxrManager.SdkOculus" />
        ///     to name correctly. Null reads as "no dependency" to the fork's own Editor Inspector
        ///     (<c>Editor/Devices/UxrTrackingEditor.cs</c>), the only place this value is ever read;
        ///     it is not evaluated at runtime anywhere in this package.
        /// </summary>
        public override string SDKDependency => null;

        #endregion
    }
}
