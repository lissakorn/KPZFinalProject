# Sweet Planner — Personal Task Organizer

Sweet Planner is a web-based application designed for efficient personal task management. The project focuses on clean architecture, the Repository design pattern, and a cohesive, user-friendly UI.

##  Functionality
* **Task Management (CRUD):** Full capability to create, read, update, and delete tasks.
* **Category Organization:** Tasks are grouped into categories for better structure.
* **Search and Filtering:** Filter tasks by name or category to find relevant information quickly.
* **Visual Indicators:** Color-coded priority and status badges for intuitive progress tracking.

##  Design Principles
In accordance with project requirements, the following 5 principles were implemented:
1. **Single Responsibility Principle (SRP):** Database interaction logic is separated into `Repositories/`, while controllers handle request routing.
2. **Dependency Inversion Principle (DIP):** Controllers depend on abstractions (`ITaskRepository`) instead of concrete classes.
3. **Don't Repeat Yourself (DRY):** Utilized a shared layout (`_Layout.cshtml`) and partial views for recurring UI elements.
4. **KISS (Keep It Simple, Stupid):** The project architecture remains straightforward without unnecessary or overly complex layers.
5. **YAGNI (You Aren't Gonna Need It):** Only essential features requested for a personal organizer were implemented.

##  Design Patterns
The following design patterns were utilized:
1. **Repository Pattern:** Used to decouple data access logic from the controllers.
   * *Implementation Files:* [ITaskRepository.cs](PersonalOrganizer/Repositories/ITaskRepository.cs), [TaskRepository.cs](PersonalOrganizer/Repositories/TaskRepository.cs), [CategoryRepository.cs](PersonalOrganizer/Repositories/CategoryRepository.cs).
2. **Dependency Injection:** Configured in [Program.cs](PersonalOrganizer/Program.cs) to provide repository instances to controllers, improving testability and maintainability.
3. **MVC (Model-View-Controller):** The fundamental architectural pattern used to structure the entire application. Controllers like [TaskController.cs](PersonalOrganizer/Controllers/TaskController.cs) orchestrate user requests.

##  Refactoring Techniques
The following techniques were used to ensure code quality:
1. **Extract Class:** Moved business and database logic from controllers into dedicated repository classes.
2. **Extract Interface:** Defined `ITaskRepository` and `ICategoryRepository` for better abstraction.
3. **Move Class (Enum Extraction):** Moved `TaskStatus` and `TaskPriority` into separate files to strictly follow the "one entity - one file" rule.
4. **Rename Variable/Method:** Used more descriptive names across the project for better readability and intent.
5. **Remove Code Smells:** Cleaned up controllers by removing direct database context dependencies.
