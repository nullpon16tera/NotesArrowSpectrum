using NotesSpectrum.Views;
using SiraUtil;
using Zenject;

namespace NotesSpectrum.Installers
{
    public class NotesSpectrumMenuInstaller : MonoInstaller
    {
        public override void InstallBindings() => this.Container.BindInterfacesAndSelfTo<SettingTabViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
    }
}
