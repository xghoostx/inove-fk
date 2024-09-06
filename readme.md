### InoveFk


## Overview
InoveFk is a basic package designed to assist in software development. It provides foundational tools and services that simplify the integration of common functionalities like notifications, exception handling, and JWT authentication.

## Example Configuration
Below is an example of how to configure InoveFk in your .NET application:

In the Program.cs file, add the desired resources:

// Configure the use of Notifications
builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .SetCompatibilityVersion(CompatibilityVersion.Latest);

// Configure the use of the exception handling middleware
app.UseCustomExceptionHandler();

// Configure the use of core services
builder.Services.AddCoreServiceDependency();

// Configure the use of Jwt service
builder.Services.AddTransient<IJwtTokenService>(provider =>
{
    return new JwtTokenService(tokenKey);
});
This example demonstrates how to integrate InoveFk with core services, exception handling middleware, and JWT authentication.

Contribution
Contributions are welcome! Feel free to open issues or pull requests on the repository.
