using System.Text.Json;
using TDList.Contracts;
using TDList.Classes;
namespace TDList.Data;

public class DataSource : IDataSource
{
    private const string FileName = "TDList.json";
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public bool Exists(string connection)
    {
        if (string.IsNullOrWhiteSpace(connection))
            return false;

        // Ensure it specifically points to TDList.json
        var filePath = Path.Combine(connection, "TDList.json");
        return File.Exists(filePath);
    }

    public bool IsValid(string connection)
    {
        // 1) Must exist
        if (!Exists(connection)) return false;

        var path = Path.Combine(connection, FileName);

        // 2) Must be non-empty
        var fi = new FileInfo(path);
        if (fi.Length == 0) return false;

        // 3) Must be valid JSON array of ToDo-like objects
        try
        {
            using var stream = File.OpenRead(path);
            using var doc = JsonDocument.Parse(stream);

            if (doc.RootElement.ValueKind != JsonValueKind.Array) return false;

            foreach (var elem in doc.RootElement.EnumerateArray())
            {
                if (!IsValidTodoElement(elem)) return false;
            }

            // optional: require at least one item
            // if you want to allow empty lists, remove this
            // return doc.RootElement.GetArrayLength() > 0;

            return true;
        }
        catch
        {
            // not valid JSON
            return false;
        }
    }

    public IEnumerable<ToDo> Get(string connection)
    {
        var path = Path.Combine(connection, FileName);
        if (!File.Exists(path))
            throw new FileNotFoundException($"Data source not found: {path}");

        // You can rely on IsValid before calling Get, but Get can still be defensive.
        using var stream = File.OpenRead(path);
        var todos = JsonSerializer.Deserialize<List<ToDo>>(stream, JsonOpts) ?? new List<ToDo>();
        return todos;
    }

    // --- Helpers ---

    // Minimal schema check for a ToDo item
    private static bool IsValidTodoElement(JsonElement e)
    {
        // Expected properties (adjust if your IToDo differs):
        // Id: Guid
        // Title: string (non-empty)
        // Description: string (allow empty?)
        // DateLogged: DateTime
        // IsComplete: bool

        // Title (required, non-empty)
        if (!TryGetString(e, "Title", out var title) || string.IsNullOrWhiteSpace(title)) return false;

        // Description (required? if optional, relax this)
        if (!TryGetString(e, "Description", out _)) return false;

        // Id (required Guid)
        if (!TryGetGuid(e, "Id", out _)) return false;

        // DateLogged (required DateTime)
        if (!TryGetDateTime(e, "DateLogged", out _)) return false;

        // IsComplete (required bool)
        if (!TryGetBool(e, "IsComplete", out _)) return false;

        return true;
    }
    private static bool TryGetString(JsonElement e, string name, out string value)
    {
        value = default!;
        if (!e.TryGetProperty(name, out var p)) return false;
        if (p.ValueKind != JsonValueKind.String) return false;
        value = p.GetString()!;
        return true;
    }

    private static bool TryGetGuid(JsonElement e, string name, out Guid value)
    {
        value = default;
        if (!e.TryGetProperty(name, out var p)) return false;
        if (p.ValueKind != JsonValueKind.String) return false;
        return Guid.TryParse(p.GetString(), out value);
    }

    private static bool TryGetDateTime(JsonElement e, string name, out DateTime value)
    {
        value = default;
        if (!e.TryGetProperty(name, out var p)) return false;

        // Accept ISO string or JSON date (string)
        if (p.ValueKind == JsonValueKind.String && DateTime.TryParse(p.GetString(), out value))
            return true;

        return false;
    }

    private static bool TryGetBool(JsonElement e, string name, out bool value)
    {
        value = default;
        if (!e.TryGetProperty(name, out var p)) return false;
        if (p.ValueKind == JsonValueKind.True) { value = true; return true; }
        if (p.ValueKind == JsonValueKind.False) { value = false; return true; }
        return false;
    }
}