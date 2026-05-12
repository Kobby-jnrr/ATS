1. Schema Question
The Applications table is the center of the system, holding candidate info and their latest scores. 
I put Notes and StageHistory into their own tables and added indexes to the ApplicationId columns. 
This keeps the system fast even as more data is added, allowing the candidate’s history to load almost instantly.

When a recruiter views a profile using the GET /api/applications/{id} endpoint, the app gets everything in one trip to the database. 
I used a method called "eager loading" to join the notes and history into one result.

In my code, the query looks like this:
application = await db.Applications.Include(a => a.StageHistories).Include(a => a.Notes).FirstOrDefaultAsync(a => a.Id == id);

This is much better for the user because the webpage doesn't have to wait for several different requests to finish before showing the data. 
It gets everything it needs in one single round-trip.

2. Scoring Design Choices
I created three separate endpoints for scoring because hiring is a team effort. 
Different people often do different interviews at different times. 
By having separate endpoints, we prevent data loss; for example, a technical lead can save an assessment score without accidentally deleting a culture-fit score that an HR recruiter already entered.

If we needed to track every single score change ever made, I would move the scores to a new ScoreHistory table. 
The API links would stay the same so the frontend doesn't break, but the backend would save a new row for every change instead of just updating the old one.

3. How to Fix a "Missing" Stage Change
If a recruiter says a stage change didn't save, I would try these steps to find the problem:

* Check the Database: I would look at the StageHistory table to see if the change was actually recorded.
* Look at Logs: I would check server logs for 400 errors to see if the recruiter tried to skip a stage, which the system should block.
* Verify the Header: I would check if the X-Team-Member-Id was missing or wrong, which would cause the request to fail.
* Check Timestamps: I would look at the "last updated" time on the application record to see when it was last changed.
* Check for Overwrites: I would see if another team member changed the same candidate at the same time.

4. Self-Assessment

* C# (3/5): I am very comfortable with the language and making sure the code can handle many users at once.
* SQL (4/5): I know how to design databases that are organized and fast.
* Git (5/5): I make small, clear saves that show exactly how the project was built.
* REST API Design (4/5): I focus on making the API easy for other developers to use and understand.
* Writing Tests (3/5): I focus my testing on the most crucial parts, like making sure the hiring rules are always followed.