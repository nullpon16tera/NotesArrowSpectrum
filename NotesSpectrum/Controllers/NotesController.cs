using NotesSpectrum.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Loader;
using UnityEngine;
using Zenject;

namespace NotesSpectrum.Controllers
{
    public class NotesController : MonoBehaviour, IInitializable
    {
        private BasicBeatmapObjectManager basicBeatmapObjectManager;

        private ConditionalWeakTable<GameNoteController, ColorNoteVisuals> noteVisualsMap = new ConditionalWeakTable<GameNoteController, ColorNoteVisuals>();

        private float disableNoteSpectrumOn = -1f;

        [Inject]
        public void Constructor(BasicBeatmapObjectManager basicBeatmapObjectManager)
        {
            this.basicBeatmapObjectManager = basicBeatmapObjectManager;
        }

        public void Initialize()
        {

        }

        public void SetNoteSpectrum(float? widthNullable, float? heightNullable, float? offsetNullable, float? duration = null)
        {
            float width = widthNullable ?? 1f;
            float height = heightNullable ?? 1f;
            float offset = offsetNullable ?? 1f;

            UpdateNoteSpectrum(width, height, offset);

            disableNoteSpectrumOn = duration.HasValue ? Time.time + duration.Value : -1f;

            SetEnabled();
        }

        public void DisableNoteSpectrum()
        {
            if (enabled)
            {
                disableNoteSpectrumOn = -1f;
                SetEnabled();
            }

            UpdateNoteSpectrum(1f, 1f, 1f);
        }

        private void UpdateNoteSpectrum(float width, float height, float offset)
        {
            List<GameNoteController> notes = ReflectionUtil.GetPrivateField<MemoryPoolContainer<GameNoteController>>(basicBeatmapObjectManager, "_basicGameNotePoolContainer").activeItems;

            foreach (GameNoteController note in notes)
            {
                if (!noteVisualsMap.TryGetValue(note, out ColorNoteVisuals noteVisuals))
                {
                    noteVisualsMap.Add(note, noteVisuals = note.GetComponent<ColorNoteVisuals>());
                }

                float noteWidth = 0.8f + width * 1.05f;
                float noteHeight = 0.8f + width * 1.05f;
                note.transform.localScale = new Vector3(noteWidth, noteHeight, offset);

                float arrowWidth = 0.5f + width * 1.2f;
                float arrowHeight = 0.5f + height * 1.2f;
                MeshRenderer[] arrowMeshRenderers = ReflectionUtil.GetPrivateField<MeshRenderer[]>(noteVisuals, "_arrowMeshRenderers");
                foreach (MeshRenderer arrowRenderer in arrowMeshRenderers)
                {
                    if (arrowRenderer.name == "NoteArrow")
                    {
                        arrowRenderer.gameObject.transform.localScale = new Vector3(arrowWidth, arrowHeight, offset);
                    }
                }

                float circleWidth = 0.5f + width / 1.5f;
                float circleHeight = 0.5f + height / 1.5f;
                MeshRenderer[] circleMeshRenderers = ReflectionUtil.GetPrivateField<MeshRenderer[]>(noteVisuals, "_circleMeshRenderers");
                foreach (MeshRenderer circleRenderer in circleMeshRenderers)
                {
                    circleRenderer.gameObject.transform.localScale = new Vector3(circleWidth, circleHeight, offset);
                }
            }
        }

        public void Reset()
        {
            DisableNoteSpectrum();
        }

        private void SetEnabled()
        {
            enabled = disableNoteSpectrumOn != -1f;
        }
    }
}
