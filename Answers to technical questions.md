# Answers To Technical Questions

## Introduction
At the first glance, dealing with external or third party services would be so straight forward for software developers, wrapping the service ad using it. But, at the heart of scenario, it would be so complicated that it appears.  Here, I want to describe some of my previous experiences about this subject and then I will answer to following questions.
Using external resources like APIs could be so challenging and the following matters would be take into consideration.
* Third party calling limitations likes number of requests per a specific duration like seconds, minutes, etc… (Queuing or throttling may solve this problem)
* Third party monetization policies that if neglected, could burden extra costs on consumer. (Caching the response or some limitation may help to improve the costs)
* Third party performance that may affect the performance of total software 
* Third party monitoring
* Third party availability and reliability that if ignored, may have bad impact on final product when the dependency to that resource would be crucial (Using failover mechanisms or redundant third party services may help)
* Security
* Third party service providing type (sync/async)
* …

Besides, the type of domain and the context the service resides may influence the complexity of implementation. For example, if the service is using in a blog that its mission is only to show the data received from third party API, lots of the mentioned concerns above could be ignored and a simple wrapping around the external services would be enough for implementation (My second implementation I provided assume that scenario). On the other hand, if the external service is in the heart of a core domain and the domain has more dependencies to it lots of things should be taken to account (My first implementation follows this). 
To sum up, implementing a solution to use an external API would be so simple or so burdensome. The key point to choose an appropriate solution is the context that external service belongs to. 



## Questions:  

### 1.	How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment, then use this as an opportunity to explain what you would add.
I broke down the story to some tasks:
* Thinking and designing the solution (3 hours)
* Implementing the structure of project and layering (1 hours)
* Developing cross-cutting concerns Logging and Exception Handling (2 hours)
* Implementing core components and seedwork (4 hours)
* Adding some failover techniques (1 hour)
* Appling  Tests (4 hours)
* Implementing the second approach (15 minutes reusing previous implementation)
* total about 16 hours

Also if I had enough time I would add or improve following parts
* More and better logging because it empower the monitoring and diagnosis capabilities
* Better exception and error handling
* Appling security concerns to API like providing api-key
* Applying throttling scenarios or CAPTCHA to prevent API over usage 
* Implementing an appropriating User interface 
* Applying facilities for monitoring the performance
* Adding more unit and integration tests
* Thinking about deployment

### 2.	What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.
Microsoft has brought a new way of starting a mvc program by eliminating the Program class.
```c#
var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var apiConfiguration = app.Services.GetService<ApiConfiguration>();

startup.Configure(app, app.Environment, apiConfiguration);

app.Run();
```

Also, c# 10 introduce ```record``` struct
```c#
public record QuotesResponse
{
	public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }
}
```

### 3.	How would you track down a performance issue in production? Have you ever had to do this?
There are lots of methods and tools that track down the performance of a software in productions environment like cloud tools which exist in Azure, AWS or other platforms or other performance counters like Windows Performance Monitor, SQL Profiler, Stopwatch logging or other Devops tools which I used them in some cases before. Also, benefiting from an appropriate logging and diagnosing libraries would be very useful. I don’t have hands on experiences with diagnosis libraries but I always pursue the rule of Maximum Logging in my implementations. Also, .NET 6 brings improved support for OpenTelemetry, which is a collection of tools, APIs, and SDKs that help developers analyze their software's performance and behavior.

### 4.	What was the latest technical book you have read or tech conference you have been to? What did you learn?
Recently I haven’t read any technical books but I read some articles about Azure (which because of limitation we don’t have access to this. I downloaded “Learn Azure in a Month of Lunches” and “Developers Guide To Azure” and have plan to start reading them soon) and Kafka to implement event streaming scenario in one of projects that I involved.
Also, 8 months ago I decided to have experiences about React because the our frontend team used that to implement project frontend and we had lots of problem in it. So, I registered to a course in Udemy.com and after that I began to implementing frontend modules that solved lots of our issues.

### 5.	What do you think about this technical assessment?
I have mentions my point of view about this assessment in this document introduction.

### 6. Please, describe yourself using JSON.

```json
{
	"identity" : {
		"first_name": "Alireza",
		"last_name": "Arezoomand Ershadi",
		"age": 44,
	},
	"marital_status": {
		"staus" : "married",
		"childrens": 1
	},
	"jobs": ["Senior Software Developer", "Software Consultant"],
	"skills: {
		"technical_skills" : ["OOP", "SOLID", ".Net Core", ".Net Framework", "SQL Server", "Reactjs"],
		"soft_skills": ["Problem Solver", "Flexible", "Self-Motivated", "Leader", "Planner"]
	},
	"interests": [
		"Learning new things",
		"Travelling",
		"Playing Music",
		"Cooking",
		"Sports",
		"Woodworking"
	]
}
```
