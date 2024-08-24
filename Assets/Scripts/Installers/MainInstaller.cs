using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ScreenText _screenText;
    [SerializeField] private ErrorHandlingService _errorHandlingService;
    
    private readonly IPseudoAPI _pseudoApi = new PseudoAPI();
    
    
    public override void InstallBindings()
    {
        Container.Bind<IPseudoAPI>()
            .FromInstance(_pseudoApi)
            .AsSingle()
            .NonLazy();

        Container.Bind<ScreenText>()
            .FromInstance(_screenText)
            .AsSingle()
            .NonLazy();

        Container.Bind<IErrorHandlingService>()
            .FromInstance(_errorHandlingService)
            .AsSingle()
            .NonLazy();
    }
}
