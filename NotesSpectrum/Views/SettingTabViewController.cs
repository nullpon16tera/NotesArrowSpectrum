using NotesArrowSpectrum.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;


namespace NotesArrowSpectrum.Views
{
    [HotReload]
    internal class SettingTabViewController : BSMLAutomaticViewController, IInitializable
    {
        public string ResourceName => string.Join(".", this.GetType().Namespace, this.GetType().Name);

        [UIValue("enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab("NASpectrum");
            base.OnDestroy();
        }

        public void Initialize() => GameplaySetup.instance.AddTab("NASpectrum", this.ResourceName, this);
    }
}
