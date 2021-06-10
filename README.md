# Ports And Adapters
This package allows you to ensure that services have been provided (with the correct scope) at startup. Even if the services have been provided in a different project.

Take Onion Architecture for instance (see https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1).

It is a common approach to specify repositories in the domain layer using interfaces and then implement them in the infrastructure layer. This allows you to abstract your database technology from your domain. This is actually a very common approach that isnt restricted to Onion Architecture - The D in SOLID stands for Dependency Inversion.

There is however a pitfall - since the domain layer isnt providing the repository implementations, how can we ensure that they *actually are* provided by the infrastructure layer? This is a problem because if we do replace our database technology we will probably implement a new project and we might miss something. Worse this doesnt only apply to repositories, actually most well designed software uses dependency inversion for a plethora of reasons.

even worse you might write a library that has infrastructure concerns and expect another library to provide them using dependency inversion (eg Authentication, Authentication.IdentityServer). What if the service has to be transient but the infrastructure layer provides it as transient? Things go bang.

*But ServiceProviderOptions.ValidateOnBuild should solve this?*
Nope. Actually that just ensures the services that have been registered can be created when calling BuildServiceProvider.

*What about ValidateScopes?*
This is probably more obvious but to be clear this just makes sure you arent trying to resolve scoped services where you shouldnt be.

*This library to the rescue*
Now we can specify in the layer with the interface that it must be registered, and we can specify the lifetime. If it isnt registered correctly the application will throw at startup with a clear exception.

## Installation
### Project With The Interface

```
            services.EnforcePortsAndAdapters(builder =>
            {
                builder.AddPort<IMyInterfaceToCodeAgainst>(ServiceLifetime.Singleton);
            });
```

### Project With Implementation
```
            services.AddSingleton<IMyInterfaceToCodeAgainst, MyConcrete>();
```
