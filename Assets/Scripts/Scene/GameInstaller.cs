using Assets.Scripts.Products;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] ProductsFactory _productsFactory;
    [SerializeField] RackView _rackView;
    public override void InstallBindings()
    {
        Container.Bind<GameCommands>().AsSingle();
        Container.Bind<IProductFactory>().To<ProductsFactory>().FromComponentInNewPrefab(_productsFactory).AsSingle();
        Container.Bind<RackView>().FromInstance(_rackView).AsSingle();
    }
}