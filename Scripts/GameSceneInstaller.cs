using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private TickSetter tickSetter;
    [SerializeField] private ItemsManager itemsManager;
    [SerializeField] private GameEnder gameEnder;
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelStarter levelStarter;
    [SerializeField] private LightActivator lightActivator;
    [SerializeField] private BreakManager breakManager;

    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().FromInstance(settings).AsSingle();
        Container.Bind<TickSetter>().FromInstance(tickSetter).AsSingle();
        Container.Bind<BreakManager>().FromInstance(breakManager).AsSingle();
        Container.Bind<GameEnder>().FromInstance(gameEnder).AsSingle();
        Container.Bind<ItemsManager>().FromInstance(itemsManager).AsSingle();
        Container.Bind<PlayerController>().FromInstance(player).AsSingle();
        Container.Bind<LightActivator>().FromInstance(lightActivator).AsSingle();

        Container.Bind<LevelStarter>().FromInstance(levelStarter).AsSingle();
    }
}
