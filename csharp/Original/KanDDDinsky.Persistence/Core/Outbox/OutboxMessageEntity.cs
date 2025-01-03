﻿using System.Text.Json;

namespace KanDDDinsky.Persistence.Core.Outbox;

public class OutboxMessageEntity
{
    public long Position { get; init; } = default;
    public required string MessageId { get; init; }
    public required string MessageType { get; init; }
    public required string Data { get; init; }
    public required DateTimeOffset Scheduled { get; init; }

    public static OutboxMessageEntity From<T>(T message) =>
        new()
        {
            MessageId = Guid.NewGuid().ToString(),
            Data = JsonSerializer.Serialize(message),
            Scheduled = DateTimeOffset.UtcNow,
            MessageType = typeof(T).FullName ?? "Unknown"
        };
}
