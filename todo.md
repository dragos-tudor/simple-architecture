
### Background services
- job scheduler service to resume failed active messages.

### Resiliency
- email clients resiliency [smtp, imap Microsoft.Extensions.Resilience].
- sql server client resiliency [EF Core DbContextOptionsBuilder].
- mongo db client resiliency [connection string].
