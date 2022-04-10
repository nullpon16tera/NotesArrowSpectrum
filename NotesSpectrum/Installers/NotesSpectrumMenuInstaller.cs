using NotesArrowSpectrum.Views;
using SiraUtil;
using Zenject;

namespace NotesArrowSpectrum.Installers
{
    public class NotesSpectrumMenuInstaller : MonoInstaller
    {
        public override void InstallBindings() => this.Container.BindInterfacesAndSelfTo<SettingTabViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
    }
}
