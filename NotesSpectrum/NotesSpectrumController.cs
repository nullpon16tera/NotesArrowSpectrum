using NotesArrowSpectrum.AudioSpectrums;
using NotesArrowSpectrum.Configuration;
using NotesArrowSpectrum.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NotesArrowSpectrum
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class NotesSpectrumController : IInitializable, ITickable
    {

        [Inject]
        public void Constructor(AudioSpectrum audioSpectrum, NotesController notesController)
        {
            this._audioSpectrum = audioSpectrum;
            this._audioSpectrum.Band = AudioSpectrum.BandType.ThirtyOneBand;
            this._audioSpectrum.fallSpeed = 0.3f;
            this._audioSpectrum.sensibility = 10f;
            this._audioSpectrum.UpdatedRawSpectums += this.OnUpdateRawSpectrums;
            this._notesController = notesController;
        }

        private AudioSpectrum _audioSpectrum;
        private NotesController _notesController;
        private float duration = 1f;
        private bool _disposedValue;

        private void OnUpdateRawSpectrums(AudioSpectrum obj)
        {
            this.UpdateAudioSpectrums(obj);
        }

        private void UpdateAudioSpectrums(AudioSpectrum audio)
        {
            if (!PluginConfig.Instance.Enable)
            {
                return;
            }
            if (!audio)
            {
                return;
            }

            float amplitude = this._audioSpectrum.PeakLevels[8] * 2 * Mathf.PI;
            this._notesController.SetNoteSpectrum(amplitude, amplitude, 1f, duration);
        }

        public void Initialize()
        {
        }

        public void Tick()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._audioSpectrum.UpdatedRawSpectums -= this.OnUpdateRawSpectrums;
                }
                this._disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
