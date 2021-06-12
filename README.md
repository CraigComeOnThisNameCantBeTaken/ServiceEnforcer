# ServiceEnforcer
This package allows you to ensure that services have been provided (with the correct scope) at startup. Even if the services have been provided in a different project.

Take Onion Architecture for instance (see https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1).

It is a common approach to usse dependency inversion to implement infrastructure concerns outside of a very general package. This is good design but how can we ensure that they *actually are* provided by the infrastructure layer?

Lets imagine a scenario where you have written 2 libraries - Authentication which has IUserManager and Authentication.Auth0 which implements it. A number of years later your business decides to switch to IdentityServer and a different developer (Jim) begins implementing Authentication.IdentityServer. How do we ensure they implement IUserManager there too?

**Obviously integration tests?**

We can do that but it would have to be tests against Authentication.Auth0. Or tests against our Web Api / Website. There is nothing forcing Jim to write tests for Authentication.IdentityServer and he might be working on a different project.

**Well Jim should write his tests**

Yes he should. That isnt debatable. I would however like to protect the business if he doesnt.

**There should be someone manually testing...**

Yes there should. But how do we know there will be? We dont even know what project Jim is on. We might not even be at the company anymore.

**Ok but ServiceProviderOptions.ValidateOnBuild should solve this?**

Nope. Actually that just ensures the services that have been registered can be created when calling BuildServiceProvider.

**What about ValidateScopes?**

This is probably more obvious but to be clear this just makes sure you arent trying to resolve scoped services where you shouldnt be.

**Why should I care? They should do their job right**

Fair. But I think declaring how to extend a library should be a responsibility of the library. I also care because I want the business to get value for my work 10 or 20 years from now.

**Just document it then**

Finally I found someone that reads documentation. Afterall you got this far ;)

## Installation
### Project With The Interface

```
services.EnforceServices(builder =>
{
    builder.Enforce<IMyInterfaceToCodeAgainst>();
    builder.EnforceTransient<IMyInterfaceToCodeAgainst>();
    builder.EnforceScoped<IMyInterfaceToCodeAgainst>();
    builder.EnforceSingleton<IMyInterfaceToCodeAgainst>();
});
```

### Project With Implementation
```
services.AddSingleton<IMyInterfaceToCodeAgainst, MyConcrete>();
```
