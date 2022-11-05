namespace YoumaconSecurityOps.Core.Shared.Models.DTO;
public sealed record ContactDto(Guid Id, String PreferredName, String LastName, String Email, PronounDto Pronoun);
