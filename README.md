# E-Commerce.Web Solution

Concise overview of architecture and design patterns enabling scalability, testability, and maintainability.

## 1. Solution Overview
Projects are organized around a Clean / Onion-inspired layered architecture. Dependencies point inward only (Presentation -> Core Services -> Domain -> (Shared) and Infrastructure for implementations). Business rules remain isolated from frameworks and UI concerns.

```
Root
├─ E-Commerce.Web.sln (Solution)
├─ E-Commerce.Web/        (API Host / Presentation Layer)
├─ Presentation/          (Optional composition endpoints layer)
├─ Core/                  (Application Services & Orchestration)
│  ├─ Contracts/          (Concrete service implementations, mapping)
│  └─ Specifications/     (Query logic encapsulation)
├─ ServiceAbstraction/    (Service interfaces + Facade abstractions)
├─ DomainLayer/           (Entities, Domain Contracts, Base abstractions)
├─ Infrastructure/
│  └─ Persistence/        (EF Core / Data access, Repositories, Seeding)
├─ Shared/                (DTOs, cross-cutting helpers, pagination)
└─ ProductSpecifications/ (Filtering, sorting & query parameter models)
```

## 2. Layer Responsibilities
- Presentation (`E-Commerce.Web`, `Presentation`): HTTP endpoints, minimal logic; delegates to service layer. Handles configuration, DI wiring, environment settings.
- Core (`Core/Contracts`): Application service implementations (e.g., `ProductService`), coordination, transaction boundaries via Unit of Work. Mapping profiles live here to transform between Domain and DTO layers.
- Service Abstraction (`ServiceAbstraction`): Interfaces (`IProductService`, `IServiceManager`) exposing a stable contract; enables substitution, mocking, versioning.
- Domain (`DomainLayer`): Entities, domain base types (`BaseEntity`), domain-level abstractions, invariants. No external dependencies.
- Infrastructure (`Infrastructure/Persistence`): EF Core (assumed) context, repository implementations, data seeding (`Dataseeding`), persistence-specific concerns.
- Shared (`Shared`): DTOs (`ProductDto`, `BrandDto`, `TypeDto`), pagination result (`PaginatedResult`), cross-cutting simple value objects / utilities.
- ProductSpecifications: Strongly-typed query parameter objects to drive specification construction (search, sorting, filtering, pagination).

## 3. Key Design Patterns & Principles
| Pattern / Principle | Location / Artifact | Purpose |
|---------------------|---------------------|---------|
| Clean / Onion Architecture | Project boundaries | Enforces dependency direction & isolation of business rules |
| Repository Pattern | `Infrastructure/Persistence/Repositories` (implied), `IGenericRepository` | Abstracts data access; swap persistence without touching services |
| Unit of Work | `IUnitOfWork` + Persistence implementation | Transactional consistency across multiple repository operations |
| Specification Pattern | `Core/Contracts/Specifications`, `ProductSpecifications` | Encapsulates query logic (filters, sorting, includes, paging) for composable, testable queries |
| DTO / Mapping (AutoMapper) | `Core/Contracts/Mappers/*`, `Shared/DataTransferObjects` | Decouples internal domain model from external contracts; version flexibility |
| Facade / Service Aggregator | `ServiceManager`, `IServiceManager` | Single entry point for grouped services; simplifies DI & composition |
| Seeding Strategy | `Dataseeding` | Idempotent initialization of reference data |
| Options & Configuration (assumed) | `appsettings*.json` | Centralized config enabling environment-based overrides |
| Pagination Abstraction | `PaginatedResult` | Standardized paging metadata & reduced duplication |
| Single Responsibility Principle | Layer & class scoping | Focused classes (e.g., query specs vs. services) |
| Open/Closed Principle | Adding new specs / DTOs | Extend via new specification classes without modifying existing logic |

## 4. Request Flow (Example: Get Products)
1. Controller receives HTTP request with query params.
2. Controller converts query params into `ProductQueryParams` / search & sorting option objects.
3. A specification is built (combining filters, includes, ordering, paging).
4. Service layer (`ProductService`) asks repository via `IUnitOfWork` to execute specification.
5. Repository translates specification into EF Core query (includes + criteria).
6. Entities returned, mapped to DTOs via AutoMapper profile (`ProductProfile` / `PictureUrlResolver`).
7. Paginated response packaged in `PaginatedResult` and returned.

Result: Controllers stay thin; query logic is reusable & testable; mapping concerns isolated.

## 5. Scalability & Maintainability Enablers
- Vertical slice friendly: New feature = new spec + DTO + service method + endpoint; minimal ripple.
- Testability: Specifications and services can be unit tested without database (mock repository interfaces).
- Extensibility: Add new filtering/sorting options by introducing new specification variants; no controller churn.
- Separation of concerns: Infra changes (e.g., move to Dapper/Document DB) isolated behind repositories + specifications.
- Performance tuning: Specifications centralize includes & projections; easy to profile & refine.
- Consistent contracts: DTO boundary prevents domain leakage & accidental over-posting.

## 6. Adding a New Feature (Pattern Playbook)
1. Define / extend DTO in `Shared/DataTransferObjects`.
2. Add fields to domain entity (if needed) inside `DomainLayer` respecting invariants.
3. Create or update specification for querying.
4. Implement service method in `ProductService` (or new service) + expose via `IServiceManager`.
5. Map via AutoMapper profile.
6. Add endpoint (controller) calling service.
7. (Optional) Seed baseline data.

## 7. Getting Started
Prerequisites: .NET 8 SDK (adjust if different). From solution root:
```
dotnet restore
dotnet build
cd E-Commerce.Web
dotnet run
```
API will listen on the configured Kestrel / HTTPS port (see `launchSettings.json`).

## 8. Testing Strategy (Recommended Next)
- Unit: Specification evaluation (criteria, includes, sorting). Service methods with mocked repositories.
- Integration: Repository + EF Core context + in-memory database.
- Contract: Endpoint + DTO shape via snapshot or schema tests.

## 9. Future Enhancements
- Introduce CQRS/mediator if command/query complexity grows.
- Add domain events for side effects (e.g., inventory adjustments).
- Introduce caching decorator for read-heavy specifications.
- Validation layer using FluentValidation for incoming query params & commands.
- Add OpenAPI/Swagger docs & versioning strategy.

## 10. Summary
The solution applies a layered, specification-driven architecture with clear seams (services, repositories, specifications, mapping) that localize change, enable testing, and allow scaling features without structural rewrites.
