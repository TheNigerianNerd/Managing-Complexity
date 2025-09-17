namespace TDList.Data;

public sealed class ConnectionProvider : IConnectionProvider
{
    // Lazy<T> ensures thread-safe, lazy initialization
    private static readonly Lazy<ConnectionProvider> _instance =
        new(() => new ConnectionProvider());

    public static ConnectionProvider Instance => _instance.Value;

    // Private constructor prevents direct instantiation
    private ConnectionProvider()
    {
        // Default file name (can be overridden if needed)
        FileName = "TDList.json";
    }

    public string FileName { get; private set; }

    // Expose a "connection string" (here it's just a file path)
    public string GetConnectionString()
    {
        return FileName;
    }
}
