# Task Management System API

A RESTful Web API for managing projects, tasks, and task comments.
This project is built with ASP.NET Core Web API and demonstrates relational database design, layered architecture, and practical API features such as filtering and sorting.

---

## 🚀 Features

* Project / Task / TaskComment CRUD
* Relational data design (Project → Task → TaskComment)
* Cascade delete (automatic cleanup of related data)
* Task filtering and sorting via query parameters
* Clean layered architecture (Controller / Service / DTO)
* Swagger API documentation

---

## 🧱 Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger (Swashbuckle)
* Newtonsoft.Json (snake_case JSON)

---

## 📊 Database Design

### Entity Relationship

```
Project
 └─ Task
     └─ TaskComment
```

* One Project can have many Tasks
* One Task can have many TaskComments

---

## 🔥 Key Design Concepts

### 1. Layered Architecture

* Controller → handles HTTP requests
* Service → business logic & data processing
* DTO → data transfer between API and client

---

### 2. DTO Pattern

DTOs are used to separate internal models from API responses.

Example:

* `TaskDto`
* `CreateTaskDto`
* `UpdateTaskDto`

---

### 3. Cascade Delete

* Deleting a project will also delete its related tasks and task comments
* Implemented via foreign key constraints with `ON DELETE CASCADE`

---

### 4. Query Filtering & Sorting

The API supports dynamic query parameters:

#### Example:

```
GET /api/tasks?task_status=doing
GET /api/tasks?assigned_to_user_id=1
GET /api/tasks?project_id=2
GET /api/tasks?sort_by=due_date&sort_order=asc
```

Supported filters:

* `task_status`
* `assigned_to_user_id`
* `project_id`

Sorting:

* `due_date`
* `created_at`
* `asc / desc`

---

### 5. snake_case API Design

* C# uses PascalCase
* API responses use snake_case

Example:

```json
{
  "task_id": 1,
  "project_name": "Energy Management System",
  "task_title": "Prepare demo",
  "task_status": "doing"
}
```

---

## 📡 API Endpoints

### Users

* `GET /api/users`
* `GET /api/users/{id}`

---

### Projects

* `GET /api/projects`
* `GET /api/projects/{id}`
* `POST /api/projects`
* `PUT /api/projects/{id}`
* `DELETE /api/projects/{id}`
* `GET /api/projects/{id}/tasks`

---

### Tasks

* `GET /api/tasks` (supports filtering & sorting)
* `GET /api/tasks/{id}`
* `POST /api/tasks`
* `PUT /api/tasks/{id}`
* `DELETE /api/tasks/{id}`

---

### Task Comments

* `GET /api/tasks/{id}/comments`
* `POST /api/tasks/{id}/comments`
* `PUT /api/task-comments/{id}`
* `DELETE /api/task-comments/{id}`

---

## 🧪 How to Run

1. Clone the repository

```
git clone <your-repo-url>
```

2. Set connection string (using user secrets recommended)

3. Run migration

```
Update-Database
```

4. Run the project

```
dotnet run
```

5. Open Swagger

```
/swagger
```


---

## 📌 Future Improvements

* Authentication / Authorization
* Pagination
* Soft delete
* Global exception handling
* Enum-based task status

---

## 👤 Author

This project is built as a portfolio to demonstrate backend development skills using ASP.NET Core.
