# Notification Dispatch Kata

A C# practice project focused on interfaces, polymorphism, dependency injection, and separating application behavior from concrete implementations.

## Overview

`NotificationDispatchKata` simulates a simple notification system capable of dispatching notifications through multiple communication channels.

The application currently supports:

- Push notifications
- SMS notifications
- Email notifications

Rather than having the dispatcher directly create or depend on concrete notification senders, each sender implements a shared `INotificationSender` abstraction.

The available implementations are provided to the `NotificationDispatcher` through constructor injection.

## What I Practiced

This kata was designed to strengthen my understanding of several important C# and software design concepts:

- Interfaces
- Multiple implementations of an interface
- Polymorphism
- Constructor dependency injection
- Dependency inversion
- Separating domain models from services
- Selecting behavior based on application data
- Manual dependency wiring

One of the main goals was to understand what dependency injection frameworks such as ASP.NET Core's built-in DI container are doing behind the scenes.

Instead of relying on a framework, the dependencies are manually created and wired together in `Program.cs`.

```csharp
var pushSender = new PushNotificationSender();
var smsSender = new SmsNotificationSender();
var emailSender = new EmailNotificationSender();

INotificationSender[] notificationSenders =
[
    pushSender,
    smsSender,
    emailSender
];

var dispatcher = new NotificationDispatcher(notificationSenders);
```

This makes the relationship between the abstraction, its implementations, and the consuming service explicit.

## Domain

A notification contains information such as:

- Id
- Recipient
- Message
- Notification channel
- Priority

Supported notification channels include:

```text
Push
SMS
Email
```

The dispatcher determines which registered `INotificationSender` implementation is responsible for the requested channel and delegates the notification to it.

## Project Structure

```text
NotificationDispatchKata/
│
├── Contracts/
│   └── INotificationSender.cs
│
├── Domain/
│   ├── Notification.cs
│   ├── NotificationChannel.cs
│   └── NotificationPriority.cs
│
├── Services/
│   ├── NotificationDispatcher.cs
│   ├── PushNotificationSender.cs
│   ├── SmsNotificationSender.cs
│   └── EmailNotificationSender.cs
│
└── Program.cs
```

## Key Design Idea

The `NotificationDispatcher` does not need to know how an SMS, email, or push notification is actually sent.

It only needs to know that each sender satisfies the `INotificationSender` contract.

This allows new notification channels to be introduced without tightly coupling the dispatcher to every concrete sender implementation.

For example, a future implementation could add:

```text
SlackNotificationSender
TeamsNotificationSender
WebhookNotificationSender
```

without requiring the dispatcher to directly instantiate those services.

## Why This Kata Matters

ASP.NET Core applications commonly use dependency injection:

```csharp
builder.Services.AddScoped<INotificationSender, EmailNotificationSender>();
```

It is easy to use this syntax without understanding what the framework is actually doing.

This kata intentionally performs the dependency wiring manually first, making concepts such as constructor injection and dependency inversion easier to understand before relying on a DI container to perform that wiring automatically.

## Running the Project

Clone the repository:

```bash
git clone https://github.com/Airsicktitan/NotificationDispatchKata.git
```

Navigate into the project:

```bash
cd NotificationDispatchKata
```

Run the application:

```bash
dotnet run
```

## Technologies

- C#
- .NET
- Object-Oriented Programming
- Dependency Injection
- Interface-Based Design

## Learning Context

This project is part of an ongoing collection of C# katas focused on strengthening software engineering fundamentals through progressively more realistic domain problems.

The emphasis is not simply on producing working code, but on understanding why common application architecture patterns work and when they should be used.