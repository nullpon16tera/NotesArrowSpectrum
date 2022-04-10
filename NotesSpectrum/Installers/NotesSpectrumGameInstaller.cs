using NotesArrowSpectrum.AudioSpectrums;
using NotesArrowSpectrum.Controllers;
using Zenject;

namespace NotesArrowSpectrum.Installers
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
	public class NotesSpectrumGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<AudioSpectrum>().FromNewComponentOn(new UnityEngine.GameObject(nameof(AudioSpectrum))).AsCached();
            this.Container.BindInterfacesAndSelfTo<NotesSpectrumController>().AsCached().NonLazy();
            this.Container.BindInterfacesAndSelfTo<NotesController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
