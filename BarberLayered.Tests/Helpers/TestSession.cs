using Microsoft.AspNetCore.Http;

namespace BarberLayered.Tests.Helpers;

public sealed class TestSession : ISession
{
    private readonly Dictionary<string, byte[]> _storage = new();

    public IEnumerable<string> Keys => _storage.Keys;

    public string Id { get; } = Guid.NewGuid().ToString();

    public bool IsAvailable => true;

    public void Clear()
    {
        _storage.Clear();
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task LoadAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        _storage.Remove(key);
    }

    public void Set(string key, byte[] value)
    {
        _storage[key] = value;
    }

    public bool TryGetValue(string key, out byte[]? value)
    {
        return _storage.TryGetValue(key, out value);
    }
}