# insurance-case
Insurance cost calculator API

* Contributors: Nejdet Eren Pinaz
* Development Time: ~10 Hrs
* Code coverage (%Line): 66.32% Covered

TASK1 - BUGFIX
------------------------------------
In the scope of this task I left the code as is and only derived several new unit tests from the business rules to identify issues.

- Encountered a bug where calculated insurance cost is 0 given an insurable laptop or smartphone product with sales price €300. 
  Expected insurance cost was €500.
  Issue caused by the erroneous top-level conditional statment that ignoring the product type while checking the sales price.
  
  Fixed by moving the product type checking statement outside
  
- Encountered a bug where calculated insurance cost is 0 given an insurable product with sales price exactly €500.
  Expected insurance cost was €1000.
  Issue caused by the erroneuous greater than (>) operator inside the top-level conditional statement's else block
  
  Fixed by changing the operator to greater than or equal (>=)

TASK2 - REFACTOR
------------------------------------
Examined the initial code, defined key refactor points and carefully worked on them without changing the API behavior.
After analyzing potential size of the project, fully implementing a complex architecture like Clean Architecture, N-Layer, etc. 
seemed like an overkill. 

While minding coupling and cohesion, I've decided to separate concerns using folders.
I've tried to keep things simple and convenient (cost of development complexity, onboarding new engineers, etc.) and decided 
not to use any 3rd party tools, docker images, etc.

Following decisions made in the scope of this task:

- Added swagger for documentation
- Renamed solution, controller and actions to clarify context
- Removed Startup.cs, unnecessary usings and introduced GlobalUsings
- Reimplemented CalculateInsurance method as GET because we only use "productId" and a POST body with the field "insuranceValue" was
  unnecessary (kept the old endpoint as deprecated)
- Removed "ref" keyword usage as it decreases readability
- Removed "dynamic" type usage to ensure type-safe code
- Created DTOs, separate request and response models to transfer data
- Created a service layer containing interface and implementations for handling business logic in order to prevent bloated Controllers
- Utilized asynchronous programming whenever possible to improve scalability
- Utilized typed HttpClient approach with IHttpClientFactory to prevent socket exhaustion
- Utilized Polly for resilience and fault-tolerance
- Utilized interfaces and dependency injection for decoupling and testability
- Utilized ExceptionHandler for error response consistency and to comply with DRY principles
- Utilized Serilog package for enriched, structured logs
- Utilized FluentValidations to validate request models
- Utilized Moq and Arrange-Act-Assert pattern for unit tests

TASK3 - FEATURE1 (ORDER INSURANCE)
------------------------------------
- Assumed that input should be multiple ProductId's without quantity for simplicity. Created new endpoint and InsuranceCostCalculator
  accepting  both single and multiple ProductId's. Should the calculation logic needed to change in the future, modifying this class
  alone would be enough.
- Utilized Task.WhenAll to make concurrent API calls (assuming there is no rate limiting for ProductData API). It would be
  better If we could retrieve data by sending multiple Id's in a single request. But the ProductData API do not offer this capability.

TASK4 - FEATURE2 (CALCULATOR LOGIC)
------------------------------------
- Assumed that business rules like insurance cost thresholds could change in the future. So I've created application-wide constants 
  for product types and all the relevant business rules in order to achieve some degree of configurability. I then used these constants 
  in calculation and tests.

TASK5 - FEATURE3 (SURCHARGE ENDPOINT)
------------------------------------
- Assumed that a surcharge rate should be a percentage of some value. In this case I've chosen sales price of the product.
- Assumed that a surcharge rate should be in some range (e.g. 200% of sales price would be a bit too much). Added validations to prevent
  creating unrealistic rate values that might eventually upset our stakeholders.
- Assumed that emulating a key-value store (C# ConcurrentDictionary with immutable primitive type) instead of running a docker container
  would be better for demonstration purposes. As an outcome of this decision, uploaded rates are not  persistent and will  be wiped-out
  as soon as the application restarts (Discussed with the stakeholders). I also decided not to use EntityFramework because I was planning
  to use a NoSQL DB in production.
- Assumed that creating a surcharge rate should be an upsert.
- Assumed that creating a surcharge rate should be allowed even when the product type is non-insurable. There is a trade-off between 
  making an additional API call and creating some unnecessary data. Making an additional API call in a high-traffic endpoint could be
  a problem.

FUTURE IMPROVEMENTS
------------------------------------
- Change frequency of product type data? Would caching this data with some TTL and auto-reloading mechanism to reduce external 
  API calls be a good idea? (Not implemented because of the time constraint)
- We have DTOs, request and response models and domain entities. How about using a mapping library like AutoMapper? It would
  make our code cleaner. (Not implemented because of the time constraint)
