namespace MojaApp.API.Common.Exceptions;

// Traženi zapis ne postoji. GlobalExceptionHandler ga pretvara u 404.
public class NotFoundException(string message) : Exception(message);
