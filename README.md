# "Slim your aggregates with an Event-Driven approach" workshop at KanDDDinsky 2024

![KanDDDinsky logo](https://kandddinsky.de/img/design/logo_light.svg)

Aggregate is a useful pattern, but it's tricky to get it right, especially when starting. The initial focus on business logic is too often replaced with state obsession.

During the session, I'll show you how an Event-driven approach and a bit of functional composition can help to slim them down.

All of that will be as live refactoring that starts from a typical, bloated implementation and ends with code focused on behaviour and simplicity.

# The what?

Start by thinking about what Aggregate is actually about. How would you define it? What's the goal of using it?

# The how?

You choose! Which language should we code with: C#, Java, or TypeScript?

# The code

To start our workshop, we need to find an implementation that looks valid at first glance but falls into the common mistakes of Aggregate Design. Where can we find it? 

Luckily, we have a perfect candidate for that: our friend Chet Geppetto, who writes precisely such code!

Let's ask our friend! Let's go to https://chatgpt.com/ and type:

> Could you give me a real-world example of the complex DDD aggregate?
> 
> Describe the business rules and invariants. It should have at least ten public and some private properties and several business rules and invariants showing business logic.
> 
> It should have a list of nested data and a few entities or value objects with logic.
> 
> Root aggregate should represent a complex workflow or process. It should express lifetime or state transition and have several methods for running business logic. It should be more complex than simple ifs and state assignments (the same goes for value objects/entities). So, more complicated business rules. It should show the outcome of the project that was completed by multiple people over a longer time span.
> 
> Use some random, untypical business use case fitting the above requirement (other than Order, Loan, or Event Management to not get us bored).
> 
> Describe aggregate flow, business rules first and invariants before implementation.
> 
> Don't list or describe properties; just express them in code.
> 
> Start with the main aggregate implementation, then put the rest of the entities' definitions.
> 
> Write the whole code, including business logic and invariant checks. Provide aggregate and entities as separate code snippets.
> 
> Remember that entities should also be broken into dedicated snippets. Business logic should be more complex than state assignments and basic ifs representing the fuzziness of a real project's outcome.
> 
>  Provide an example implementation in C# 11.

Now, go to your LinkedIn profile and add the Prompt Engineering skill!
