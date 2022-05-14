<h1>DiffFinder Application</h1>

<h3>What does it do?</h3>
<p>This application contains comparison of two encoded strings. This comparison checks if the inputs are the same size. Otherwise, comparison is not possible.</p>
<p>When comparing, if the inputs are equal, it does not return the comparison data. If not, it returns the diff-offsets between the two inputs and which characters are different. </p>

<h3>How is the application used?</h3>
<p>When the application runs, a web page welcomes you. With the help of the "Find Difference" tab in the NavBar, the page with the previous comparisons opens. With the "Create New" link on this page, the comparison screen opens.</p>
<p>There are two textboxes on the Create screen where you can enter two encoded strings. After entering data into these textboxes, comparisons are made with the "Calculate" button. This button sends this information with the post method.</p>
<p>In addition, this information is saved in the sqlite database integrated into the project. When this registration and calculation process is completed, you will be redirected to the listing screen. On the listing screen, you can view, edit, delete or navigate to the detail page.You can view the offset and diff values found when comparing the characters of the inputs on the listing screen.</p>

<h3>Application Structure</h3>
<p>Code-First approach and MVC structure were used for the application. I preferred SQLite as database. There are two models for this application. One is the table with two inputs and the result: DifferenceInformation. The other is the table kept if there is a difference between the inputs: DiffsOffsets. Diff, offset, conflicting characters are kept in this table. There is one to many relationships between tables.</p>
