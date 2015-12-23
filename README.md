# Scientist.NET
A .NET interpretation of the Ruby library [Scientist](https://github.com/github/scientist), a library for carefully refactoring critical paths.

## How do I sciene?

Let's pretend you're changing the way you handle permissions in a large web app. Tests can help guide your refactoring, but you really want to capture the current and refactored beahviours under load.

``` c#
using Scientist;

namespace MyNamespace 
{
    public class MyClass
    {
        private MyOtherClass Model;    
    
        public bool MyMethod(User User)
        {
            var experiment = new Experiment("widget-permissions");
            experiment.Use<bool>(() => Model.CheckUser(User).Valid); // old way
            experiment.Try<bool>(() => User.Can(Permission.Read, Model); // new way
        
            return experiment.Run<bool>();    
        }
    }
}
```

Use `Use<T>(..)` to wrap the existing original behaviour, and use `Try<T>(..)` to wrap the new behaviour. `experiment.Run<T>();` will always return the result of the `Use` block, but it does a bunch of stuff behind the scenes:

- It decides whether or not to run the `Try` block
- Measures the duration of both behaviours
- Swallows (but records) any exceptions raised in the `Try` block
- Set a condition to filter calls to `Try`

Upcoming features (these already exist in the Ruby library):

- Randomises order to execute `Try` and `Use` blocks
- Compares results of `Try` and `Use` blocks
- Publishes all the information gathered
- Turn project into a NuGet package
