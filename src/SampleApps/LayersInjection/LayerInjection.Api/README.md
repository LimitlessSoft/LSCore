This project shows how injecting through different layer using one of scanning features (default convention) works.

Using the AddLSCoreDependency injection, we scan the assemblies and register the services following default convetion.

Using `LayerInjection.Contracts` we define the interfaces which we catch and use inside our application.
Inside any other layer, we can reference `Contracts` and implement specific interface.

In this example, we have `LayerInjection.Api` which is the main project.
It references `LayerInjection.Contracts` and also implementation layers like `LayerInjection.Domain` and `LayerInjection.Repository`.
Inside layers which implement the interfaces, we reference only `Contracts` and implement the interface.