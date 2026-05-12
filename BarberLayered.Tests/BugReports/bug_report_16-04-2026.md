Defect Report

1. General Information
Project: BarberBook (ASP.NET Core MVC)
Testing Type: Manual + Automated + Smoke Testing
Date: (fill if needed)
Environment: Localhost (.NET 8, EF Core, PostgreSQL/InMemory for tests)


2. Defect List

---------------------------------------------------------------------------------

DEF-001 — Incorrect UserType stored in session after login
Module: Authentication (AccountController)
Source: Automated tests

Description:
User type stored in session after login does not match expected value.

Steps to reproduce:

Execute automated login test
Check session value UserType

Expected result:
Correct user type enum value is stored in session

Actual result:
Incorrect value is stored (50331648 instead of expected 3)

Priority: High
Status: Confirmed

---------------------------------------------------------------------------------

DEF-002 — Login service does not properly validate credentials
Module: Business Logic (LoginService)
Source: Automated tests

Description:
Login logic behaves inconsistently and may allow incorrect credential handling.

Steps to reproduce:

Execute login tests with invalid credentials
Observe returned result

Expected result:
Invalid credentials should return an error

Actual result:
Service may return incorrect or inconsistent result

Priority: High
Status: Confirmed

---------------------------------------------------------------------------------

DEF-003 — RegisterService throws exception due to missing email store support
Module: Business Logic (RegisterService)
Source: Automated tests

Description:
RegisterService fails with NotSupportedException due to missing email store configuration.

Steps to reproduce:

Run RegisterService tests
Observe exception

Expected result:
User creation should succeed or fail gracefully

Actual result:
Unhandled exception occurs:
System.NotSupportedException: The default UI requires a user store with email support

Priority: High
Status: Confirmed

---------------------------------------------------------------------------------

DEF-004 — VisitRepository causes entity tracking conflict
Module: Data Access (VisitRepository)
Source: Automated tests

Description:
Entity Framework throws tracking exception when handling Visit entities.

Steps to reproduce:

Run VisitRepository tests
Observe error

Expected result:
Entities should be tracked correctly

Actual result:
Exception occurs:
InvalidOperationException: entity cannot be tracked because another instance exists

Priority: Medium
Status: Confirmed

---------------------------------------------------------------------------------

DEF-005 — Missing barber shop image on home page
Module: UI (Home Page)
Source: Manual testing

Description:
Main image on the home page is not displayed.

Steps to reproduce:

Open home page
Observe image area

Expected result:
Barber shop image should be displayed

Actual result:
Broken image placeholder is shown

Priority: Low
Status: Confirmed

---------------------------------------------------------------------------------

DEF-006 — Inconsistent language usage in UI
Module: UI
Source: Manual testing

Description:
Application uses mixed languages (English and Ukrainian).

Steps to reproduce:

Open application
Observe UI text

Expected result:
Single consistent language should be used

Actual result:
Mixed language observed (e.g., “BarberBook”, “Наші барбери”, “Увійти”)

Priority: Medium
Status: Confirmed

---------------------------------------------------------------------------------

DEF-007 — Layout issues on barber details page
Module: UI (Barber Details)
Source: Manual testing

Description:
Content is misaligned and partially cut off.

Steps to reproduce:

Open barber details page
Observe layout

Expected result:
Content should be properly aligned and fully visible

Actual result:
Elements appear misaligned or clipped

Priority: Medium
Status: Confirmed

---------------------------------------------------------------------------------

DEF-008 — Poor empty state handling (reviews page)
Module: UI (Reviews)
Source: Manual testing

Description:
Empty reviews page lacks user guidance.

Steps to reproduce:

Open reviews page with no data
Observe message

Expected result:
User should see helpful guidance or action

Actual result:
Only message displayed: “Ще немає відгуків”

Priority: Low
Status: Confirmed

---------------------------------------------------------------------------------

DEF-009 — Poor empty state handling (services page)
Module: UI (Services)
Source: Manual testing

Description:
Services page does not provide actionable feedback when empty.

Steps to reproduce:

Open services page with no data
Observe message

Expected result:
User should see guidance or next step

Actual result:
Only message displayed: “Ще немає доступних послуг”

Priority: Low
Status: Confirmed

---------------------------------------------------------------------------------

DEF-010 — Unclear navigation control (back button)
Module: UI / Navigation
Source: Manual testing

Description:
Back navigation icon is unclear and lacks label.

Steps to reproduce:

Open any detail page
Observe back navigation control

Expected result:
Navigation should be clearly labeled

Actual result:
Arrow icon without explanation may confuse users

Priority: Medium
Status: Confirmed

---------------------------------------------------------------------------------

3. Summary
Total defects: 10
High priority: 3
Medium priority: 4
Low priority: 3