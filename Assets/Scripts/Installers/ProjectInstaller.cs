using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ContentLoader>().AsSingle().NonLazy();
    }
}
