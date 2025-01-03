﻿using KanDDDinsky.Books.Entities;
using KanDDDinsky.Persistence.Authors;

namespace KanDDDinsky.Persistence.Books.Mappers;

public static class AuthorMapper
{
    public static Author Map(this AuthorEntity author) =>
        new Author(new AuthorId(author.Id), new AuthorFirstName(author.FirstName), new AuthorLastName(author.LastName));


    public static AuthorEntity MapToEntity(this Author author) =>
        new AuthorEntity
        {
            Id = author.Id.Value,
            FirstName = author.FirstName.Value,
            LastName = author.LastName.Value
        };
}
