using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private LoadingScreen loadingScreen;

    public override void InstallBindings()
    {
        Container.Bind<LoadingScreen>().FromInstance(loadingScreen).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ContentLoader>().AsSingle().NonLazy();
    }
}
