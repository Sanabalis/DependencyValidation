# Dependency validation

Supports: 
- Scans and validates entire IServiceCollection 
- Handles OpenGenerics
- Has two severity levels:
- - Warning which just prints to the test output, but does not fail the test
- - Error which does the same but fails the test
- Automatically validates service lifetimes (with Warning severity), with following rules:
- - Singleton implementation should not depend on Scoped or Transient dependencies
- - Scoped implementation should not depend on Transient dependencies
- Scanning assemblies for Controllers
- - It validates both constructor injected dependencies and endpoint injected dependencies
- Supports several different manual (explicit) checks


DependencyValidator, FailedValidation and Severity are implementation of the Dependency validation. They are the classes you want to copy and use.
Open the Tests project and look at DependencyTests to see how to use them.

Another few interesting files are Program.cs and WeatherForecastControllers.
