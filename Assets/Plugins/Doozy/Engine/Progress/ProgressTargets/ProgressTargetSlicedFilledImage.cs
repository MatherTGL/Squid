// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using Doozy.Engine.Utils;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Doozy.Engine.Progress
{
    /// <inheritdoc />
    /// <summary>
    /// Used by a Progressor to update the fillAmount value of an Image component
    /// </summary>
    //[AddComponentMenu(MenuUtils.ProgressTargetSFImage_AddComponentMenu_MenuName, MenuUtils.ProgressTargetSFImage_AddComponentMenu_Order)]
    [DefaultExecutionOrder(DoozyExecutionOrder.PROGRESS_TARGET_IMAGE)]
    public class ProgressTargetSlicedFilledImage : ProgressTarget
    {
        #region UNITY_EDITOR

#if UNITY_EDITOR
        //[MenuItem(MenuUtils.ProgressTargetSFImage_MenuItem_ItemName, false, MenuUtils.ProgressTargetSFImage_MenuItem_Priority)]
        private static void CreateComponent(MenuCommand menuCommand) { DoozyUtils.AddToScene<ProgressTargetImage>(MenuUtils.ProgressTargetImage_GameObject_Name, false, true); }
#endif

        #endregion
        
        #region Public Variables
        
        /// <summary> Target Image component </summary>
        public SlicedFilledImage Image;
        
        /// <summary> Progress direction to be used (Progress or InverseProgress) </summary>
        public TargetProgress TargetProgress;
        
        #endregion
        
        #region Public Methods
        
        /// <inheritdoc />
        /// <summary> Method used by a Progressor to when the current Value has changed </summary>
        /// <param name="progressor"> The Progressor that triggered this update </param>
        public override void UpdateTarget(Progressor progressor)
        {
            base.UpdateTarget(progressor);
            
            //if(Image == null) return;
            Image.fillAmount = TargetProgress == TargetProgress.Progress ? progressor.Progress : progressor.InverseProgress;
        }
        
        #endregion

        #region Private Methods

        private void Reset() { Image = GetComponent<SlicedFilledImage>(); }

        #endregion
    }
}