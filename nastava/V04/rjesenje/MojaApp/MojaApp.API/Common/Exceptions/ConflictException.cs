namespace MojaApp.API.Common.Exceptions;

// Zahtjev je ispravan, ali je u sukobu s postojećim podacima (duplikat, povezani zapisi). Postaje 409.
public class ConflictException(string message) : Exception(message);
