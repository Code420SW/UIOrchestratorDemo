### TL;DR
Broken Access Control vulnerabilities leaped to the top of the most recent OWASP 10 Web Application Security Risks in their most recent survey. Insecure Direct Object Reference (IDOR) is a class of vulnerabilities that is the fuzziest and less commonly addressed since it pits the creativity of the attacker to construct something from the data elements exposed by the client.

Code420 published a demo showing techniques to mitigate IDOR security issues in the context of the Blazor framework.

### Overview

Broken Access Control vulnerabilities leaped to the top of the most recent OWASP 10 Web Application Security Risks in their most recent survey. This vulnerability covers numerous categories which can be grouped as Vertical Access Controls and Horizontal Access Controls issues. The intent of this demo is to show how Code420 mitigates security issues in the context of the Blazor framework.

Insecure Direct Object Reference (IDOR) is a class of vulnerabilities that cuts across Vertical and Horizontal Access Controls. This is the fuzziest and less commonly addressed vulnerability since it pits the creativity of the attacker to construct something from the data elements exposed by the client. The demo primarily focuses on IDORs.

Blazor is gaining popularity as a framework to render web applications leveraging the mature C# language and a rich collection of support libraries to enable developers to deliver robust solutions. Like many web applications, Blazor renders single-page applications (SPA) to a client (browser) and, as with most web applications, utilizes the `@page` directives for inter-page navigation and, in many cases, query parameters to pass data between otherwise disparate pages.

This project demonstrates one of the methods Code420 uses to improve security in its Blazor applications. Many elements are included in the demonstration but the primary focus is security.

This wiki documents all aspects of the demo codebase and the code itself contains lots of comments to facilitate spelunking. However, to assist you in finding the salient elements in the wiki:
- The [Security Considerations](https://github.com/Code420SW/UIOrchestratorDemo/wiki/Security-Considerations) section discusses the vulnerabilities mentioned above and contains links to additional sources.
- The [UI Orchestrator](https://github.com/Code420SW/UIOrchestratorDemo/wiki/UI-Orchestrator) section summarizes how Code420 addresses IDOR vulnerabilities.
- Pay attention to the [Syncfusion](https://github.com/Code420SW/UIOrchestratorDemo/wiki/Syncfusion-Component-Library) section about needing a license to run the demo.

The balance of sections provides a deeper dive into the elements that comprise the UI Orchestrator.

***

### Demo Application Structure

Code420 typically uses the Backend for Frontend (BFF) pattern for its SaaS application which supports multi-tenant configurations for its client base. Authentication, authorization, and data isolation are always top-of-mind.
 
<br />
<figure>
<img src="https://github.com/Code420SW/UIOrchestratorDemo/assets/97139631/0bbdcd69-6abb-4598-86d7-b2c1f4b35040" width=75% height=75%>
<br />
<figcaption>Source: MS Build 2023, "What's new in .NET 8 for web frontend, backends and futures</figcaption>
</figure>
<br /><br />

The Backend for Frontend (BFF) pattern in the demo has the following workflow:
- The client (browser) connects to the BFF (Blazor Server)
- The BFF is responsible for UI-related concerns and relies on backend APIs to manage all business aspects of the application
- The API (not part of the demo) relies on additional external APIs as needed to support the business logic
- Each BFF session (one per client) stores a JSON Web Token (JWT) reflecting an authenticated user's authorization (permissions) which govern all aspects of the client-BFF conversations and gate conversations between the BFF and API.

The security concerns addressed by the demo lie between the client and BFF. It is assumed that since the developer has full control over the deployment of the BFF and API, they can easily harden against security vulnerabilities. The same cannot be said about the client-BFF relationship: data sent to the client is no longer in the application's control.

Fair warning: Some elements of the demo are opinionated. The structure of the demo UI can be what you want it to be. Using a tabbed interface to containerize non-addressable razor pages (those without a `@page` directive) is typical of our apps though the concept of containerization is the key takeaway. There are many ways to implement this concept.

A final word about the demo. This is NOT intended to be a framework you use to develop Blazor applications. It demonstrates concepts you may use in your application. If you are looking for a framework, may exist ranging from:
- [Oqtane](https://github.com/oqtane/oqtane.framework)<br />
Oqtane is a CMS and Application Framework. It leverages Blazor, an open source and cross-platform web UI framework for building single-page apps using .NET and C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries.
- [BlazorPlate](https://www.blazorplate.net/)<br />
BlazorPlate is a project template for startups that utilizes .NET 7 and Blazor WASM to provide developers with commonly used boilerplate code in many .NET projects with minimal customization.
