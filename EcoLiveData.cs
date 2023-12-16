using System;
using System.Threading.Tasks;

using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Aliases;
using Eco.Gameplay.GameActions;
using Eco.Gameplay.Property;
using Eco.Plugins.EcoLiveDataExporter;
using Eco.Plugins.EcoLiveDataExporter.ActionsProcessor;
using Eco.Plugins.EcoLiveDataExporter.Utils;
using Eco.Shared.Localization;

public class EcoLiveData : IModKitPlugin, IInitializablePlugin, IShutdownablePlugin, IConfigurablePlugin, IGameActionAware, IWebPlugin
{
    public static string Status = "Not initialized";

    public static readonly Version PluginVersion = new Version(3, 4, 0);
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
        Logger.Info("Plugin EcoLiveData version is " + PluginVersion);
        Config.Instance.Initialize();
        TimerUtil.Instance.RestartTimers();
        ExportUtil.Instance.DumpRecipesAndItemsToDatabase();
        Status = "EcoLiveDataExporter fully initialized!";
        ActionUtil.AddListener(this);
        TradeUtil.Initialize();
    }
    public Task ShutdownAsync()
    {
        ActionUtil.RemoveListener(this);
        Logger.Info("Plugin shutdown");
        Status = "EcoLiveDataExporter fully Shutdown!";
        return Task.CompletedTask;
    }
    public LazyResult ShouldOverrideAuth(IAlias alias, IOwned property, GameAction action)
    {
        return new LazyResult();
    }

    public void ActionPerformed(GameAction action)
    { 
        //Logger.Debug("Action perfomed: " + action.GetType());
        switch (action)
        {
            case CurrencyTrade currencyTrade: TradeActionProcessor.Process(currencyTrade); break;
            case BarterTrade barterTrade: TradeActionProcessor.Process(barterTrade); break;
            case TransferMoney transferMoney: TradeActionProcessor.Process(transferMoney) ; break;
        }
    }

    public string GetCategory() => "EcoWorld Mods";
    public LocString GetMenuTitle()
    {
        return new LocString("Price Calculator");
    }

    public string GetPluginIndexUrl()
    {
        return "EcoLiveData/index.html";
    }

    public string GetFontAwesomeIcon()
    {
        return "fa fa-fw fa-calculator";
    }

    public string GetStaticFilesPath()
    {
        return null;
    }

    public string GetEmbeddedResourceNamespace()
    {
        return "Eco.Plugins.EcoLiveDataExporter.assets";
    }
}