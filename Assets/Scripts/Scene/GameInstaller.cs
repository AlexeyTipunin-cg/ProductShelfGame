using Assets.Scripts.Audio;
using Assets.Scripts.Products;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] ProductsFactory _productsFactory;
    [SerializeField] RackView _rackView;
    [SerializeField] AudioManager _audioManager;
    public override void InstallBindings()
    {
        Container.Bind<IGameCommands>().To<GameCommands>().AsSingle();
        Container.Bind<IProductFactory>().To<ProductsFactory>().FromComponentInNewPrefab(_productsFactory).AsSingle();
        Container.Bind<ISoundManager>().To<AudioManager>().FromInstance(_audioManager).AsSingle();
        Container.Bind<RackView>().FromInstance(_rackView).AsSingle();
        Container.Bind<SoundController>().AsSingle().NonLazy();
    }
}