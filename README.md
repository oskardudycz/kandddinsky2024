# "Slim your aggregates with an Event-Driven approach" workshop at KanDDDinsky 2024

![KanDDDinsky logo](https://kandddinsky.de/img/design/logo_light.svg)

Aggregate is a useful pattern, but it's tricky to get it right, especially when starting. The initial focus on business logic is too often replaced with state obsession.

During the session, I'll show you how an Event-driven approach and a bit of functional composition can help to slim them down.

All of that will be as live refactoring that starts from a typical, bloated implementation and ends with code focused on behaviour and simplicity.

## The what?

Start by thinking about what Aggregate is actually about. How would you define it? What's the goal of using it?

## The how?

You choose! Which language should we code with: C#, Java, or TypeScript?

## The code

To start our workshop, we need to find an implementation that looks valid at first glance but falls into the common mistakes of Aggregate Design. Where can we find it? 

Luckily, we have a perfect candidate for that: our friend Chet Geppetto, who writes precisely such code!

Let's ask our friend! Let's go to https://chatgpt.com/ and type:

> Could you give me a real-world example of the complex DDD aggregate?
> 
> Describe the business rules and invariants. It should have at least ten public and some private properties and several business rules and invariants showing business logic.
> 
> It should have a list of nested data and a few value objects with logic. Use strongly typed identifiers.
> 
> Root aggregate should represent a complex workflow or process. It should express lifetime or state transition and have several methods for running business logic (more than seven).
> 
> It should be more complex than simple ifs and data assignments (the same goes for value objects). So, more complicated business rules. It should show the imperfect outcome of the project that was completed by multiple people over a longer time span.
> 
> Use some random, untypical business use case fitting the above requirement (other than Order, Loan, or Event Management to not get us bored).
> 
> Describe aggregate flow, business rules first and invariants before implementation.
> 
> Don't list or describe properties; just express them in code.
> 
> Start with the main aggregate implementation, then put the rest of the value objects definitions.
> 
> Write the whole code, including business logic and invariant checks. Provide aggregate and value objects as separate code snippets.
> 
> Remember that value objects should also be broken into dedicated snippets. Business logic should be more complex than state assignments and basic ifs representing the imperfection of a real project's outcome. Again, the logic inside methods should be more complicated than if statement.  We should at least 7 business workflows handled by aggregate. Some methods (but not all!) should be publishing domain events.
> 
> Keep in mind that it'll be placed in the Clean/Hexagonal Architecture and has to be stored with Entity Framework. Provide example application layer and repository for storing our aggregate.
>
> The code should look like written by team of average developers inspired by Youtube/LinkedIn influencers advices.
> 
> Provide an example implementation in C# 11 please don't leave any placeholders or comments for further implementation.

Now, go to your LinkedIn profile and add the Prompt Engineering skill and DDD practitioner title!

## The Output

TBD

## Paste it in your code

Create the new module inside the Domain folder in your selected language:
- [C#](./csharp/Original/KanDDDinsky.Domain/),
- [Java](./java/src/main/java/io/eventdriven/slimdownaggregates/original/domain/),
- [TypeScript](./node.js/src/original/domain/).

## Prerequisitions

1. Clone this repository.
2. Install .NET 8 - https://dotnet.microsoft.com/en-us/download/dotnet/7.0.
3. Install Rider, Visual Studio, VSCode or other preferred IDE.
4. Open [SlimDownAggregates.sln](./csharp/SlimDownAggregates.sln) solution.
5. Run build.
6. To run integration tests you either need to run `docker-compose up` to setup Postgres container. Or set `TEST_IN_MEMORY` environment variable to true if you'd like to run them in memory.

### Java

1. Clone this repository.
2. Install Java JDK 22 (or later) - https://www.oracle.com/java/technologies/downloads/.
3. Install IntelliJ, Eclipse, VSCode or other preferred IDE.
4. Open [java](./java/) folder as project.

### TypeScript

1. Clone this repository.
2. Install Node.js 18 (or later) - https://node.js.org/en/download/ (Or better using NVM).
3. Install VSCode, WebStorm or other preferred IDE.
4. Open [node.js](./node.js/) folder as project.
5. Run `npm run build` to verify that code is compiling.
6. Run `npm run test` to verify if all is working.