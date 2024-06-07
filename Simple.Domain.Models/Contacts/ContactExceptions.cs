
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Simple.Domain.Models;

public sealed class ContactValidationException (string message): ValidationException(message);

public sealed class ContactDuplicateException (string message): DataException(message);