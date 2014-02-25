# [Aspects for .NET](http://aashishkoirala.github.io/aspects/)
### Aspect Oriented Programing Support for .NET 
___
##### By [Aashish Koirala](http://aashishkoirala.github.io)

Aspects for .NET is a library that you can use to add aspect orientation to your .NET application. It provides interfaces that you can implement to build your own aspects which you can then apply to your implementation classes. It also then provides a mechanism to wrap, at runtime, your implementation with aspects (and no, it does not mess with the MSIL post-compilation). There is also integrated support for MEF, albeit with some limitations.

#### In a Nutshell
Say you have an aspect attribute `MyAspectAttribute` applied to an implementation `MyImplementation` that implements the contract `IMyContract`:

	public interface IMyContract
	{
		void DoThing();
	}

	public class MyImplementation : IMyContract
	{
		[MyAspect]
		public void DoThing()
		{
			// ... Do stuff
		}
  	}

Then, you can get an "aspectized" instance like so:

	var myInstance = AspectHelper.Wrap<IMyContract>(new MyImplementation());
	myInstance.DoThing();

#### Installation
Download the NuGet package [Aspects](https://www.nuget.org/packages/aspects/) and add to your application:

	Install-Package Aspects

#### How It Works
When you call `Wrap`, it inspects the contract interface and implementation class using reflection. It uses `CodeDOM` to generate an on-the-fly implementation of the contract interface, which expects an instance of the implementation to be initialized, and where each member simply calls out to the implementation (i.e. a hollow wrapper). However, in the process, it also inspects the implementation member to see if any aspect attributes are applied and adds code at appropriate places before or after the invocation to execute those aspects. The type that is generated on the fly is cached in memory.

There are also overloads of the `Wrap` method in the `AspectHelper` class that support lazy instantiation, as well as a corresponding set of `WrapMany` methods that deal with multiple instances. In addition, two events `CodeGenerated` and `CodeCompiled` are provided that you can handle to inspect or change the generated output.

#### MEF Support
Going back to the previous example, if `MyImplementation` is also a MEF export for `IMyContract`, like so:

	[Export(typeof(IMyContract))]
	public class MyImplementation { .... }

And you're MEF-importing `IMyContract` like so:

	[Import] IMyContract myContract

You can easily inject aspects by following these steps:

1. In your startup routine, after you've composed your MEF container (say `myContainer`), call the following:

		AspectHelper.RegisterForComposition(myContainer, /* list of assemblies to scan for MEF exports */);
2. Modify your import declaration as:

		[Import(typeof(IAspected<IMyContract>))] IMyContract myContract

There are a few limitations though:

+ You must use an `AggregateCatalog` to compose the container.
+ Exports with multiple cardinalities are not supported (i.e. does not work with `ImportMany`).
+ Support for derivations of `Export` (e.g. `InheritedExport`) has not been tested.
+ Support for metadata attributes has not been tested.

#### Creating Aspects
Aspects can be applied to methods or properties by decorating the methods or properties with the aspect attribute class. You can also decorate the class - which will apply the aspect to all properties or methods within the class that belong to the contract. If multiple aspects of the same type are applied, they are executed in order as defined by the `Order` property. Three types of aspects are supported:

+ **Entry Aspect**: Executed as soon as control enters into the method or property. To create an entry aspect, create an attribute class (i.e. one that inherits from `Attribute`) that implements `IEntryAspect`.

		public class MyEntryAspectAttribute : Attribute, IEntryAspect
		{
			public bool Execute(MemberInfo memberInfo, IDictionary<string, object> parameters)
		    {
				// You can access or modify parameters by name from the "parameters" dictionary,
				// e.g. parameters["paramName"].
	
				// If you're in a property setter, you can use parameters["value"] to get the setter value.
	
				// For generic methods, use parameters["T1"], parameters["T2"], etc. to get the
				// type-parameter values.
		
				// Return True to continue execution, return False to stop execution and return
				// immediately. For methods that return values, in this case the default value for
				// the return type is returned.
			}	
		}

+ **Exit Aspect**: Executed just before control leaves the method or property. To create an exit aspect, create an attribute class (i.e. one that inherits from `Attribute`) that implements `IExitAspect`.

		public class MyExitAspectAttribute : Attribute, IExitAspect
		{
			public void Execute(MemberInfo memberInfo,
				IDictionary<string, object> parameters, 
				object returnValue, 
				TimeSpan duration)
			{
				// Similar to entry attribute, but you also get the value that is being
				// returned in "returnValue" (NULL for void methods).

				// You get the time taken to execute the method/property in "duration".
				// Comes in handy for instrumentation.
			}
		}

+ **Error Aspect**: Executed if an exception is thrown within the method or property. To create an error aspect, create an attribute class (i.e. one that inherits from `Attribute`) that implements `IErrorAspect`.

		public class MyErrorAspectAttribute : Attribute, IErrorAspect
		{
			public bool Execute(MemberInfo memberInfo, 
				IDictionary<string, object> parameters, Exception ex)
        	{
				// "ex" has the exception that was thrown. This aspect is called from
				// a generated "catch" block.

				// After you're done, return True to rethrow the exception, or False to
				// swallow it. If you swallow it and another error aspect rethrows, the final
				// result will be that the error is rethrown.
			}
		}


There you have it, AOP for you without having to depend on an IoC container or having your code messed with like PostSharp does. Quite a bit of room for improvement, though, especially on the performance (I'm guessing as I haven't run performance tests) and MEF support side.