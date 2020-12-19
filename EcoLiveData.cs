using System;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.GameActions;
using Eco.Plugins.EcoLiveDataExporter;
using Eco.Plugins.EcoLiveDataExporter.ActionsProcessor;
using Eco.Plugins.EcoLiveDataExporter.Utils;
public class EcoLiveData : IModKitPlugin, IInitializablePlugin, IConfigurablePlugin, IGameActionAware
{
    public static string Status = "Not initialized";
    public readonly Version PluginVersion = new Version(1, 0, 0);
    public IPluginConfig PluginConfig => Config.Instance.PluginConfig;

    public ThreadSafeAction<object, string> ParamChanged { get; set; }

    public object GetEditObject() => Config.Data;

    public string GetStatus() => Status;

    public void OnEditObjectChanged(object o, string param)
    {
        Logger.Debug("Plugin configuration changed");
    }

    public void Initialize(TimedTask timer)
    {
        Logger.Info("Plugin version is " + PluginVersion);
        Config.Instance.Initialize();
        Status = "EcoLiveDataExporter fully initialized!";
    }

    public Result ShouldOverrideAuth(GameAction action)
    {
        return new Result(ResultType.None);
    }

    public void ActionPerformed(GameAction action)
    {
        Logger.Debug("Action perfomed: " + action.GetType());
        switch (action)
        {
            case CurrencyTrade currencyTrade: TradeActionProcessor.Process(currencyTrade); break;
        }
    }
}