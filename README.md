# ATS Backend - Technical Assessment

A high-performance Applicant Tracking System (ATS) built with **ASP.NET Core 10** and **PostgreSQL**.

## 🚀 Key Features
- **State Machine Pipeline**: Strict validation logic for candidate transitions (e.g., cannot skip screening).
- **Audit Logging**: Every stage change tracks the team member responsible and includes recruiter comments.
- **Dimensional Scoring**: Separate scoring for Culture-Fit, Interview, and Technical Assessment.
- **Safety Checks**: Automatic prevention of duplicate applications for the same job.

## 🛠 Tech Stack
- **Framework**: .NET 10 (Minimal APIs)
- **Database**: PostgreSQL with Entity Framework Core
- **Testing**: xUnit for business logic validation
- **Serialization**: System.Text.Json with Enum-to-String conversion and cycle handling.

## 🔍 Deep-Dive: Rich Audit Trail
I implemented an **Audit Trail** feature located at `GET /api/applications/{id}/audit`. 
Unlike a simple "current stage" field, this feature:
1. Joins the `StageHistory` and `TeamMembers` tables to provide human-readable names.
2. Provides a descending timeline of candidate movement.
3. Captures the specific reasoning (comments) provided by recruiters at each step.

## 🧪 Testing
Run the test suite using:
```bash
dotnet test
