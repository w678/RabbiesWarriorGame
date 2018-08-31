// Copyright © Pixel Crushers. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem
{

    [AddComponentMenu("")] // Use wrapper.
    public class StandardDialogueUI : CanvasDialogueUI
    {

        #region Serialized Fields

        public StandardUIAlertControls alertUIElements;
        public StandardUIDialogueControls conversationUIElements;
        public StandardUIQTEControls QTEIndicatorElements;

        #endregion

        #region Properties & Private Fields

        private Queue<QueuedUIAlert> m_alertQueue = new Queue<QueuedUIAlert>();
        private StandardUIRoot m_uiRoot = new StandardUIRoot();
        public override AbstractUIRoot uiRootControls { get { return m_uiRoot; } }
        public override AbstractUIAlertControls alertControls { get { return alertUIElements; } }
        public override AbstractDialogueUIControls dialogueControls { get { return conversationUIElements; } }
        public override AbstractUIQTEControls qteControls { get { return QTEIndicatorElements; } }

        #endregion

        #region Initialization

        /// <summary>
        /// Sets up the component.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            VerifyAssignments();
            conversationUIElements.Initialize();
            alertUIElements.HideImmediate();
            conversationUIElements.HideImmediate();
            QTEIndicatorElements.HideImmediate();
        }

        private void VerifyAssignments()
        {
            UITools.RequireEventSystem();
            if (DialogueDebug.logWarnings)
            {
                if (alertUIElements.alertText.gameObject == null) Debug.LogWarning("Dialogue System: No UI text element is assigned to Standard Dialogue UI's Alert UI Elements.", this);
                if (conversationUIElements.subtitlePanels.Length == 0) Debug.LogWarning("Dialogue System: No subtitle panels are assigned to Standard Dialogue UI.", this);
                if (conversationUIElements.menuPanels.Length == 0) Debug.LogWarning("Dialogue System: No response menu panels are assigned to Standard Dialogue UI.", this);
            }
        }

#if UNITY_5_3 // SceneManager.sceneLoaded wasn't implemented for all Unity 5.3.x versions.
        public void OnLevelWasLoaded(int level)
        {
            UITools.RequireEventSystem();
        }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
#else
        public virtual void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public virtual void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            UITools.RequireEventSystem();
        }
#endif

        public override void Open()
        {
            base.Open();
            conversationUIElements.OpenSubtitlePanelsOnStart();
        }

        #endregion

        #region Update

        public override void Update()
        {
            base.Update();
            UpdateAlertQueue();
        }

        #endregion

        #region Alerts

        public override void ShowAlert(string message, float duration)
        {
            if (alertUIElements.queueAlerts)
            {
                m_alertQueue.Enqueue(new QueuedUIAlert(message, duration));
            }
            else
            {
                base.ShowAlert(message, duration);
            }
        }

        private void UpdateAlertQueue()
        {
            if (alertUIElements.queueAlerts && m_alertQueue.Count > 0 && !alertUIElements.isVisible && !(alertUIElements.waitForHideAnimation && alertUIElements.isHiding))
            {
                ShowNextQueuedAlert();
            }
        }

        private void ShowNextQueuedAlert()
        {
            if (m_alertQueue.Count > 0)
            {
                var queuedAlert = m_alertQueue.Dequeue();
                base.ShowAlert(queuedAlert.message, queuedAlert.duration);
            }
        }

        #endregion

        #region Subtitles

        public override void ShowSubtitle(Subtitle subtitle)
        {
            conversationUIElements.standardMenuControls.Close();
            conversationUIElements.standardSubtitleControls.ShowSubtitle(subtitle);
        }

        public override void HideSubtitle(Subtitle subtitle)
        {
            conversationUIElements.standardSubtitleControls.HideSubtitle(subtitle);
        }

        /// <summary>
        /// Returns the speed of the first typewriter effect found.
        /// </summary>
        public virtual float GetTypewriterSpeed()
        {
            return conversationUIElements.standardSubtitleControls.GetTypewriterSpeed();
        }

        /// <summary>
        /// Sets the speed of all typewriter effects.
        /// </summary>
        /// <param name="charactersPerSecond"></param>
        public virtual void SetTypewriterSpeed(float charactersPerSecond)
        {
            conversationUIElements.standardSubtitleControls.SetTypewriterSpeed(charactersPerSecond);
        }

        #endregion

        #region Response Menu

        public override void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
        {
            conversationUIElements.standardSubtitleControls.UnfocusAll();
            base.ShowResponses(subtitle, responses, timeout);
        }

        #endregion

    }

}
