using System.Text.Json;
using VoidWalker.Engine.Core.Data.Events.Data;
using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Types;
using VoidWalker.Engine.Server.Utils;


namespace VoidWalker.Engine.Server.Services;

public class DataLoaderService : BaseVoidWalkerService, IDataLoaderService
{
    private readonly Dictionary<string, Type> _dataTypes = new();

    private readonly Dictionary<string, List<Action<List<object>>>> _dataLoadedSubscribers = new();

    private const string _typeProperty = "$type";

    private readonly IMessageBusService _messageBusService;


    private readonly Dictionary<string, List<object>> _loadedData = new();

    public DataLoaderService(
        ILogger<DataLoaderService> logger, List<JsonMapTypeData> jsonMaps, IMessageBusService messageBusService
    ) : base(
        logger
    )
    {
        _messageBusService = messageBusService;

        foreach (var jsonMap in jsonMaps)
        {
            _dataTypes.Add(jsonMap.Name, jsonMap.Type);
        }
    }

    public override async Task InitializeAsync()
    {
        var dataFiles = DirectoriesUtils.GetFiles(DirectoryType.Data, "*.json").ToList();

        Logger.LogInformation("Loading data files: {DataFiles}", dataFiles.Count);

        foreach (var dataFile in dataFiles)
        {
            await LoadDataAsync(dataFile);
        }

        await BroadcastDataLoadedMessage();
    }

    public void SubscribeToDataLoaded<T>(string type, Action<List<T>> callBack) where T : class
    {
        if (_loadedData.ContainsKey(type))
        {
            if (_dataLoadedSubscribers.ContainsKey(type))
            {
                _dataLoadedSubscribers[type].Add(callBack as Action<List<object>>);
            }
            else
            {
                _dataLoadedSubscribers.Add(
                    type,
                    new List<Action<List<object>>>
                    {
                        callBack as Action<List<object>>
                    }
                );
            }

            callBack(_loadedData[type].Cast<T>().ToList());
        }
    }

    private async Task BroadcastDataLoadedMessage()
    {
        foreach (var loadedData in _loadedData)
        {
            _messageBusService.Publish(new DataLoadedEvent(loadedData.Key, loadedData.Value));
        }

        _dataLoadedSubscribers.ToList()
            .ForEach(
                subscriber =>
                {
                    if (_loadedData.ContainsKey(subscriber.Key))
                    {
                        subscriber.Value.ForEach(action => action(_loadedData[subscriber.Key]));
                    }
                }
            );
    }

    private void ProcessElement(JsonElement element)
    {
        if (element.TryGetProperty(_typeProperty, out var typeProperty))
        {
            Logger.LogDebug("Processing element with type {Type}", typeProperty.GetString());
            var typeToDeserialize = _dataTypes[typeProperty.GetString()];
            var data = JsonSerializer.Deserialize(element.GetRawText(), typeToDeserialize);

            if (_loadedData.ContainsKey(typeProperty.GetString()))
            {
                _loadedData[typeProperty.GetString()].Add(data);
            }
            else
            {
                _loadedData.Add(typeProperty.GetString(), new List<object> { data });
            }
        }
        else
        {
            Logger.LogWarning("Type property not found in {DataFile}", element.GetRawText());
        }
    }

    private async Task LoadDataAsync(string dataFile)
    {
        var fileStream = File.OpenRead(dataFile);
        var json = (await JsonDocument.ParseAsync(fileStream));

        if (json.RootElement.ValueKind == JsonValueKind.Object)
        {
            ProcessElement(json.RootElement);
        }


        if (json.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in json.RootElement.EnumerateArray())
            {
                ProcessElement(element);
            }
        }
    }
}
