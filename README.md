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
- For controllers, it validates both constructor injected dependencies and endpoint injected dependencies
- Supports several different manual (explicit) checks

Just open the Tests project and look at DependencyTests to see how to use it.
