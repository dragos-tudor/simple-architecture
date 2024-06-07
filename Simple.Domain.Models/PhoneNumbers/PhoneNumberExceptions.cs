
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Simple.Domain.Models;

public sealed class PhoneNumberValidationException(string message): ValidationException(message);

public sealed class PhoneNumberDuplicateException(string message): DataException(message);