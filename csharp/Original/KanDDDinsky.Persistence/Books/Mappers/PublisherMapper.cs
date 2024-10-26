﻿using KanDDDinsky.Books.Entities;
using KanDDDinsky.Persistence.Publishers;

namespace KanDDDinsky.Persistence.Books.Mappers;

public static class PublisherMapper
{
    public static Publisher Map(this PublisherEntity publisher) =>
        new Publisher(new PublisherId(publisher.Id), new PublisherName(publisher.Name));

    public static PublisherEntity MapToEntity(this Publisher publisher) =>
        new PublisherEntity { Id = publisher.Id.Value, Name = publisher.Name.Value};
}
