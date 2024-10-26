using KanDDDinsky.Persistence.Languages;
using KanDDDinsky.Persistence.Translators;

namespace KanDDDinsky.Persistence.Books.ValueObjects;

public class TranslationVO
{
    public required LanguageEntity Language { get; set; }
    public required TranslatorEntity Translator { get; set; }
}

