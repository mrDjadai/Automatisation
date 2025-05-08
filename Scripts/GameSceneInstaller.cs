using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private TickSetter tickSetter;

    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().FromInstance(settings).AsSingle();
        Container.Bind<TickSetter>().FromInstance(tickSetter).AsSingle();
    }
}
