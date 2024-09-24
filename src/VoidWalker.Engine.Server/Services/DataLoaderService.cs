using System.Text.Json;
using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;
using VoidWalker.Engine.Server.Utils;

namespace VoidWalker.Engine.Server.Services;

public class DataLoaderService : BaseVoidWalkerService, IDataLoaderService
{
    private readonly Dictionary<string, Type> _dataTypes = new();

    private const string _typeProperty = "$type";


    private Dictionary<string, List<object>> _loadedData = new();

    public DataLoaderService(ILogger<DataLoaderService> logger, List<JsonMapTypeData> jsonMaps) : base(logger)
    {
        foreach (var jsonMap in jsonMaps)
        {
            _dataTypes.Add(jsonMap.Name, jsonMap.Type);
        }
    }

    public override async Task InitializeAsync()
    {
        var dataFiles = DirectoriesUtils.GetFilesInRootDirectory("*.json").ToList();

        Logger.LogInformation("Loading data files: {DataFiles}", dataFiles.Count);

        foreach (var dataFile in dataFiles)
        {
            await LoadDataAsync(dataFile);
        }
    }

    private async Task LoadDataAsync(string dataFile)
    {
        var fileStream = File.OpenRead(dataFile);
        var json = (await JsonDocument.ParseAsync(fileStream));

        if (json.RootElement.ValueKind == JsonValueKind.Object)
        {
            if (json.RootElement.TryGetProperty(_typeProperty, out var typeProperty))
            {
                var typeToDeserialize = _dataTypes[typeProperty.GetString()];

                var data = JsonSerializer.Deserialize(json.RootElement.GetRawText(), typeToDeserialize);

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
                Logger.LogWarning("Type property not found in {DataFile}", dataFile);
            }
        }


        if (json.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in json.RootElement.EnumerateArray())
            {
                if (element.TryGetProperty(_typeProperty, out var typeProperty))
                {
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
                    Logger.LogWarning("Type property not found in {DataFile}", dataFile);
                }
            }
        }
    }
}
