# ATS Backend - Technical Assessment


## Key Features
- **Audit Logging**: Every stage change tracks the team member responsible and includes recruiter comments.
- **Dimensional Scoring**: Separate scoring for Culture-Fit, Interview, and Technical Assessment with a 1-5 scale.
- **Safety Checks**: Automatic prevention of duplicate applications for the same job using email verification.

## Tech Stack
- **Framework**: .NET 10 (Minimal APIs).
- **Database**: PostgreSQL with Entity Framework Core and Npgsql driver.
- **Architecture**: DTO-based communication to ensure internal database entities are never exposed directly to the client.

## Rich Audit Trail
I implemented a **Rich Audit Trail** to satisfy the requirement for tracking history. Accessible via `GET /api/applications/{id}`, this feature provides:
1. **Human-Readable Logs**: Joins the `StageHistory` and `TeamMembers` tables to provide actual names instead of just GUIDs/IDs.
2. **Timeline View**: Provides a chronological history of candidate movement, newest first.
3. **Accountability**: Captures the specific reasoning (comments) provided by recruiters at each step of the process.

## ⚙️ Setup & Installation
1. **Configure Database**: Update the connection string in `appsettings.json` with your PostgreSQL credentials.
2. **Run Migrations**: 
   ```bash
   dotnet ef database update

## If I Had A More Time
For now, the job and applicant applied for is being displayed by its ID. I would have made the actual job name display in place of that. I could not work on all the error responses. Also I fixed the stage transition rules but it broke again when I was moving forward. I could not really figure out what was wrong so I moved on. There are a few additions but these were my main challenges.
