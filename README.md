**To start coding:**
  1. _Local Project Setup:_
     1.1 Open repository, branch _dev_layered_, with Visual Studio
     1.2 _Pull changes:_
       Go to Git Changes tab in Visual Studio and press Pull button (Arrow pointed downwards)
     1.3 _Create your branch:_
       Click New Branch
       Select dev_layered as source
       Name the branch as <branch_Yourname>
       Checkout to your branch
     1.4 _You are good to go_
  2. _Commiting Changes:_
     2.1 In the Git Changes tab, under the Changes section You will see a lot of changed files,
         **SELECT ONLY THE FILES YOU CODED IN**, all of the other garbage was generated with Your
         VisualStudio
     2.2 After You selected changed files, write a _commit message:_
         "<Message Header>
          <Message Body>"
         Short and generalized header, More details on what You worked on in the body;
         Use Present Simple ("Add new controller" instead of "Added new controller")
     2.3 Under the Commit button, select "Commit and Push"
     2.4 Notify Andrew about your commit.
  3. _Updating Your Branch:_
     3.1 Under the Git Changes tab, select _dev_layered_ branch,
         **Continue checkout if there is no conflicts**, if there is a conflict
         between your branch and _dev_layered_, than resolve the conflict or notify Andrew or Rostyk
     3.2 Pull the changes to _dev_layered_ branch
     3.3 Open this solution in terminal (You will find the option somewhere in the Git Changes tab)
     3.4 Run two commands:
         git checkout <your_branch_name>
         git merge dev_layered
     3.5 If succesful, Your branch should be updated to the _dev_layered_ state
  4. _Build&Run:_
     4.1 Double Click on the "BarberLayered.sln" under the BarberLayered Folder to open the solution view
     4.2 Install and add (**to all 3 projects**) following NuGet Packets:
         Npgsql
         Npgsql.EntityFrameworkCore.PostgreSQL
         Npgsql.EntityFrameworkCore.PostgreSQL.Design
         Microsoft.EntityFrameworkCore
         Microsoft.EntityFrameworkCore.Tools
         Microsoft.Extensions.Configuration
         Microsoft.Extensions.ConfigurationJson
         Microsoft.AspNetCore.Identity
         Serilog.AspNetCore
         Serilog.Sinks.Seq
         SonarAnalyzer.CSharp
         Bcrypt.Net-Next
     4.3 Edit _appsettings.json_ in PLL:
         Change password in the connection string to your postgres password
     4.4 Find Package Manager Console and enter the following
         Database-Update
     4.5 _You are good to go_.
